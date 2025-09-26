using AutoMapper;
using DotNet.Template.Application.Common;
using DotNet.Template.Application.DTOs;
using DotNet.Template.Domain.Interfaces;
using MediatR;

namespace DotNet.Template.Application.Features.TodoItems.Queries.GetAllTodoItems;

public class GetAllTodoItemsQueryHandler : IRequestHandler<GetAllTodoItemsQuery, Result<IEnumerable<TodoItemDto>>>
{
    private readonly ITodoRepository _todoRepository;
    private readonly IMapper _mapper;

    public GetAllTodoItemsQueryHandler(ITodoRepository todoRepository, IMapper mapper)
    {
        _todoRepository = todoRepository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<TodoItemDto>>> Handle(GetAllTodoItemsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var todoItems = await _todoRepository.GetAllAsync();
            var todoItemDtos = _mapper.Map<IEnumerable<TodoItemDto>>(todoItems);

            return Result<IEnumerable<TodoItemDto>>.Success(todoItemDtos);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<TodoItemDto>>.Failure($"Error retrieving todo items: {ex.Message}");
        }
    }
}
