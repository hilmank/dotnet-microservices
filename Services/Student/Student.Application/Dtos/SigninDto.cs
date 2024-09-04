using System;

namespace Student.Application.Dtos;

public class SigninDto
{
    public string Token { get; set; }
    public StudentDto Student { get; set; }
}
