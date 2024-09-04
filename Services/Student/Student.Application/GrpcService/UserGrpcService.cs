using AutoMapper;
using Grpc.Core;
using Student.Application.Dtos;
using UserApp.Application.Protos;
namespace Student.Application.GrpcService
{
    public class UserGrpcService
    {
        private readonly UserProtoService.UserProtoServiceClient _userProtoServiceClient;
        private readonly IMapper _mapper;
        public UserGrpcService(UserProtoService.UserProtoServiceClient userProtoServiceClient, IMapper mapper)
        {
            _userProtoServiceClient = userProtoServiceClient;
            _mapper = mapper;
        }

        public async Task<ResponseDto<SigninDto>> UserSignin(string usernameOrEmail, string password)
        {
            var userLoginReguest = new UserSigninRequest { UsernameOrEmail = usernameOrEmail, Password = password }; ;
            var resLogin = await _userProtoServiceClient.UserSigninAsync(userLoginReguest);
            if (!resLogin.Success)
                return new ResponseDto<SigninDto>
                {
                    Success = resLogin.Success,
                    Message = resLogin.Message,
                };
            var result = new ResponseDto<SigninDto>
            {
                Success = true,
                Data = new SigninDto
                {
                    Token = resLogin.Token,
                    Student = new()
                }
            };
            var headers = new Metadata
            {
                { "Authorization", $"Bearer {resLogin.Token}" }
            };
            var userRequest = new GetUserRequest { Id = usernameOrEmail };
            var user = await _userProtoServiceClient.GetUserAsync(userRequest, headers);
            result.Data.Student = _mapper.Map<StudentDto>(user.User);
            return result;
        }
    }
}
