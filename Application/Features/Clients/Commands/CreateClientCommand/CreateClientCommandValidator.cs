using FluentValidation;

namespace Application.Features.Clients.Commands.CreateClientCommand
{
    /// <CreateClientCommandValidator>
    /// The class is created to validate the values sent by the client for the objects in this case the client class 
    /// must comply with the following filters.
    /// </CreateClientCommandValidator>
    public class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
    {
        public CreateClientCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} Cannot be empty.")
                .MaximumLength(80).WithMessage("{PropertyName} Not to exceed {MasLength} characters.");
            RuleFor(p => p.Lastname)
                .NotEmpty().WithMessage("{PropertyName} Cannot be empty.")
                .MaximumLength(80).WithMessage("{PropertyName} Not to exceed {MasLength} characters.");
            RuleFor(p => p.Birthday)
                .NotEmpty().WithMessage("Date Birthday Cannot be empty");
            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("{PropertyName} Cannot be empty")
                .EmailAddress().WithMessage("{PropertyName} Must be a valid email address")
                .MaximumLength(100).WithMessage("{PropertyName} Not to exceed {MasLength} characters.");
            RuleFor(p => p.Addres)
                .NotEmpty().WithMessage("{PropertyName} Cannot be empty")
                .MaximumLength(200).WithMessage("{PropertyName} Not to exceed {MasLength} characters.");
        }
    }
}
