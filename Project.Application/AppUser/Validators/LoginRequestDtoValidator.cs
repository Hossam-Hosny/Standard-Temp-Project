using FluentValidation;
using Project.Application.AppUser.Dtos;

namespace Project.Application.AppUser.Validators;

public class LoginRequestDtoValidator:AbstractValidator<LoginRequestDto>
{
    public LoginRequestDtoValidator()
    {
        RuleFor(dto => dto.Email)
            .EmailAddress()
            .NotEmpty()
            .WithMessage("Email Required !");

        RuleFor(dto => dto.Password)
         .MaximumLength(256)
         .NotEmpty()
         .WithMessage("Password Required !");
    }
}
