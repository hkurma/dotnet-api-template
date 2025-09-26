using AutoMapper;
using DotNet.Template.Application.Common;
using DotNet.Template.Application.DTOs;
using DotNet.Template.Domain.Entities;
using DotNet.Template.Domain.Interfaces;
using MediatR;

namespace DotNet.Template.Application.Features.TodoItems.Commands.CreateTodoItem;

public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, Result<TodoItemDto>>
{
    private readonly ITodoRepository _todoRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateTodoItemCommandHandler(ITodoRepository todoRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _todoRepository = todoRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<TodoItemDto>> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var todoItem = _mapper.Map<TodoItem>(request.TodoItem);

            var createdTodoItem = await _todoRepository.AddAsync(todoItem);
            await _unitOfWork.SaveChangesAsync();

            var todoItemDto = _mapper.Map<TodoItemDto>(createdTodoItem);
            return Result<TodoItemDto>.Success(todoItemDto);
        }
        catch (Exception ex)
        {
            return Result<TodoItemDto>.Failure($"Error creating todo item: {ex.Message}");
        }
    }
}
