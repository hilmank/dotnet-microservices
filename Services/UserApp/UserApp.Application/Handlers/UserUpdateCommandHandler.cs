using System;
using AutoMapper;
using Grpc.Core;
using Infrastructure.Common.Constants;
using Infrastructure.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using UserApp.Application.Commands;
using UserApp.Application.Exceptions;
using UserApp.Application.Mapper;
using UserApp.Application.Protos;
using UserApp.Application.Settings;
using UserApp.Application.Validators;
using UserApp.Core.Contants;
using UserApp.Core.Entities;
using UserApp.Core.Repositories;

namespace UserApp.Application.Handlers;

public class UserUpdateCommandHandler : IRequestHandler<UserUpdateCommand, UserUpdateResponse>
{
    private readonly ILogger<UserUpdateCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IStringLocalizer<ErrorsResource> _errorLocalizer;
    private readonly IStringLocalizer<MessagesResource> _messageLocalizer;

    public UserUpdateCommandHandler(ILogger<UserUpdateCommandHandler> logger, IUserRepository userRepository, IMapper mapper, IStringLocalizer<ErrorsResource> errorLocalizer, IStringLocalizer<MessagesResource> messageLocalizer)
    {
        _logger = logger;
        _userRepository = userRepository;
        _mapper = mapper;
        _errorLocalizer = errorLocalizer;
        _messageLocalizer = messageLocalizer;
    }

    public async Task<UserUpdateResponse> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
    {

        var user = await _userRepository.Get(request.Id);
        if (user is null)
            return new UserUpdateResponse { Success = false, Message = _errorLocalizer["Error.Common.NotFound"] };
        if (request.Email is not null)
        {
            var userExist = await _userRepository.GetByUsernameOrEmail(request.Email);
            if (userExist.Id != userExist.Id)
                return new UserUpdateResponse { Success = false, Message = _messageLocalizer["User.Email.Registered"] };
            user.Email = request.Email!;
        }
        if (request.FirstName is not null)
            user.FirstName = request.FirstName!;
        if (request.MiddleName is not null)
            user.MiddleName = request.MiddleName!;
        if (request.LastName is not null)
            user.LastName = request.LastName!;
        if (request.Address is not null)
            user.Address = request.Address!;
        if (request.PhoneNumber is not null)
            user.PhoneNumber = request.PhoneNumber!;
        if (request.MobileNumber is not null)
            user.MobileNumber = request.MobileNumber!;
        if (request.Orgid is not null)
            user.OrgId = request.Orgid!;

        Dictionary<string, IFormFile> fileToUploads = null!;
        if (request.ProfilePicture is not null)
        {
            ByteStringModel profilePicture = new() { Base64String = request.ProfilePicture.Base64String, ContentType = request.ProfilePicture.ContentType, FileName = request.ProfilePicture.FileName };
            ByteStringValidator validatror = new();
            var validatorResult = validatror.Validate(profilePicture);
            if (!validatorResult.IsValid)
                return new UserUpdateResponse { Success = false, Message = string.Join("; ", validatorResult.Errors.Select(error => error.ErrorMessage).ToList()) };

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
            fileToUploads = new()
            {
                { $"{user.Id}{Path.GetExtension(file.FileName)}", file }
            };
        }
        var ret = await _userRepository.Update(user, result =>
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

        return new UserUpdateResponse { Success = true };
    }
}
