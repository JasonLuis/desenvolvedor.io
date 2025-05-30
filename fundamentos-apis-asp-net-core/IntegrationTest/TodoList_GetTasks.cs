using IntegrationTest.DataBuilders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TodoListApi.Models;

namespace IntegrationTest;

public class TodoList_GetTasks: IClassFixture<TaskWebApplicationFactory>
{
    private readonly TaskWebApplicationFactory _app;
    public TodoList_GetTasks(TaskWebApplicationFactory app)
    {
        _app = app;
    }
    [Fact]
    public async void GetTasks()
    {
        // Arrange
        var userId = _app._context.Users.Where(u => u.Email == "teste@teste.com").Select(u => u.Id).First();
        var taskDataBuilder = new TaskDataBuilder(userId);
        var taskList = taskDataBuilder.Generate(10).ToList();
        _app._context.Tasks.AddRange(taskList);
        _app._context.SaveChanges();
        using var client = await _app.GetClientWithAcessTokenAsync();


        // Act
        var response = await client.GetAsync("/api/tasks/all");
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async void GetTasksById()
    {
        // Arrange
        var userId = _app._context.Users.Where(u => u.Email == "teste@teste.com").Select(u => u.Id).First(); 
        var taskExist = _app._context.Tasks.FirstOrDefault();
        
        if (taskExist is null)
        {
            taskExist = new TaskItem
            {
                Title = "Task de Teste",
                Description = "Descrição da Task de Teste",
                UserId = userId
            };

            _app._context.Tasks.Add(taskExist);
            _app._context.SaveChanges();
        }

        using var client = await _app.GetClientWithAcessTokenAsync();
        // Act
        var response = await client.GetFromJsonAsync<TaskItem>($"/api/tasks/{taskExist.Id}");
        // Assert
        Assert.NotNull(response);
        Assert.Equal(taskExist.Title, response.Title);
        Assert.Equal(taskExist.Description, response.Description);
        Assert.Equal(taskExist.Status, response.Status);
    }

    [Fact]
    public async void GetTasksUnauthorized()
    {
       // Arrange
       using var client = await _app.GetClientWithAcessTokenAsync();

        // Act
       var response = await client.GetAsync("/api/tasks/all");
    }
}
