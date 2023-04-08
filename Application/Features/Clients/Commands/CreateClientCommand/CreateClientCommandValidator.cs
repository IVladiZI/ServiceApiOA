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
            RuleFor(p => p.userDto.UserName)
                .NotEmpty().WithMessage("{PropertyName} Cannot be empty.")
                .MaximumLength(80).WithMessage("{PropertyName} Not to exceed {MasLength} characters.");
            RuleFor(p => p.userDto.Password)
                .NotEmpty().WithMessage("{PropertyName} Cannot be empty.")
                .MaximumLength(150).WithMessage("{PropertyName} Not to exceed {MasLength} characters.");
            RuleFor(p => p.userDto.Level)
                .NotEmpty().WithMessage("{PropertyName} Cannot be empty.");
            RuleFor(p => p.userDto.Client.Name)
                .NotEmpty().WithMessage("{PropertyName} Cannot be empty.")
                .MaximumLength(80).WithMessage("{PropertyName} Not to exceed {MasLength} characters.");
            RuleFor(p => p.userDto.Client.Name)
                .NotEmpty().WithMessage("{PropertyName} Cannot be empty.")
                .MaximumLength(80).WithMessage("{PropertyName} Not to exceed {MasLength} characters.");
            RuleFor(p => p.userDto.Client.Lastname)
                .NotEmpty().WithMessage("{PropertyName} Cannot be empty.")
                .MaximumLength(80).WithMessage("{PropertyName} Not to exceed {MasLength} characters.");
            RuleFor(p => p.userDto.Client.Birthday)
                .NotEmpty().WithMessage("Date Birthday Cannot be empty");
            RuleFor(p => p.userDto.Client.Email)
                .NotEmpty().WithMessage("{PropertyName} Cannot be empty")
                .EmailAddress().WithMessage("{PropertyName} Must be a valid email address")
                .MaximumLength(100).WithMessage("{PropertyName} Not to exceed {MasLength} characters.");
            RuleFor(p => p.userDto.Client.Addres)
                .NotEmpty().WithMessage("{PropertyName} Cannot be empty")
                .MaximumLength(200).WithMessage("{PropertyName} Not to exceed {MasLength} characters.");
        }
    }
}
