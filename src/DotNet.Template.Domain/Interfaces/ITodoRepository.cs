using DotNet.Template.Domain.Entities;

namespace DotNet.Template.Domain.Interfaces;

public interface ITodoRepository
{
    Task<TodoItem?> GetByIdAsync(Guid id);
    Task<IEnumerable<TodoItem>> GetAllAsync();
    Task<IEnumerable<TodoItem>> GetByStatusAsync(DotNet.Template.Domain.Enums.TodoStatus status);
    Task<IEnumerable<TodoItem>> GetByPriorityAsync(DotNet.Template.Domain.Enums.TodoPriority priority);
    Task<IEnumerable<TodoItem>> GetOverdueAsync();
    Task<TodoItem> AddAsync(TodoItem todoItem);
    Task<TodoItem> UpdateAsync(TodoItem todoItem);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
    Task<int> CountAsync();
}
