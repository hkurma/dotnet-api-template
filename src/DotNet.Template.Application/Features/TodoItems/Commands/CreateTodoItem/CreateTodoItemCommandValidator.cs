using FluentValidation;

namespace DotNet.Template.Application.Features.TodoItems.Commands.CreateTodoItem;

public class CreateTodoItemCommandValidator : AbstractValidator<CreateTodoItemCommand>
{
    public CreateTodoItemCommandValidator()
    {
        RuleFor(x => x.TodoItem.Title)
            .NotEmpty()
            .WithMessage("Title is required")
            .MaximumLength(200)
            .WithMessage("Title must not exceed 200 characters");

        RuleFor(x => x.TodoItem.Description)
            .MaximumLength(1000)
            .WithMessage("Description must not exceed 1000 characters");

        RuleFor(x => x.TodoItem.DueDate)
            .GreaterThanOrEqualTo(DateTime.UtcNow.Date)
            .WithMessage("Due date must be today or in the future")
            .When(x => x.TodoItem.DueDate.HasValue);
    }
}
