using DotNet.Template.Domain.Enums;

namespace DotNet.Template.Application.DTOs;

public record TodoItemDto
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
    public TodoStatus Status { get; init; }
    public TodoPriority Priority { get; init; }
    public DateTime? DueDate { get; init; }
    public bool IsCompleted { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
    public string? CreatedBy { get; init; }
    public string? UpdatedBy { get; init; }
}
