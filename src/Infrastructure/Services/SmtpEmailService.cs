using System.Net;
using System.Net.Mail;
using CourseBookingAppBackend.src.Application.Abstractions.Email;

namespace CourseBookingAppBackend.src.Infrastructure.Services;

public sealed class SmtpEmailService : IEmailService
{
    private readonly IConfiguration _config;

    public SmtpEmailService(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendAsync(string to, string subject, string htmlBody)
    {
        var message = new MailMessage
        {
            From = new MailAddress(_config["Email:Smtp:From"]!),
            Subject = subject,
            Body = htmlBody,
            IsBodyHtml = true
        };

        message.To.Add(to);

        var client = new SmtpClient(
            _config["Email:Smtp:Host"],
            int.Parse(_config["Email:Smtp:Port"]!)
        )
        {
            Credentials = new NetworkCredential(
                _config["Email:Smtp:Username"],
                _config["Email:Smtp:Password"]
            ),
            EnableSsl = true
        };

        await client.SendMailAsync(message);
    }
}
