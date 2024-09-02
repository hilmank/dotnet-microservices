using MediatR;
using Microsoft.AspNetCore.Http;
using UserApp.Application.Protos;

namespace UserApp.Application.Commands;

public class UserCreateCommand : IRequest<UserCreateResponse>
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string MobileNumber { get; set; }
    public string CreatedBy { get; set; }
    public UserProfilePictureModel ProfilePicture { get; set; }

}
