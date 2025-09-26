using DotNet.Template.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotNet.Template.Infrastructure.Data;

public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
    {
    }

    public DbSet<TodoItem> TodoItems { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TodoItem>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.Description)
                .HasMaxLength(1000);

            entity.Property(e => e.Status)
                .IsRequired()
                .HasConversion<int>();

            entity.Property(e => e.Priority)
                .IsRequired()
                .HasConversion<int>();

            entity.Property(e => e.CreatedAt)
                .IsRequired();

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100);

            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100);

            entity.HasQueryFilter(e => !e.IsDeleted);

            entity.HasIndex(e => e.Status);
            entity.HasIndex(e => e.Priority);
            entity.HasIndex(e => e.DueDate);
            entity.HasIndex(e => e.CreatedAt);
        });
    }
}
