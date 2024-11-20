namespace Wasit.Services.ServiceHelpers.EmailTemplates
{
    public class ResetPasswordTemplate : INotificationTemplate
    {
        public string Prepare(string htmlTemplate, dynamic input)
        {
            return htmlTemplate.Replace("{{OTP}}", input);
        }
    }
}
