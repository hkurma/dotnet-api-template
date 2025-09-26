using DotNet.Template.Domain.Entities;
using DotNet.Template.Domain.Enums;

namespace DotNet.Template.Tests.Unit.Domain;

public class TodoItemTests
{
    [Fact]
    public void TodoItem_Should_Initialize_With_Default_Values()
    {
        // Arrange & Act
        var todoItem = new TodoItem();

        // Assert
        Assert.NotEqual(Guid.Empty, todoItem.Id);
        Assert.Equal(TodoStatus.Pending, todoItem.Status);
        Assert.Equal(TodoPriority.Medium, todoItem.Priority);
        Assert.False(todoItem.IsDeleted);
        Assert.False(todoItem.IsCompleted);
        Assert.True(todoItem.CreatedAt <= DateTime.UtcNow);
    }

    [Fact]
    public void MarkAsCompleted_Should_Update_Status_And_UpdatedAt()
    {
        // Arrange
        var todoItem = new TodoItem();

        // Act
        todoItem.MarkAsCompleted();

        // Assert
        Assert.Equal(TodoStatus.Completed, todoItem.Status);
        Assert.True(todoItem.IsCompleted);
        Assert.NotNull(todoItem.UpdatedAt);
        Assert.True(todoItem.UpdatedAt.Value <= DateTime.UtcNow);
    }

    [Fact]
    public void UpdateTitle_Should_Update_Title_And_UpdatedAt()
    {
        // Arrange
        var todoItem = new TodoItem();
        var newTitle = "New Title";

        // Act
        todoItem.UpdateTitle(newTitle);

        // Assert
        Assert.Equal(newTitle, todoItem.Title);
        Assert.NotNull(todoItem.UpdatedAt);
        Assert.True(todoItem.UpdatedAt.Value <= DateTime.UtcNow);
    }

    [Fact]
    public void UpdateTitle_Should_Throw_ArgumentException_When_Title_Is_Empty()
    {
        // Arrange
        var todoItem = new TodoItem();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => todoItem.UpdateTitle(""));
        Assert.Throws<ArgumentException>(() => todoItem.UpdateTitle("   "));
        Assert.Throws<ArgumentException>(() => todoItem.UpdateTitle(null!));
    }

    [Fact]
    public void SetPriority_Should_Update_Priority_And_UpdatedAt()
    {
        // Arrange
        var todoItem = new TodoItem();
        var newPriority = TodoPriority.High;

        // Act
        todoItem.SetPriority(newPriority);

        // Assert
        Assert.Equal(newPriority, todoItem.Priority);
        Assert.NotNull(todoItem.UpdatedAt);
        Assert.True(todoItem.UpdatedAt.Value <= DateTime.UtcNow);
    }
}
