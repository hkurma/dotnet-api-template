using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using DotNet.Template.Application.DTOs;
using Microsoft.AspNetCore.Mvc.Testing;

namespace DotNet.Template.Tests.Integration;

public class TodoItemsControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _jsonOptions;

    public TodoItemsControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }


    [Fact]
    public async Task CreateTodoItem_Should_Return_CreatedTodoItem()
    {
        // Arrange
        var createDto = new CreateTodoItemDto
        {
            Title = "Test Todo",
            Description = "Test Description",
            Priority = DotNet.Template.Domain.Enums.TodoPriority.High
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/todoitems", createDto);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var content = await response.Content.ReadAsStringAsync();
        var todoItem = JsonSerializer.Deserialize<TodoItemDto>(content, _jsonOptions);

        Assert.NotNull(todoItem);
        Assert.Equal(createDto.Title, todoItem.Title);
        Assert.Equal(createDto.Description, todoItem.Description);
        Assert.Equal(createDto.Priority, todoItem.Priority);
        Assert.Equal(DotNet.Template.Domain.Enums.TodoStatus.Pending, todoItem.Status);
    }

    [Fact]
    public async Task GetTodoItem_Should_Return_NotFound_For_NonExistent_Id()
    {
        // Arrange
        var nonExistentId = Guid.NewGuid();

        // Act
        var response = await _client.GetAsync($"/api/todoitems/{nonExistentId}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task CreateAndGetTodoItem_Should_Work_EndToEnd()
    {
        // Arrange
        var createDto = new CreateTodoItemDto
        {
            Title = "Integration Test Todo",
            Description = "End-to-end test",
            Priority = DotNet.Template.Domain.Enums.TodoPriority.Medium
        };

        // Act - Create
        var createResponse = await _client.PostAsJsonAsync("/api/todoitems", createDto);
        createResponse.EnsureSuccessStatusCode();

        var createContent = await createResponse.Content.ReadAsStringAsync();
        var createdTodoItem = JsonSerializer.Deserialize<TodoItemDto>(createContent, _jsonOptions);

        Assert.NotNull(createdTodoItem);

        // Act - Get
        var getResponse = await _client.GetAsync($"/api/todoitems/{createdTodoItem.Id}");

        // Assert
        getResponse.EnsureSuccessStatusCode();
        var getContent = await getResponse.Content.ReadAsStringAsync();
        var retrievedTodoItem = JsonSerializer.Deserialize<TodoItemDto>(getContent, _jsonOptions);

        Assert.NotNull(retrievedTodoItem);
        Assert.Equal(createdTodoItem.Id, retrievedTodoItem.Id);
        Assert.Equal(createdTodoItem.Title, retrievedTodoItem.Title);
        Assert.Equal(createdTodoItem.Description, retrievedTodoItem.Description);
    }
}
