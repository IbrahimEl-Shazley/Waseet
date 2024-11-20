using System.Net.Mail;

namespace Wasit.Integration.Services.Abstraction
{
    public interface IMailService
    {
        Task<bool> Send(MailMessage mailMessage);
    }
}
