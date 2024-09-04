using MediatR;
using Student.Application.Dtos;

namespace Student.Application.Queries;

public class GetCourseQuery : IRequest<ResponseDto<CourseDto>>
{
    public string Id { get; set; }
}
