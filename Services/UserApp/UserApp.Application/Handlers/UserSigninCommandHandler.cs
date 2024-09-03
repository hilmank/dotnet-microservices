using AutoMapper;
using Infrastructure.Common.Constants;
using Infrastructure.Common.Exceptions;
using Infrastructure.Common.Extensions;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using UserApp.Application.Commands;
using UserApp.Application.Middlewares;
using UserApp.Application.Protos;
using UserApp.Application.Resources;
using UserApp.Core.Repositories;

namespace UserApp.Application.Handlers;

public class UserSigninCommandHandler : IRequestHandler<UserSigninCommand, UserSigninResponse>
{
    private readonly ILogger<UserUpdateCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IStringLocalizer<ErrorsResource> _errorLocalizer;
    private readonly IStringLocalizer<MessagesResource> _messageLocalizer;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public UserSigninCommandHandler(ILogger<UserUpdateCommandHandler> logger, IUserRepository userRepository, IMapper mapper, IStringLocalizer<ErrorsResource> errorLocalizer, IStringLocalizer<MessagesResource> messageLocalizer, IJwtTokenGenerator jwtTokenGenerator)
    {
        _logger = logger;
        _userRepository = userRepository;
        _mapper = mapper;
        _errorLocalizer = errorLocalizer;
        _messageLocalizer = messageLocalizer;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<UserSigninResponse> Handle(UserSigninCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByUsernameOrEmail(request.UsernameOrEmail);
        if (user == null)
            return new UserSigninResponse { Success = false, Message = _errorLocalizer["Error.Common.NotFound"] };
        if (user.Password.DecryptString() != request.Password)
            return new UserSigninResponse { Success = false, Message = _messageLocalizer["User.Password.Wrong"] };
        if (user.Status == StatusDataConstant.New)
            return new UserSigninResponse { Success = false, Message = _errorLocalizer["Error.Common.StatusDataNew"] };
        if (user.Status == StatusDataConstant.NotActive)
            return new UserSigninResponse { Success = false, Message = _errorLocalizer["Error.Common.StatusDataNotActive"] };

        user.UpdatedBy = user.Id;
        user.LastLogin = DateTime.Now;
        user.UpdatedDate = DateTime.Now;
        var ret = await _userRepository.Update(user, result => { });

        return new UserSigninResponse
        {
            Success = ret,
            Message = "",
            Token = ret ? _jwtTokenGenerator.GenerateToken(user) : string.Empty
        };
    }

}
