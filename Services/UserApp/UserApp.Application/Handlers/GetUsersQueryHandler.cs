using AutoMapper;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using UserApp.Application.Protos;
using UserApp.Application.Queries;
using UserApp.Core.Repositories;

namespace UserApp.Application.Handlers;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserModel>>
{
    private readonly IMapper _mapper;
    private readonly ILogger<GetUsersQueryHandler> _logger;
    private readonly IUserRepository _userRepository;

    public GetUsersQueryHandler(IMapper mapper, ILogger<GetUsersQueryHandler> logger, IUserRepository userRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<UserModel>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAll();
        if (!users.Any()) return null!;
        return users.Select(_mapper.Map<UserModel>);
    }
}
