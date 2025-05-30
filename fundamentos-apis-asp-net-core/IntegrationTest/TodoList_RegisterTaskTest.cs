using System.Net;
using System.Net.Http.Json;
using TodoListApi.Controllers.Task;

namespace IntegrationTest;

public class TodoList_RegisterTaskTest : IClassFixture<TaskWebApplicationFactory>
{
    private readonly TaskWebApplicationFactory _app;

    public TodoList_RegisterTaskTest(TaskWebApplicationFactory app)
    {
        _app = app;
    }

    [Fact]
    public async void RegisterTask()
    {
        // Arrange
        using var client = await _app.GetClientWithAcessTokenAsync();

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

    [Fact]
    public async void RegisterTaskUnauthorized()
    {
        // Arrange
        var app = new TaskWebApplicationFactory();
        using var client = _app.CreateClient();
        var task = new CreateTaskDto
        {
            Title = "Teste unitário",
            Description = "Descrição do teste unitário"
        };
        // Act
        var response = await client.PostAsJsonAsync("/api/tasks/create", task);
        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}
