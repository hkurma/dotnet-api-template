using DotNet.Template.Application.Common;
using MediatR;

namespace DotNet.Template.Application.Features.TodoItems.Commands.DeleteTodoItem;

public record DeleteTodoItemCommand(Guid Id) : IRequest<Result<bool>>;
