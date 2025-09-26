using DotNet.Template.Application.Common;
using DotNet.Template.Application.DTOs;
using MediatR;

namespace DotNet.Template.Application.Features.TodoItems.Queries.GetAllTodoItems;

public record GetAllTodoItemsQuery : IRequest<Result<IEnumerable<TodoItemDto>>>;
