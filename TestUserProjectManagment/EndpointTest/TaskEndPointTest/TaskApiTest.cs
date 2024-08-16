using Application.Commands.Task.CreateTask;
using Application.Commands.Task.UpdateTask;
using Application.Models;
using Application.Queries.Login;
using Application.Queries.Task.GetListTask;
using Domain.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using System.Net;
using System.Text;
using System.Text.Json;

namespace TestUserProjectManagment.EndpointTest.TaskEndPointTest
{
    public class TaskApiTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private int _taskId = 1;
        private int _projectId = 1;
        private readonly HttpClient _httpClient;
        private readonly Mock<IApplicationDbContext> _context;
        private readonly LoginHandler _handler;

        public TaskApiTest(WebApplicationFactory<Program> factory)
        {
            _httpClient = factory.CreateClient();
            _context = new Mock<IApplicationDbContext>();
            _handler = new LoginHandler(_context.Object);
            LoginSecced();
        }

        [Fact]
        public async void LoginSecced()
        {
            var command = new LoginQuery { UserName = "test", Password = "test" };
            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.True(result.Token.Any());

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", result.Token);
        }

        [Fact]
        public async Task CreateTask()
        {
            var command = new CreateTaskCommand
            {
                Name = "testApiCreateTask",
                Status = Domain.Enums.Status.Todo,
                DueDate = DateTime.Now,
            };

            var json = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"/api/Tasks/{_projectId}", json);

            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var responceString = await response.Content.ReadAsStringAsync();
            var Task = JsonSerializer.Deserialize<TaskDto>(responceString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Task.Should().NotBeNull();
            Task.Name.Should().Be("testApiCreateTask");
            Task.Id.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task UpdateTask()
        {
            var command = new UpdateTaskCommand
            {
                Name = "testApiUpdateTask",
                Status = Domain.Enums.Status.Inprogress,
            };

            var json = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");

            var response = await _httpClient.PatchAsync($"/api/Tasks/{_projectId}/{_taskId}", json);

            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responceString = await response.Content.ReadAsStringAsync();
            var Task = JsonSerializer.Deserialize<TaskDto>(responceString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Task.Should().NotBeNull();
            Task.Name.Should().Be("testApiUpdateTask");
        }

        [Fact]
        public async Task DeleteTask()
        {
            var response = await _httpClient.DeleteAsync($"/api/Tasks/{_projectId}/{_taskId}");

            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responceString = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<bool>(responceString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.True(result);
        }

        [Fact]
        public async Task GetListTask()
        {
            var response = await _httpClient.GetAsync($"/api/Tasks/{_projectId}");

            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responceString = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<GetListTaskQueryResult>(responceString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            result.Should().NotBeNull();
        }
    }
}
