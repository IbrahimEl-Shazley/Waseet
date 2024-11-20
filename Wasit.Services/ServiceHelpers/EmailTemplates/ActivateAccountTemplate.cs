namespace Wasit.Services.ServiceHelpers.EmailTemplates
{
    public class ActivateAccountTemplate : INotificationTemplate
    {
        public string Prepare(string htmlTemplate, dynamic input)
        {
            return htmlTemplate.Replace("{{OTP}}", input);
        }
    }
}
