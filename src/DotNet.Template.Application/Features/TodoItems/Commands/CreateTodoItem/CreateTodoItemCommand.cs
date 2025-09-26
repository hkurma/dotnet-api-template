using DotNet.Template.Application.Common;
using DotNet.Template.Application.DTOs;
using MediatR;

namespace DotNet.Template.Application.Features.TodoItems.Commands.CreateTodoItem;

public record CreateTodoItemCommand(CreateTodoItemDto TodoItem) : IRequest<Result<TodoItemDto>>;
