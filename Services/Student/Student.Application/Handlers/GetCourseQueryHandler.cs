using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Student.Application.Queries;
using Student.Application.Dtos;
using Student.Core.Repositories;

namespace Student.Application.Handlers;

public class GetCourseQueryHandler : IRequestHandler<GetCourseQuery, ResponseDto<CourseDto>>
{
    private readonly IMapper _mapper;
    private readonly ILogger<GetCourseQueryHandler> _logger;
    private readonly ICourseRepository _courseRepository;
    public GetCourseQueryHandler(ICourseRepository courseRepository, IMapper mapper, ILogger<GetCourseQueryHandler> logger)
    {
        _courseRepository = courseRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ResponseDto<CourseDto>> Handle(GetCourseQuery request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.Get(request.Id);
        var result = _mapper.Map<CourseDto>(course);
        return new ResponseDto<CourseDto>
        {
            Success = result is not null,
            Message = result is not null ? "" : "No data found",
            Data = result
        };
    }
}
