using System;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using UserApp.Application.Commands;

namespace UserApp.Application.Validators;

public class UserChangePasswordValidator : AbstractValidator<UserChangePasswordCommand>
{
    public UserChangePasswordValidator()
    {
        RuleFor(x => x.UsernameOrEmail).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
        RuleFor(x => x.NewPassword).NotEmpty();
        RuleFor(x => x.ConfirmPassword).NotEmpty();
        RuleFor(x => x).Custom((model, context) =>
        {
            if (model.NewPassword != model.ConfirmPassword)
            {
                context.AddFailure("ConfirmPassword", "New Password and Confirm Password must match.");
            }
        });
    }
}