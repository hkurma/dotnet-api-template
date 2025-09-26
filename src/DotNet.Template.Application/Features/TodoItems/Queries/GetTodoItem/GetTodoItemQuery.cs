using DotNet.Template.Application.Common;
using DotNet.Template.Application.DTOs;
using MediatR;

namespace DotNet.Template.Application.Features.TodoItems.Queries.GetTodoItem;

public record GetTodoItemQuery(Guid Id) : IRequest<Result<TodoItemDto>>;
