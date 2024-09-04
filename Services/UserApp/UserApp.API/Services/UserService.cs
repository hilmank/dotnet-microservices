using AutoMapper;
using Grpc.Core;
using MediatR;
using UserApp.Application.Commands;
using UserApp.Application.Protos;
using UserApp.Application.Queries;
using Microsoft.AspNetCore.Authorization;
using UserApp.Core.Entities;
using Infrastructure.Common.Constants;
using Microsoft.Extensions.Localization;
using Infrastructure.Common.Exceptions;
using UserApp.Application.Resources;
namespace UserApp.API.Services;

public class UserService : UserProtoService.UserProtoServiceBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IStringLocalizer<ErrorsResource> _errorLocalizer;
    private readonly IStringLocalizer<MessagesResource> _messageLocalizer;


    public UserService(IMapper mapper, IMediator mediator, IHttpContextAccessor httpContextAccessor, IStringLocalizer<ErrorsResource> errorLocalizer, IStringLocalizer<MessagesResource> messageLocalizer)
    {
        _mapper = mapper;
        _mediator = mediator;
        _httpContextAccessor = httpContextAccessor;
        _errorLocalizer = errorLocalizer;
        _messageLocalizer = messageLocalizer;
    }
    protected User UserInfo()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        var ret = httpContext!.Items["User"];
        var result = ret as User;
        result!.Password = string.Empty;
        return result;
    }
    public static T CreateErrorResponse<T>(Exception exception, string message) where T : new()
    {
        var response = new T();
        var properties = typeof(T).GetProperties();

        foreach (var prop in properties)
        {
            if (prop.Name == "Success" && prop.PropertyType == typeof(bool))
            {
                prop.SetValue(response, false);
            }
            else if (prop.Name == "Message" && prop.PropertyType == typeof(string))
            {
                prop.SetValue(response, message);
            }
            else if (prop.Name == "MessageDetail" && prop.PropertyType == typeof(UserApp.Application.Protos.CustomServiceFault))
            {
                var fault = new UserApp.Application.Protos.CustomServiceFault
                {
                    ErrorMessage = exception.Message,
                    Source = exception.Source,
                    StackTrace = exception.StackTrace,
                    Target = exception.TargetSite?.ToString(),
                    InnerExceptionMessage = exception.GetBaseException().ToString()
                };
                prop.SetValue(response, fault);
            }
        }

        return response;
    }
    [Authorize]
    public override async Task<UserListResponse> GetUsers(EmptyRequest request, ServerCallContext context)
    {
        try
        {
            var query = new GetUsersQuery();
            var result = await _mediator.Send(query);
            if (result == null)
                return new UserListResponse
                {
                    Success = true,
                    Message = _errorLocalizer["Error.Common.NotFound"]
                };
            var retVal = new UserListResponse
            {
                Success = true
            };
            retVal.Users.Add(result);
            return retVal;
        }
        catch (System.Exception exception)
        {
            return CreateErrorResponse<UserListResponse>(exception, _errorLocalizer["Error.Common.Failed"]);
        }
    }
    [Authorize]
    public override async Task<UserResponse> GetUser(GetUserRequest request, ServerCallContext context)
    {
        try
        {
            var user = UserInfo();
            var query = new GetUserQuery(request.Id);
            var result = await _mediator.Send(query);
            if (result == null)
                return new UserResponse
                {
                    Success = false,
                    Message = _errorLocalizer["Error.Common.NotFound"]
                };
            return new UserResponse
            {
                Success = true,
                User = result
            };
        }
        catch (System.Exception exception)
        {
            return CreateErrorResponse<UserResponse>(exception, _errorLocalizer["Error.Common.Failed"]);
        }
    }
    [Authorize]
    public override async Task<UserCreateResponse> UserCreate(UserCreateRequest request, ServerCallContext context)
    {
        try
        {
            var command = _mapper.Map<UserCreateCommand>(request);
            return await _mediator.Send(command);
        }
        catch (System.Exception exception)
        {
            return CreateErrorResponse<UserCreateResponse>(exception, _errorLocalizer["Error.Common.Failed"]);
        }
    }
    [Authorize]
    public override async Task<UserUpdateResponse> UserUpdate(UserUpdateRequest request, ServerCallContext context)
    {
        try
        {
            var command = _mapper.Map<UserUpdateCommand>(request);
            return await _mediator.Send(command);
        }
        catch (System.Exception exception)
        {
            return CreateErrorResponse<UserUpdateResponse>(exception, _errorLocalizer["Error.Common.Failed"]);
        }
    }
    [Authorize]
    public override async Task<UserDeleteResponse> UserDelete(UserDeleteRequest request, ServerCallContext context)
    {
        try
        {
            var command = new UserDeleteCommand { Id = request.Id, UserId = UserInfo().Id };
            return await _mediator.Send(command);
        }
        catch (System.Exception exception)
        {
            return CreateErrorResponse<UserDeleteResponse>(exception, _errorLocalizer["Error.Common.Failed"]);
        }
    }
    public override async Task<UserSigninResponse> UserSignin(UserSigninRequest request, ServerCallContext context)
    {
        try
        {
            var command = new UserSigninCommand
            {
                UsernameOrEmail = request.UsernameOrEmail,
                Password = request.Password
            };
            var retVal = await _mediator.Send(command);
            return retVal;
        }
        catch (System.Exception exception)
        {
            return CreateErrorResponse<UserSigninResponse>(exception, _errorLocalizer["Error.Common.Failed"]);
        }
    }
    [Authorize]
    public override async Task<UserUpdateResponse> UserChangePassword(UserChangePasswordRequest request, ServerCallContext context)
    {
        try
        {
            var command = new UserChangePasswordCommand
            {
                UsernameOrEmail = request.UsernameOrEmail,
                Password = request.Password,
                NewPassword = request.NewPassword,
                ConfirmPassword = request.ConfirmPassword
            };
            var retVal = await _mediator.Send(command);
            return retVal;
        }
        catch (System.Exception exception)
        {
            return CreateErrorResponse<UserUpdateResponse>(exception, _errorLocalizer["Error.Common.Failed"]);
        }
    }
    [Authorize]
    public override async Task<UserUpdateResponse> UserActivate(GetUserRequest request, ServerCallContext context)
    {
        try
        {
            var command = new UserUpdateStatusCommand
            {
                Id = request.Id,
                UpdatedBy = UserInfo().Id,
                Status = StatusDataConstant.Active
            };
            var retVal = await _mediator.Send(command);
            return retVal;
        }
        catch (System.Exception exception)
        {
            return CreateErrorResponse<UserUpdateResponse>(exception, _errorLocalizer["Error.Common.Failed"]);
        }
    }
    [Authorize]
    public override async Task<UserUpdateResponse> UserDeActivate(GetUserRequest request, ServerCallContext context)
    {
        try
        {
            var command = new UserUpdateStatusCommand
            {
                Id = request.Id,
                UpdatedBy = UserInfo().Id,
                Status = StatusDataConstant.NotActive
            };
            var retVal = await _mediator.Send(command);
            return retVal;
        }
        catch (System.Exception exception)
        {
            return CreateErrorResponse<UserUpdateResponse>(exception, _errorLocalizer["Error.Common.Failed"]);
        }
    }
    [Authorize]
    public override async Task<UserUpdateResponse> UserResetPassword(GetUserRequest request, ServerCallContext context)
    {
        try
        {
            var command = new UserResetPasswordCommand
            {
                Id = request.Id,
                UpdatedBy = UserInfo().Id
            };
            var retVal = await _mediator.Send(command);
            return retVal;
        }
        catch (System.Exception exception)
        {
            return CreateErrorResponse<UserUpdateResponse>(exception, _errorLocalizer["Error.Common.Failed"]);
        }
    }
}
