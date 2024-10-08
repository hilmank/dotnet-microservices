using AutoMapper;
using Infrastructure.Common.Exceptions;
using Infrastructure.Common.Extensions;
using Infrastructure.Common.UserPreferences;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using UserApp.Application.Commands;
using UserApp.Application.Dtos;
using UserApp.Application.Middlewares;
using UserApp.Application.Protos;
using UserApp.Application.Resources;
using UserApp.Application.Validators;
using UserApp.Core.Repositories;

namespace UserApp.Application.Handlers;

public class UserChangePasswordCommandHandler : IRequestHandler<UserChangePasswordCommand, UserUpdateResponse>
{
    private readonly ILogger<UserUpdateCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IStringLocalizer<ErrorsResource> _errorLocalizer;
    private readonly IStringLocalizer<MessagesResource> _messageLocalizer;
    private readonly UserPreferences _userPreferences;
    private readonly IEmailService _emailService;
    private readonly IStringLocalizer<EmailMessagesResource> _emailMessageLocalizer;
    public UserChangePasswordCommandHandler(ILogger<UserUpdateCommandHandler> logger, IUserRepository userRepository, IMapper mapper, IStringLocalizer<ErrorsResource> errorLocalizer, IStringLocalizer<MessagesResource> messageLocalizer, UserPreferences userPreferences, IEmailService emailService, IStringLocalizer<EmailMessagesResource> emailMessageLocalizer)
    {
        _logger = logger;
        _userRepository = userRepository;
        _mapper = mapper;
        _errorLocalizer = errorLocalizer;
        _messageLocalizer = messageLocalizer;
        _userPreferences = userPreferences;
        _emailService = emailService;
        _emailMessageLocalizer = emailMessageLocalizer;
    }

    public async Task<UserUpdateResponse> Handle(UserChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var validator = new UserChangePasswordValidator();
        var validatorResult = validator.Validate(request);
        if (!validatorResult.IsValid)
            return new UserUpdateResponse { Success = false, Message = string.Join("; ", validatorResult.Errors.Select(error => error.ErrorMessage).ToList()) };

        var user = await _userRepository.GetByUsernameOrEmail(request.UsernameOrEmail);
        if (user == null)
            return new UserUpdateResponse { Success = false, Message = _errorLocalizer["Error.Common.NotFound"] };
        if (user.Password.DecryptString() != request.Password)
            return new UserUpdateResponse { Success = false, Message = _messageLocalizer["User.Password.Wrong"] };
        user.Password = request.NewPassword.EncryptString();
        user.UpdatedBy = user.Id;
        user.UpdatedDate = DateTime.Now;
        var ret = await _userRepository.Update(user, result =>
        {
            if (result is not null)
            {
                string strBody = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + $"/Resources/Templates/Email/HeaderEmail-{_userPreferences.LanguageId}.html");
                strBody += System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + $"/Resources/Templates/Email/ChangePassword-{_userPreferences.LanguageId}.html");
                strBody += System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + $"/Resources/Templates/Email/FooterEmail-{_userPreferences.LanguageId}.html");

                strBody = strBody.Replace("[UserName]", result.FirstName);
                strBody = strBody.Replace("[UserEmail]", result.Email);
                strBody = strBody.Replace("[Date]", ((DateTime)result.UpdatedDate!).ToString());

                EmailMessageDto message = new()
                {
                    To = [user.Email],
                    Cc = [],
                    Subject = _emailMessageLocalizer["Subject.ChangePassword"],
                    Attachments = [],
                    Content = strBody
                };
                _emailService.SendHtmlEmailAsync(message);
            }
        });
        return new UserUpdateResponse { Success = ret };
    }
}
