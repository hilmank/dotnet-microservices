using AutoMapper;
using Infrastructure.Common.Exceptions;
using Infrastructure.Common.Extensions;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using UserApp.Application.Commands;
using UserApp.Application.Exceptions;
using UserApp.Application.Protos;
using UserApp.Application.Validators;
using UserApp.Core.Repositories;

namespace UserApp.Application.Handlers;

public class UserChangePasswordCommandHandler : IRequestHandler<UserChangePasswordCommand, UserUpdateResponse>
{
    private readonly ILogger<UserUpdateCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IStringLocalizer<ErrorsResource> _errorLocalizer;
    private readonly IStringLocalizer<MessagesResource> _messageLocalizer;
    public UserChangePasswordCommandHandler(ILogger<UserUpdateCommandHandler> logger, IUserRepository userRepository, IMapper mapper, IStringLocalizer<ErrorsResource> errorLocalizer, IStringLocalizer<MessagesResource> messageLocalizer)
    {
        _logger = logger;
        _userRepository = userRepository;
        _mapper = mapper;
        _errorLocalizer = errorLocalizer;
        _messageLocalizer = messageLocalizer;
    }

    public async Task<UserUpdateResponse> Handle(UserChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var validator = new UserChangePasswordValidator();
        var validatorResult = validator.Validate(request);
        if (!validatorResult.IsValid)
            return new UserUpdateResponse { Success = false, Message = string.Join("; ", validatorResult.Errors.Select(error => error.ErrorMessage).ToList()) };

        var user = await _userRepository.GetByUsernameOrEmail(request.UsernameOrEmail);
        if (user == null)
            return new UserUpdateResponse { Success = false, Message = _errorLocalizer["Error.Common.NotFound"] };
        if (user.Password.DecryptString() != request.Password)
            return new UserUpdateResponse { Success = false, Message = _messageLocalizer["User.Password.Wrong"] };
        user.Password = request.NewPassword.EncryptString();
        user.UpdatedBy = user.Id;
        user.UpdatedDate = DateTime.Now;
        var ret = await _userRepository.Update(user, result => { });
        return new UserUpdateResponse { Success = ret };
    }
}
