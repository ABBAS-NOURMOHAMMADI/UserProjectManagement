using Application.Commands.Project.CreateProject;
using Application.Commands.Project.UpdateProject;
using Application.Models;
using Application.Queries.Login;
using Application.Queries.Project.GetListProject;
using Domain.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using System.Net;
using System.Text;
using System.Text.Json;

namespace TestUserProjectManagment.EndpointTest.ProjectEndPointTest
{
    public class ProjectApiTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private int _projectId = 1;
        private readonly HttpClient _httpClient;
        private readonly Mock<IApplicationDbContext> _context;
        private readonly LoginHandler _handler;

        public ProjectApiTest(WebApplicationFactory<Program> factory)
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
        public async Task CreateProject()
        {
            var command = new CreateProjectCommand { Name = "testApiCreateProject" };

            var json = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/projects", json);

            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var responceString = await response.Content.ReadAsStringAsync();
            var project = JsonSerializer.Deserialize<ProjectDto>(responceString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            project.Should().NotBeNull();
            project.Name.Should().Be("testApiCreateProject");
            project.Id.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task UpdateProject()
        {
            var command = new UpdateProjectCommand
            {
                Name = "testApiUpdateProject",
                ProjectId = _projectId
            };

            var json = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");

            var response = await _httpClient.PatchAsync("/api/projects", json);

            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responceString = await response.Content.ReadAsStringAsync();
            var project = JsonSerializer.Deserialize<ProjectDto>(responceString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            project.Should().NotBeNull();
            project.Name.Should().Be("testApiUpdateProject");
        }

        [Fact]
        public async Task DeleteProject()
        {
            var response = await _httpClient.DeleteAsync($"/api/projects/{_projectId}");

            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responceString = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<bool>(responceString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.True(result);
        }

        [Fact]
        public async Task GetListProject()
        {
            var response = await _httpClient.GetAsync($"/api/projects");

            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responceString = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<GetListProjectQueryResult>(responceString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            result.Should().NotBeNull();
        }
    }
}
