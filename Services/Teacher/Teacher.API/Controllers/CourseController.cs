using MediatR;
using Microsoft.AspNetCore.Mvc;
using Teacher.Application;
namespace Teacher.API.Controllers
{
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CourseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetCourses", Name = "GetCourses")]
        public async Task<IEnumerable<CourseDto>> GetCourses()
        {
            var query = new GetCoursesQuery();
            var retVal = _mediator.Send(query);
            return await retVal;
        }
        [HttpGet("GetCourse", Name = "GetCourse")]
        public async Task<ResponseDto<CourseDto>> GetCourse(string id)
        {
            var query = new GetCourseQuery{Id = id};
            var retVal= _mediator.Send(query);
            return await retVal;
        }
    }
}