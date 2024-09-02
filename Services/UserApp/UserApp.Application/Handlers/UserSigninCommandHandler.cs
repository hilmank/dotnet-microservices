using AutoMapper;
using Infrastructure.Common.Constants;
using Infrastructure.Common.Exceptions;
using Infrastructure.Common.Extensions;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using UserApp.Application.Commands;
using UserApp.Application.Exceptions;
using UserApp.Application.Middlewares;
using UserApp.Application.Protos;
using UserApp.Core.Repositories;

namespace UserApp.Application.Handlers;

public class UserSigninCommandHandler : IRequestHandler<UserSigninCommand, UserSiginResponse>
{
    private readonly ILogger<UserUpdateCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IStringLocalizer<ErrorsResource> _errorLocalizer;
    private readonly IStringLocalizer<MessagesResource> _messageLocalizer;

    public UserSigninCommandHandler(ILogger<UserUpdateCommandHandler> logger, IUserRepository userRepository, IMapper mapper, IStringLocalizer<ErrorsResource> errorLocalizer, IStringLocalizer<MessagesResource> messageLocalizer)
    {
        _logger = logger;
        _userRepository = userRepository;
        _mapper = mapper;
        _errorLocalizer = errorLocalizer;
        _messageLocalizer = messageLocalizer;
    }

    public async Task<UserSiginResponse> Handle(UserSigninCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByUsernameOrEmail(request.UsernameOrEmail);
        if (user == null)
            return new UserSiginResponse { Success = false, Message = _errorLocalizer["Error.Common.NotFound"] };
        if (user.Password.DecryptString() != request.Password)
            return new UserSiginResponse { Success = false, Message = _messageLocalizer["User.Password.Wrong"] };
        if (user.Status == StatusDataConstant.New)
            return new UserSiginResponse { Success = false, Message = _errorLocalizer["Error.Common.StatusDataNew"] };
        if (user.Status == StatusDataConstant.NotActive)
            return new UserSiginResponse { Success = false, Message = _errorLocalizer["Error.Common.StatusDataNotActive"] };

        user.UpdatedBy = user.Id;
        user.LastLogin = DateTime.Now;
        user.UpdatedDate = DateTime.Now;
        var ret = await _userRepository.Update(user, result => { });

        JwtTokenGenerator jwtTokenGenerator = new();
        return new UserSiginResponse
        {
            Success = ret,
            Message = "",
            Token = ret ? jwtTokenGenerator.GenerateToken(user) : string.Empty
        };
    }

}
