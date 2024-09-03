using AutoMapper;
using Infrastructure.Common.Constants;
using Infrastructure.Common.Exceptions;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using UserApp.Application.Commands;
using UserApp.Application.Protos;
using UserApp.Application.Resources;
using UserApp.Core.Repositories;

namespace UserApp.Application.Handlers;

public class UserUpdateStatusCommandHandler : IRequestHandler<UserUpdateStatusCommand, UserUpdateResponse>
{
    private readonly ILogger<UserUpdateCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IStringLocalizer<ErrorsResource> _errorLocalizer;
    private readonly IStringLocalizer<MessagesResource> _messageLocalizer;

    public UserUpdateStatusCommandHandler(ILogger<UserUpdateCommandHandler> logger, IUserRepository userRepository, IMapper mapper, IStringLocalizer<ErrorsResource> errorLocalizer, IStringLocalizer<MessagesResource> messageLocalizer)
    {
        _logger = logger;
        _userRepository = userRepository;
        _mapper = mapper;
        _errorLocalizer = errorLocalizer;
        _messageLocalizer = messageLocalizer;
    }

    public async Task<UserUpdateResponse> Handle(UserUpdateStatusCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.Get(request.Id);
        if (user == null)
            return new UserUpdateResponse { Success = false, Message = _errorLocalizer["Error.Common.NotFound"] };
        if (!StatusDataConstant.Dict.Select(x => x.Key).Contains(request.Status))
            return new UserUpdateResponse { Success = false, Message = _errorLocalizer["Error.Common.StatusDataInvalid"] };
        user.Status = request.Status;
        user.UpdatedBy = request.UpdatedBy;
        user.UpdatedDate = DateTime.Now;
        var ret = await _userRepository.Update(user, result => { });
        return new UserUpdateResponse { Success = ret };
    }

}
