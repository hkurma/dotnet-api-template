using DotNet.Template.Application.Common;
using DotNet.Template.Domain.Interfaces;
using MediatR;

namespace DotNet.Template.Application.Features.TodoItems.Commands.DeleteTodoItem;

public class DeleteTodoItemCommandHandler : IRequestHandler<DeleteTodoItemCommand, Result<bool>>
{
    private readonly ITodoRepository _todoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTodoItemCommandHandler(ITodoRepository todoRepository, IUnitOfWork unitOfWork)
    {
        _todoRepository = todoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var exists = await _todoRepository.ExistsAsync(request.Id);
            if (!exists)
            {
                return Result<bool>.Failure("Todo item not found");
            }

            var deleted = await _todoRepository.DeleteAsync(request.Id);
            if (!deleted)
            {
                return Result<bool>.Failure("Failed to delete todo item");
            }

            await _unitOfWork.SaveChangesAsync();
            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure($"Error deleting todo item: {ex.Message}");
        }
    }
}
