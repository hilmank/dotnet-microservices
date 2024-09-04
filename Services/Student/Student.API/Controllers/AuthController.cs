using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Student.Application.Commands;
using Student.Application.Dtos;

namespace Student.API.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Signin", Name = "Signin")]
        public async Task<ResponseDto<string>> Login(string usernameOrEmail, string password)
        {
            SigninCommand command = new()
            {
                UsernameOrEmail = usernameOrEmail,
                Password = password
            };
            var retVal =  await _mediator.Send(command);
            return new ResponseDto<string>{
                Success=true,
                Data = retVal?.Data?.Token
            };
        }
    }
}
