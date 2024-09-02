using MediatR;
using UserApp.Application.Protos;

namespace UserApp.Application.Queries;

public class GetUsersQuery : IRequest<IEnumerable<UserModel>>
{
    public GetUsersQuery()
    {
    }
}