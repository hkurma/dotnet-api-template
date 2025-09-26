using DotNet.Template.Application.DTOs;
using DotNet.Template.Application.Features.TodoItems.Commands.CreateTodoItem;
using DotNet.Template.Application.Features.TodoItems.Commands.DeleteTodoItem;
using DotNet.Template.Application.Features.TodoItems.Commands.UpdateTodoItem;
using DotNet.Template.Application.Features.TodoItems.Queries.GetAllTodoItems;
using DotNet.Template.Application.Features.TodoItems.Queries.GetTodoItem;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DotNet.Template.Api.Controllers;

public class TodoItemsController : BaseApiController
{
    private readonly IMediator _mediator;
    private readonly ILogger<TodoItemsController> _logger;

    public TodoItemsController(IMediator mediator, ILogger<TodoItemsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Get all todo items
    /// </summary>
    /// <returns>List of todo items</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetAllTodoItems()
    {
        _logger.LogInformation("Getting all todo items");

        var result = await _mediator.Send(new GetAllTodoItemsQuery());

        return HandleResult(result);
    }

    /// <summary>
    /// Get a specific todo item by ID
    /// </summary>
    /// <param name="id">Todo item ID</param>
    /// <returns>Todo item details</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<TodoItemDto>> GetTodoItem(Guid id)
    {
        _logger.LogInformation("Getting todo item with ID: {Id}", id);

        var result = await _mediator.Send(new GetTodoItemQuery(id));

        return HandleResult(result);
    }

    /// <summary>
    /// Create a new todo item
    /// </summary>
    /// <param name="createTodoItemDto">Todo item creation data</param>
    /// <returns>Created todo item</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<TodoItemDto>> CreateTodoItem([FromBody] CreateTodoItemDto createTodoItemDto)
    {
        _logger.LogInformation("Creating new todo item with title: {Title}", createTodoItemDto.Title);

        var result = await _mediator.Send(new CreateTodoItemCommand(createTodoItemDto));

        if (!result.IsSuccess)
        {
            return HandleResult(result);
        }

        return CreatedAtAction(nameof(GetTodoItem), new { id = result.Value!.Id }, result.Value);
    }

    /// <summary>
    /// Update an existing todo item
    /// </summary>
    /// <param name="id">Todo item ID</param>
    /// <param name="updateTodoItemDto">Todo item update data</param>
    /// <returns>Updated todo item</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<TodoItemDto>> UpdateTodoItem(Guid id, [FromBody] UpdateTodoItemDto updateTodoItemDto)
    {
        _logger.LogInformation("Updating todo item with ID: {Id}", id);

        var result = await _mediator.Send(new UpdateTodoItemCommand(id, updateTodoItemDto));

        return HandleResult(result);
    }

    /// <summary>
    /// Delete a todo item
    /// </summary>
    /// <param name="id">Todo item ID</param>
    /// <returns>No content</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> DeleteTodoItem(Guid id)
    {
        _logger.LogInformation("Deleting todo item with ID: {Id}", id);

        var result = await _mediator.Send(new DeleteTodoItemCommand(id));

        return HandleResult(result);
    }
}
