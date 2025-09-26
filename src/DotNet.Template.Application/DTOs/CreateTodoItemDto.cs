using DotNet.Template.Domain.Enums;

namespace DotNet.Template.Application.DTOs;

public record CreateTodoItemDto
{
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
    public TodoPriority Priority { get; init; } = TodoPriority.Medium;
    public DateTime? DueDate { get; init; }
}
