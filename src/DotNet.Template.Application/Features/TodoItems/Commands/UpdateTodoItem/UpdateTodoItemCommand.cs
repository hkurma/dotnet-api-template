using DotNet.Template.Application.Common;
using DotNet.Template.Application.DTOs;
using MediatR;

namespace DotNet.Template.Application.Features.TodoItems.Commands.UpdateTodoItem;

public record UpdateTodoItemCommand(Guid Id, UpdateTodoItemDto TodoItem) : IRequest<Result<TodoItemDto>>;
