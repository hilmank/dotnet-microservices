using MediatR;
using UserApp.Application.Protos;

namespace UserApp.Application.Queries;

public class GetUserByUsernameOrEmailQuery : IRequest<UserModel>
{
    public string UsernameOrEmail { get; set; }
    public GetUserByUsernameOrEmailQuery(string usernameOrEmail)
    {
        UsernameOrEmail = usernameOrEmail;
    }
}
