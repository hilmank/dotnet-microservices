using MediatR;
using Student.Application.Commands;
using Student.Application.Dtos;
using Student.Application.GrpcService;

namespace Student.Application.Handlers;

public class SigninCommanHandler : IRequestHandler<SigninCommand, ResponseDto<SigninDto>>
{
    private readonly UserGrpcService userGrpcService;
    public SigninCommanHandler(UserGrpcService userGrpcService)
    {
        this.userGrpcService = userGrpcService;
    }

    public async Task<ResponseDto<SigninDto>> Handle(SigninCommand request, CancellationToken cancellationToken)
    {
        var retSignin =  await userGrpcService.UserSignin(request.UsernameOrEmail, request.Password);
        if(retSignin.Success){

        }
        return retSignin;
    }
}
