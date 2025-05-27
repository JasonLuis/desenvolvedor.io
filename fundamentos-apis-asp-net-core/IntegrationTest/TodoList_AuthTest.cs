using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TodoListApi.Controllers.Auth;

namespace IntegrationTest
{
    public class TodoList_AuthTest
    {
        [Fact]
        public async Task PostLogInSucess()
        {
            // Arrange
            var app = new TaskWebApplicationFactory();

            var user = new LoginDto { Email = "teste@teste.com", Password = "Teste@123" };
            using var client = app.CreateClient();

            // Act
            var result = await client.PostAsJsonAsync("/api/auth/login", user);

            // Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task LogInvalid()
        {
            // Arrange
            var app = new TaskWebApplicationFactory();

            var user = new LoginDto { Email = "error@teste.com", Password = "error@123" };
            using var client = app.CreateClient();

            // Act
            var result = await client.PostAsJsonAsync("/api/auth/login", user);

            // Assert
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
        }
    }
}
