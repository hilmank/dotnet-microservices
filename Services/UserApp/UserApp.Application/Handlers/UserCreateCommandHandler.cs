using AutoMapper;
using Infrastructure.Common.Constants;
using Infrastructure.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using UserApp.Application.Commands;
using UserApp.Application.Mapper;
using UserApp.Application.Protos;
using UserApp.Application.Resources;
using UserApp.Application.Settings;
using UserApp.Application.Validators;
using UserApp.Core.Contants;
using UserApp.Core.Entities;
using UserApp.Core.Repositories;

namespace UserApp.Application.Handlers;

public class UserCreateCommandHandler : IRequestHandler<UserCreateCommand, UserCreateResponse>
{
    private readonly ILogger<UserUpdateCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IStringLocalizer<ErrorsResource> _errorLocalizer;
    private readonly IStringLocalizer<MessagesResource> _messageLocalizer;

    public UserCreateCommandHandler(ILogger<UserUpdateCommandHandler> logger, IUserRepository userRepository, IMapper mapper, IStringLocalizer<ErrorsResource> errorLocalizer, IStringLocalizer<MessagesResource> messageLocalizer)
    {
        _logger = logger;
        _userRepository = userRepository;
        _mapper = mapper;
        _errorLocalizer = errorLocalizer;
        _messageLocalizer = messageLocalizer;
    }

    public async Task<UserCreateResponse> Handle(UserCreateCommand request, CancellationToken cancellationToken)
    {
        ByteStringModel profilePicture = new() { Base64String = request.ProfilePicture.Base64String, ContentType = request.ProfilePicture.ContentType, FileName = request.ProfilePicture.FileName };
        ByteStringValidator validatror = new();
        var validatorResult = validatror.Validate(profilePicture);
        if (!validatorResult.IsValid)
            return new UserCreateResponse { Success = false, Message = string.Join("; ", validatorResult.Errors.Select(error => error.ErrorMessage).ToList()) };

        var validator = new UserCreateValidator();
        validatorResult = validator.Validate(request);
        if (!validatorResult.IsValid)
            return new UserCreateResponse { Success = false, Message = string.Join("; ", validatorResult.Errors.Select(error => error.ErrorMessage).ToList()) };
        var userExist = await _userRepository.GetByUsernameOrEmail(request.Username);
        if (userExist is not null)
            throw new ApplicationException(_messageLocalizer["User.Username.Registered"]);
        userExist = await _userRepository.GetByUsernameOrEmail(request.Email);
        if (userExist is not null)
            throw new ApplicationException(_messageLocalizer["User.Email.Registered"]);

        User user = _mapper.Map<User>(request);

        var file = _mapper.Map<FormFileModel>(profilePicture).FormFileProperty;
        List<UserFile> userFiles = [
            new() {
                Id = user.Id,
                FileName = $"{user.Id}{Path.GetExtension(file.FileName)}",
                Category = UserFileCategoryConstant.ProfilePicture,
                Description = UserFileCategoryConstant.Dict[UserFileCategoryConstant.ProfilePicture],
                FileThumbnail = $"{user.Id}{Path.GetExtension(file.FileName)}",
                Title = UserFileCategoryConstant.Dict[UserFileCategoryConstant.ProfilePicture],
                Type = FileTypeConstant.Image
                }
        ];
        user.Files = userFiles;
        Dictionary<string, IFormFile> fileToUploads = new()
        {
            { $"{user.Id}{Path.GetExtension(file.FileName)}", file }
        };

        var ret = await _userRepository.Create(user, result =>
        {
            if (result is not null)
            {
                //save file
                foreach (var item in fileToUploads)
                {
                    using (Stream fileStream = new FileStream($"{DirectorySettings.PathFileUser}/{item.Key}", FileMode.Create, FileAccess.Write))
                    {
                        item.Value.CopyTo(fileStream);
                    }
                }
            }
        });
        return new UserCreateResponse { Success = ret, Message = ret ? string.Empty : _errorLocalizer["Error.Common.Failed"] };
    }
}
