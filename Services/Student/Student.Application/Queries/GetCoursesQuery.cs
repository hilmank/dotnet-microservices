using MediatR;
using Student.Application.Dtos;

namespace Student.Application.Queries;

public class GetCoursesQuery : IRequest<IEnumerable<CourseDto>>
{

}
