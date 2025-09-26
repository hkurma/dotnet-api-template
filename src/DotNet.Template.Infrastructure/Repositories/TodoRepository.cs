using DotNet.Template.Domain.Entities;
using DotNet.Template.Domain.Enums;
using DotNet.Template.Domain.Interfaces;
using DotNet.Template.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DotNet.Template.Infrastructure.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly TodoDbContext _context;

    public TodoRepository(TodoDbContext context)
    {
        _context = context;
    }

    public async Task<TodoItem?> GetByIdAsync(Guid id)
    {
        return await _context.TodoItems
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<TodoItem>> GetAllAsync()
    {
        return await _context.TodoItems
            .OrderBy(t => t.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<TodoItem>> GetByStatusAsync(TodoStatus status)
    {
        return await _context.TodoItems
            .Where(t => t.Status == status)
            .OrderBy(t => t.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<TodoItem>> GetByPriorityAsync(TodoPriority priority)
    {
        return await _context.TodoItems
            .Where(t => t.Priority == priority)
            .OrderBy(t => t.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<TodoItem>> GetOverdueAsync()
    {
        var now = DateTime.UtcNow;
        return await _context.TodoItems
            .Where(t => t.DueDate.HasValue && t.DueDate < now && t.Status != TodoStatus.Completed)
            .OrderBy(t => t.DueDate)
            .ToListAsync();
    }

    public async Task<TodoItem> AddAsync(TodoItem todoItem)
    {
        var entry = await _context.TodoItems.AddAsync(todoItem);
        return entry.Entity;
    }

    public Task<TodoItem> UpdateAsync(TodoItem todoItem)
    {
        _context.TodoItems.Update(todoItem);
        return Task.FromResult(todoItem);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var todoItem = await GetByIdAsync(id);
        if (todoItem == null)
        {
            return false;
        }

        // Soft delete
        todoItem.IsDeleted = true;
        todoItem.UpdatedAt = DateTime.UtcNow;
        _context.TodoItems.Update(todoItem);

        return true;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.TodoItems
            .AnyAsync(t => t.Id == id);
    }

    public async Task<int> CountAsync()
    {
        return await _context.TodoItems.CountAsync();
    }
}
