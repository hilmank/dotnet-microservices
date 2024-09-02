using System;
using System.Security.Cryptography.X509Certificates;
using FluentValidation;
using UserApp.Application.Mapper;

namespace UserApp.Application.Validators;

public class ByteStringValidator : AbstractValidator<ByteStringModel>
{
    public ByteStringValidator()
    {
        RuleFor(x => x.ContentType).NotEmpty();
        RuleFor(x => x.FileName).NotEmpty();
        RuleFor(x => x.Base64String).NotEmpty();
        //RuleFor(x => x).Must(BeAValidContentType).WithMessage("Invalid content type");
    }
    private static bool BeAValidContentType(ByteStringModel file)
    {
        return FileTypeChecker.IsValidContentType(file.Base64String, file.ContentType);
    }
}
