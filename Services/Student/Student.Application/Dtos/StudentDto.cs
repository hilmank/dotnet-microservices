using UserApp.Application.Protos;

namespace Student.Application.Dtos;

public class StudentDto
{
    public string Id { get; set; }
    public string NickName { get; set; }
    public string PlaceOfBirth { get; set; }
    public string DateOfBirth { get; set; }
    public string Gender { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string MobileNumber { get; set; }
    public string OrgId { get; set; }
    public int Status { get; set; }
    public string LastLogin { get; set; }
    public string FullName { get; set; }
}
