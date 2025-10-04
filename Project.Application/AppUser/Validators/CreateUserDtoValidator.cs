using FluentValidation;
using Project.Application.AppUser.Dtos;

namespace Project.Application.AppUser.Validators;

public class CreateUserDtoValidator:AbstractValidator<CreateUserDto>
{
    public CreateUserDtoValidator()
    {
        RuleFor(dto=>dto.firstName)
            .Length(30)
            .NotEmpty()
            .WithMessage("First Name Should not be null ");
        
        RuleFor(dto=>dto.lastName)
            .Length(50)
            .NotEmpty()
            .WithMessage("Last Name Should not be null ");
    }
}
