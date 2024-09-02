using MediatR;
using UserApp.Application.Protos;

namespace UserApp.Application.Queries;

public class GetUserQuery : IRequest<UserModel>
{
    public string Id { get; set; }
    public GetUserQuery(string id)
    {
        Id = id;
    }
}
