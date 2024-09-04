using System;
using AutoMapper;
using Student.Application.Dtos;
using UserApp.Application.Protos;

namespace Student.Application.Mapper;

public class StudentProfile : Profile
{
    protected StudentProfile()
    {
        CreateMap<UserModel, StudentDto>()
        ;
    }
}
