using System;
using AutoMapper;
using Student.Application.Dtos;
using Student.Core.Entities;

namespace Student.Application.Mapper;

public class CourseProfile : Profile
{
    public CourseProfile()
    {
        //map entity to dto
        CreateMap<Course, CourseDto>()
        ;
    }
}
