using DotNet.Template.Domain.Enums;

namespace DotNet.Template.Application.DTOs;

public record UpdateTodoItemDto
{
    public string? Title { get; init; }
    public string? Description { get; init; }
    public TodoStatus? Status { get; init; }
    public TodoPriority? Priority { get; init; }
    public DateTime? DueDate { get; init; }
}
