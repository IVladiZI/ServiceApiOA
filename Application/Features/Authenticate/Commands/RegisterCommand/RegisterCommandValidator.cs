using FluentValidation;

namespace Application.Features.Authenticate.Commands.RegisterCommand
{
    /// <RegisterCommandValidator>
    /// In this class we place the validators for each requested parameter
    /// </RegisterCommandValidator>
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(p => p.UserName)
                .NotEmpty().WithMessage("{PropertyName} Cannot be empty.")
                .MaximumLength(10).WithMessage("{PropertyName} Not to exceed {MasLength} characters.");
            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("{PropertyName} Cannot be empty.")
                .MaximumLength(15).WithMessage("{PropertyName} Not to exceed {MasLength} characters.");
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} Cannot be empty.")
                .MaximumLength(80).WithMessage("{PropertyName} Not to exceed {MasLength} characters.");
            RuleFor(p => p.LastName)
                .NotEmpty().WithMessage("{PropertyName} Cannot be empty.")
                .MaximumLength(80).WithMessage("{PropertyName} Not to exceed {MasLength} characters.");
            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("{PropertyName} Cannot be empty")
                .EmailAddress().WithMessage("{PropertyName} Must be a valid email address")
                .MaximumLength(100).WithMessage("{PropertyName} Not to exceed {MasLength} characters.");
        }
    }
}
