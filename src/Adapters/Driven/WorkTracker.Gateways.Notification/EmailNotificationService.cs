using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WorkTracker.Clock.Domain.Ports;

namespace WorkTracker.Gateways.Notification;

public class EmailNotificationService : IEmailNotificationService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<EmailNotificationService> _logger;

    public EmailNotificationService(IConfiguration configuration, ILogger<EmailNotificationService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public void SendMessageAsync(string email, string subject, string message)
    {
        try {
        var client = new SmtpClient(_configuration["Mailtrap:Host"], 2525)
        {
            Credentials = new NetworkCredential(_configuration["Mailtrap:Username"], _configuration["Mailtrap:Password"]),
            EnableSsl = false
        };
        client.Send("worktracker@teste.com", email, subject, message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending email");
        }
    }
}