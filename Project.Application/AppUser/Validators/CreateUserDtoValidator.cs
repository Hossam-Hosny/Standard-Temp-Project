using FluentValidation;
using Project.Application.AppUser.Dtos;

namespace Project.Application.AppUser.Validators;

public class CreateUserDtoValidator:AbstractValidator<CreateUserDto>
{
    public CreateUserDtoValidator()
    {
        RuleFor(dto=>dto.FirstName)
            .MaximumLength(30)
            .NotEmpty()
            .WithMessage("First Name Should not be null ");
        
        RuleFor(dto=>dto.LastName)
            .MaximumLength(50)
            .NotEmpty()
            .WithMessage("Last Name Should not be null ");

        RuleFor(dto => dto.UserName)
         .MaximumLength(100)
         .NotEmpty()
         .WithMessage("UserName Should not be null ");

        RuleFor(dto => dto.Email)
         .EmailAddress()
         .NotEmpty()
         .WithMessage("Email Should not be null ");

        RuleFor(dto => dto.Password)
         .MaximumLength(256)
         .NotEmpty()
         .WithMessage("Password Should not be null ");
    }
}
