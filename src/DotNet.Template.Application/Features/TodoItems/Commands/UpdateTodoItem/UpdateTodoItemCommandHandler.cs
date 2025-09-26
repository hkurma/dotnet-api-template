using AutoMapper;
using DotNet.Template.Application.Common;
using DotNet.Template.Application.DTOs;
using DotNet.Template.Domain.Interfaces;
using MediatR;

namespace DotNet.Template.Application.Features.TodoItems.Commands.UpdateTodoItem;

public class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTodoItemCommand, Result<TodoItemDto>>
{
    private readonly ITodoRepository _todoRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateTodoItemCommandHandler(ITodoRepository todoRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _todoRepository = todoRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<TodoItemDto>> Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var existingTodoItem = await _todoRepository.GetByIdAsync(request.Id);
            if (existingTodoItem == null)
            {
                return Result<TodoItemDto>.Failure("Todo item not found");
            }

            // Update only the provided fields
            if (!string.IsNullOrEmpty(request.TodoItem.Title))
            {
                existingTodoItem.UpdateTitle(request.TodoItem.Title);
            }

            if (request.TodoItem.Description != null)
            {
                existingTodoItem.UpdateDescription(request.TodoItem.Description);
            }

            if (request.TodoItem.Status.HasValue)
            {
                switch (request.TodoItem.Status.Value)
                {
                    case DotNet.Template.Domain.Enums.TodoStatus.Completed:
                        existingTodoItem.MarkAsCompleted();
                        break;
                    case DotNet.Template.Domain.Enums.TodoStatus.InProgress:
                        existingTodoItem.MarkAsInProgress();
                        break;
                    case DotNet.Template.Domain.Enums.TodoStatus.Cancelled:
                        existingTodoItem.Cancel();
                        break;
                }
            }

            if (request.TodoItem.Priority.HasValue)
            {
                existingTodoItem.SetPriority(request.TodoItem.Priority.Value);
            }

            if (request.TodoItem.DueDate.HasValue)
            {
                existingTodoItem.SetDueDate(request.TodoItem.DueDate.Value);
            }

            var updatedTodoItem = await _todoRepository.UpdateAsync(existingTodoItem);
            await _unitOfWork.SaveChangesAsync();

            var todoItemDto = _mapper.Map<TodoItemDto>(updatedTodoItem);
            return Result<TodoItemDto>.Success(todoItemDto);
        }
        catch (Exception ex)
        {
            return Result<TodoItemDto>.Failure($"Error updating todo item: {ex.Message}");
        }
    }
}
