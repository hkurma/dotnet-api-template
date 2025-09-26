using AutoMapper;
using DotNet.Template.Application.Common;
using DotNet.Template.Application.DTOs;
using DotNet.Template.Domain.Interfaces;
using MediatR;

namespace DotNet.Template.Application.Features.TodoItems.Queries.GetTodoItem;

public class GetTodoItemQueryHandler : IRequestHandler<GetTodoItemQuery, Result<TodoItemDto>>
{
    private readonly ITodoRepository _todoRepository;
    private readonly IMapper _mapper;

    public GetTodoItemQueryHandler(ITodoRepository todoRepository, IMapper mapper)
    {
        _todoRepository = todoRepository;
        _mapper = mapper;
    }

    public async Task<Result<TodoItemDto>> Handle(GetTodoItemQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var todoItem = await _todoRepository.GetByIdAsync(request.Id);
            if (todoItem == null)
            {
                return Result<TodoItemDto>.Failure("Todo item not found");
            }

            var todoItemDto = _mapper.Map<TodoItemDto>(todoItem);
            return Result<TodoItemDto>.Success(todoItemDto);
        }
        catch (Exception ex)
        {
            return Result<TodoItemDto>.Failure($"Error retrieving todo item: {ex.Message}");
        }
    }
}
