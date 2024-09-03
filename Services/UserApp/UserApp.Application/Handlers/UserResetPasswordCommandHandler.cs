using AutoMapper;
using Infrastructure.Common.Constants;
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
using UserApp.Core.Repositories;

namespace UserApp.Application.Handlers;

public class UserResetPasswordCommandHandler : IRequestHandler<UserResetPasswordCommand, UserUpdateResponse>
{
    private readonly ILogger<UserUpdateCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IStringLocalizer<ErrorsResource> _errorLocalizer;
    private readonly IStringLocalizer<MessagesResource> _messageLocalizer;
    private readonly IStringLocalizer<EmailMessagesResource> _emailMessageLocalizer;
    private readonly IEmailService _emailService;
    private readonly UserPreferences _userPreferences;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public UserResetPasswordCommandHandler(ILogger<UserUpdateCommandHandler> logger, IUserRepository userRepository, IMapper mapper, IStringLocalizer<ErrorsResource> errorLocalizer, IStringLocalizer<MessagesResource> messageLocalizer, IStringLocalizer<EmailMessagesResource> emailMessageLocalizer, IEmailService emailService, UserPreferences userPreferences, IJwtTokenGenerator jwtTokenGenerator)
    {
        _logger = logger;
        _userRepository = userRepository;
        _mapper = mapper;
        _errorLocalizer = errorLocalizer;
        _messageLocalizer = messageLocalizer;
        _emailMessageLocalizer = emailMessageLocalizer;
        _emailService = emailService;
        _userPreferences = userPreferences;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<UserUpdateResponse> Handle(UserResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.Get(request.Id);
        if (user == null)
            return new UserUpdateResponse { Success = false, Message = _errorLocalizer["Error.Common.NotFound"] };
        user.Password = _jwtTokenGenerator.GeneratePassword().EncryptString();
        user.UpdatedBy = request.UpdatedBy;
        user.UpdatedDate = DateTime.Now;
        var ret = await _userRepository.Update(user, result =>
        {
            if (result is not null)
            {
                string strBody = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + $"/Resources/Templates/Email/HeaderEmail-{_userPreferences.LanguageId}.html");
                strBody += System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + $"/Resources/Templates/Email/ResetPassword-{_userPreferences.LanguageId}.html");
                strBody += System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + $"/Resources/Templates/Email/FooterEmail-{_userPreferences.LanguageId}.html");

                strBody = strBody.Replace("[UserName]", result.FirstName);
                strBody = strBody.Replace("[UserEmail]", result.Email);
                strBody = strBody.Replace("[Password]", result.Password.DecryptString());
                strBody = strBody.Replace("[Date]", ((DateTime)result.UpdatedDate!).ToString());

                EmailMessageDto message = new()
                {
                    To = [user.Email],
                    Cc = [],
                    Subject = _emailMessageLocalizer["Subject.ReserPassword"],
                    Attachments = [],
                    Content = strBody
                };
                _emailService.SendHtmlEmailAsync(message);
            }
        });
        return new UserUpdateResponse { Success = ret };
    }

}
