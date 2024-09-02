using System;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using UserApp.Application.Commands;

namespace UserApp.Application.Validators;

public class UserCreateValidator : AbstractValidator<UserCreateCommand>
{
    public UserCreateValidator()
    {
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
        RuleFor(x => x.ConfirmPassword).NotEmpty();
        RuleFor(x => x.Email).NotEmpty();
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.Address).NotEmpty();
        RuleFor(x => x).Custom((model, context) =>
                {
                    if (model.Password != model.ConfirmPassword)
                    {
                        context.AddFailure("ConfirmPassword", "Password and Confirm Password must match.");
                    }
                });
        //        RuleFor(x => x.ProfilePicture).NotEmpty().OverridePropertyName("Type").Must(BeAImageFile);
    }
    private static bool BeAImageFile(IFormFile file)
    {
        if (file is null) return false;
        string[] imageContenTypes = ["image/jpeg", "image/jpg", "image/png", "image/bmp", "image/x-ms-bmp"];
        string fileExtension = Path.GetExtension(file.FileName).ToLower();
        return imageContenTypes.Contains(file.ContentType);
    }
}