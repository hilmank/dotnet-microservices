using System;
using MediatR;
using Student.Application.Dtos;

namespace Student.Application.Queries;

public class GetStudentQuery : IRequest<ResponseDto<SigninDto>>
{
    public string UsernameOrEmail { get; set; }
}
