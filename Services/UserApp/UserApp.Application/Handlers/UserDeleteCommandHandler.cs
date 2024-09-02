using AutoMapper;
using Infrastructure.Common.Exceptions;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using UserApp.Application.Commands;
using UserApp.Application.Exceptions;
using UserApp.Application.Protos;
using UserApp.Core.Repositories;

namespace UserApp.Application.Handlers;

public class UserDeleteCommandHandler : IRequestHandler<UserDeleteCommand, UserDeleteResponse>
{
    private readonly ILogger<UserUpdateCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IStringLocalizer<ErrorsResource> _errorLocalizer;
    private readonly IStringLocalizer<MessagesResource> _messageLocalizer;
    public UserDeleteCommandHandler(ILogger<UserUpdateCommandHandler> logger, IMapper mapper, IUserRepository userRepository, IStringLocalizer<ErrorsResource> errorLocalizer, IStringLocalizer<MessagesResource> messageLocalizer)
    {
        _logger = logger;
        _mapper = mapper;
        _userRepository = userRepository;
        _errorLocalizer = errorLocalizer;
        _messageLocalizer = messageLocalizer;
    }

    public async Task<UserDeleteResponse> Handle(UserDeleteCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.Get(request.Id);
        if (user is null)
            return new UserDeleteResponse { Success = false, Message = _errorLocalizer["Error.Common.NotFound"] };
        var ret = await _userRepository.Delete(request.UserId, request.Id, result =>
        {
        });
        return new UserDeleteResponse { Success = ret };

    }
}
