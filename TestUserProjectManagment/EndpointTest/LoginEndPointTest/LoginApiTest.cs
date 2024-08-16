using Application.Queries.Login;
using FluentAssertions;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Text;
using System.Text.Json;

namespace TestUserProjectManagment.EndpointTest.LoginEndPointTest
{
    public class LoginApiTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;

        public LoginApiTest(WebApplicationFactory<Program> factory)
        {
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task LoginSucced()
        {
            var command = new LoginQuery { UserName = "test", Password = "test" };

            var json = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/login", json);

            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responceString = await response.Content.ReadAsStringAsync();
            var login = JsonSerializer.Deserialize<LoginQueryResult>(responceString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            login.Should().NotBeNull();
            Assert.True(login.Token.Any());
        }

        [Fact]
        public async Task LoginFailed()
        {
            var command = new LoginQuery { UserName = "123", Password = "123" };

            var json = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/login", json);

            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);

            var responceString = await response.Content.ReadAsStringAsync();
            var login = JsonSerializer.Deserialize<BadRequest>(responceString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            login.Should().NotBeNull();
            Assert.True(login.TechnicalMessage.Contains("user not found"));
        }
    }
}
