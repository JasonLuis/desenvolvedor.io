using System.Net;
using System.Net.Http.Json;
using TodoListApi.Controllers.Task;

namespace IntegrationTest;

public class TodoList_CreateTaskTest
{
    [Fact]
    public async void RegisterTask()
    {
        // Arrange
        var app = new TaskWebApplicationFactory();
        using var client = await app.GetClientWithAcessTokenAsync();

        var task = new CreateTaskDto
        {
            Title = "Teste unitário",
            Description = "Descrição do teste unitário"
        };

        // Act
        var response = await client.PostAsJsonAsync("/api/tasks/create", task);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }
}
