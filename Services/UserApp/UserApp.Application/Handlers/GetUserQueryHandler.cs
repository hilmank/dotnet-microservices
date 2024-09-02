using AutoMapper;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using UserApp.Application.Protos;
using UserApp.Application.Queries;
using UserApp.Core.Repositories;

namespace UserApp.Application.Handlers;

public class GetUserQueryHandler(
    IMapper mapper,
    ILogger<GetUsersQueryHandler> logger,
    IUserRepository userRepository)
    : IRequestHandler<GetUserQuery, UserModel>
{
    private readonly ILogger<GetUsersQueryHandler> _logger = logger;

    public async Task<UserModel> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.Get(request.Id);
        return mapper.Map<UserModel>(user);

    }
}
