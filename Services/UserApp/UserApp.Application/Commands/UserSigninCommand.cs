using MediatR;
using UserApp.Application.Protos;

namespace UserApp.Application.Commands;

public class UserSigninCommand : IRequest<UserSigninResponse>
{
    public string UsernameOrEmail { get; set; }
    public string Password { get; set; }
}
