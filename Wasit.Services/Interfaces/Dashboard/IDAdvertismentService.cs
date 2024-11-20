using Microsoft.AspNetCore.Http;
using Wasit.Services.ViewModels.Advertisments;

namespace Wasit.Services.Interfaces.Dashboard
{
    public interface IDAdvertismentService
    {
        Task<List<AdvertismentViewModel>> Advertisments();
        Task<(bool isSuccess, string message)> CreateAdvertisment(CreateAdvertismentViewModel model);
        Task<(bool isSuccess, string message)> Remove(long id);
    }
}
