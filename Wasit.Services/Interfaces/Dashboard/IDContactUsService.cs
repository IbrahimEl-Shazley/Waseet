using Wasit.Services.ViewModels.ContactUs;

namespace Wasit.Services.Interfaces.Dashboard
{
    public interface IDContactUsService
    {
        Task<List<ContactUsViewModel>> ContactUsMessages();
    }
}
