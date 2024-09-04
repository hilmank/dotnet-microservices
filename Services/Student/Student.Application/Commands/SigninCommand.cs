using System;
using MediatR;
using Student.Application.Dtos;

namespace Student.Application.Commands;

public class SigninCommand : IRequest<ResponseDto<SigninDto>>
{
    public string UsernameOrEmail { get; set; }
    public string Password { get; set; }
}
