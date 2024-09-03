using MediatR;
using UserApp.Application.Protos;

namespace UserApp.Application.Commands;

public class UserResetPasswordCommand : IRequest<UserUpdateResponse>
{
    public string Id { get; set; }
    public string UpdatedBy { get; set; }
}