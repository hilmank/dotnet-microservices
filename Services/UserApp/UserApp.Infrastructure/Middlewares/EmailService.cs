using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using UserApp.Application.Dtos;
using UserApp.Application.Middlewares;
using UserApp.Application.Settings;

namespace UserApp.Infrastructure.Middlewares;

public class EmailService : IEmailService
{
        public async Task SendEmailAsync(EmailMessageDto message)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(SmtpSettings.EmailSender, SmtpSettings.Username));
            for (int i = 0; i < message.To.Count; i++)
            {
                mimeMessage.To.Add(new MailboxAddress(message.Title, message.To[i]));
            }
            for (int i = 0; i < message.Cc.Count; i++)
            {
                mimeMessage.Cc.Add(new MailboxAddress(message.Title, message.Cc[i]));
            }

            mimeMessage.Subject = message.Subject;

        var builder = new BodyBuilder
        {
            TextBody = message.Content
        };
        if (message.Attachments != null)
            {
                foreach (var attacthment in message.Attachments)
                {
                    builder.Attachments.Add(attacthment);
                }
            }
            mimeMessage.Body = builder.ToMessageBody();
            try
            {
                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    await client.ConnectAsync(SmtpSettings.Host, SmtpSettings.Port, SecureSocketOptions.Auto);
                    await client.AuthenticateAsync(SmtpSettings.Username, SmtpSettings.Password);
                    client.AuthenticationMechanisms.Remove("XOAUTH");
                    await client.SendAsync(mimeMessage);
                    await client.DisconnectAsync(true);

                }
            }
            catch (Exception ex)
            {
                var e = ex;
                throw;
            }
        }

        public async Task SendHtmlEmailAsync(EmailMessageDto message)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(SmtpSettings.EmailSender, SmtpSettings.Username));
            for (int i = 0; i < message.To.Count; i++)
            {
                mimeMessage.To.Add(new MailboxAddress(message.Title, message.To[i]));
            }
            for (int i = 0; i < message.Cc.Count; i++)
            {
                mimeMessage.Cc.Add(new MailboxAddress(message.Title, message.Cc[i]));
            }
            mimeMessage.Subject = message.Subject;

            var builder = new BodyBuilder();
            builder.HtmlBody = EmailProperty.ReplaceFooterContent(builder, message.Content);

            if (message.Attachments != null)
            {
                foreach (var attacthment in message.Attachments)
                {
                    builder.Attachments.Add(attacthment);
                }
            }

            mimeMessage.Body = builder.ToMessageBody();
            try
            {
                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    await client.ConnectAsync(SmtpSettings.Host, SmtpSettings.Port, SecureSocketOptions.Auto);
                    await client.AuthenticateAsync(SmtpSettings.Username, SmtpSettings.Password);
                    client.AuthenticationMechanisms.Remove("XOAUTH");
                    await client.SendAsync(mimeMessage);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                var e = ex;
                throw;
            }
        }

}
