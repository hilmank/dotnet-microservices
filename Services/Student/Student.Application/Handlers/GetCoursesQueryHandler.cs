using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Student.Application.Queries;
using Student.Application.Dtos;
using Student.Core.Repositories;

namespace Student.Application.Handlers;

public class GetCoursesQueryHandler : IRequestHandler<GetCoursesQuery, IEnumerable<CourseDto>>
{
    private readonly IMapper _mapper;
    private readonly ILogger<GetCourseQueryHandler> _logger;
    private readonly ICourseRepository _courseRepository;
    public GetCoursesQueryHandler(IMapper mapper, ILogger<GetCourseQueryHandler> logger, ICourseRepository courseRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _courseRepository = courseRepository;
    }

    public async Task<IEnumerable<CourseDto>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
    {
        var courses = await _courseRepository.GetAll();
        if (!courses.Any()) return null!;
        return courses.Select(_mapper.Map<CourseDto>);

    }
}
