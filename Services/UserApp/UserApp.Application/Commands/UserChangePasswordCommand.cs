using MediatR;
using UserApp.Application.Protos;

namespace UserApp.Application.Commands;

public class UserChangePasswordCommand : IRequest<UserUpdateResponse>
{
    public string UsernameOrEmail { get; set; }
    public string Password { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmPassword { get; set; }
    public string UpdatedBy { get; set; }
}
