using DotNet.Template.Domain.Common;
using DotNet.Template.Domain.Enums;

namespace DotNet.Template.Domain.Entities;

public class TodoItem : BaseEntity, IAuditable
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public TodoStatus Status { get; set; } = TodoStatus.Pending;
    public TodoPriority Priority { get; set; } = TodoPriority.Medium;
    public DateTime? DueDate { get; set; }
    public bool IsCompleted => Status == TodoStatus.Completed;
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }

    public void MarkAsCompleted()
    {
        Status = TodoStatus.Completed;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsInProgress()
    {
        Status = TodoStatus.InProgress;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Cancel()
    {
        Status = TodoStatus.Cancelled;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty", nameof(title));

        Title = title;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateDescription(string? description)
    {
        Description = description;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetPriority(TodoPriority priority)
    {
        Priority = priority;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetDueDate(DateTime? dueDate)
    {
        DueDate = dueDate;
        UpdatedAt = DateTime.UtcNow;
    }
}
