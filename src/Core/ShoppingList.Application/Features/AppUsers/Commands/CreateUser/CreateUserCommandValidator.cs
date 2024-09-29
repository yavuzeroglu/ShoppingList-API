using FluentValidation;

namespace ShoppingList.Application.Features.AppUsers.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommandRequest>
{
    public CreateUserCommandValidator()
    {
        RuleFor(u => u.Email)
            .NotEmpty()
            .EmailAddress()
            .MinimumLength(8);
            
    }
}
