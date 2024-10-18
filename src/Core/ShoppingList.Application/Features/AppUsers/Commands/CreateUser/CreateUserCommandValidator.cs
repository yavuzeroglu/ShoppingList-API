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
        
        RuleFor(u => u.Password)
            .NotEmpty()
            .Length(5,25);

        RuleFor(u => u.PasswordConfirm)
            .NotEmpty()
            .Length(5,25)
            .Equal(u => u.Password);
            
    }
}
