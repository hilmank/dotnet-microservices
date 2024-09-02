using MediatR;
using UserApp.Application.Protos;

namespace UserApp.Application.Commands;

public class UserUpdateCommand : IRequest<UserUpdateResponse>
{
    public string Id { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public string? MobileNumber { get; set; }
    public string? Orgid { get; set; }
    public string UpdatedBy { get; set; }
    public UserProfilePictureModel? ProfilePicture { get; set; }
}
