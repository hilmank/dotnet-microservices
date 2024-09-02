using MediatR;
using UserApp.Application.Protos;

namespace UserApp.Application.Commands;

public class UserDeleteCommand : IRequest<UserDeleteResponse>
{
    public string UserId { get; set; }
    public string Id { get; set; }
}
