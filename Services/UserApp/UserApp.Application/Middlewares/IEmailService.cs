using UserApp.Application.Dtos;

namespace UserApp.Application.Middlewares;
public interface IEmailService
{
    Task SendEmailAsync(EmailMessageDto message);
    Task SendHtmlEmailAsync(EmailMessageDto message);
}
