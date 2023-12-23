using Microsoft.AspNetCore.Identity.UI.Services;

namespace WebApplication7.Utility
{
    public class DummyEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Dummy implementation that does nothing
            return Task.CompletedTask;
        }
    }
}
