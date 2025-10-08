using FluentValidation;
using Project.Application.AppUser.Dtos;

namespace Project.Application.AppUser.Validators;

public class AddRoleDtoValidator:AbstractValidator<AddRoleDto>
{
    public AddRoleDtoValidator()
    {
        RuleFor(dto => dto.UserId)
            .NotEmpty()
            .WithMessage("UserId is required");

        RuleFor(dto => dto.role)
            .NotEmpty()
            .WithMessage("Role Name is Required");
    }
}
