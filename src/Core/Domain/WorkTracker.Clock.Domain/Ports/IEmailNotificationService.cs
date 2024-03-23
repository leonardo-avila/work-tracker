namespace WorkTracker.Clock.Domain.Ports;

public interface IEmailNotificationService {
    void SendMessageAsync(string email, string subject, string message);
}