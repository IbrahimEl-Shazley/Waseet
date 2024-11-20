using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using Wasit.Integration.Services.Abstraction;

namespace Wasit.Integration.Services.Implementation
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task<bool> Send(MailMessage mailMessage)
        {
            return await SmtpSendEmail(mailMessage);
        }

        #region private
        private async Task<bool> SmtpSendEmail(MailMessage mailMessage)
        {
            try
            {
                mailMessage.IsBodyHtml = true;
                bool useSmtp = bool.Parse(_configuration["Email:UseSmtp"]);
                if (useSmtp)
                {
                    SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new System.Net.NetworkCredential(mailMessage.From.ToString(), _configuration["Email:AppPassword"]);
                    client.EnableSsl = true;
                    await client.SendMailAsync(mailMessage);
                }
                else
                {
                    SmtpClient client = new SmtpClient("127.0.0.1", 25);
                    client.Credentials = new System.Net.NetworkCredential("", "");
                    await client.SendMailAsync(mailMessage);
                }
                return true;
            }
            catch (Exception ex)
            {
                 return false;
            }
        }
        #endregion
    }
}
