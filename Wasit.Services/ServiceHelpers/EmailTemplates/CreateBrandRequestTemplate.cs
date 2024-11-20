namespace Wasit.Services.ServiceHelpers.EmailTemplates
{
    public class CreateBrandRequestTemplate : INotificationTemplate
    {
        public string Prepare(string htmlTemplate, dynamic input)
        {
            return htmlTemplate.Replace("{{RequestNumber}}", input.RequestNumber);
        }
    }
}
