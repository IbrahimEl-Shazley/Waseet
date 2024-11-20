using Wasit.Services.ViewModels.City;

namespace Wasit.Services.Interfaces.Dashboard
{
    public interface IDCityService
    {
        Task<CityViewModel> Cities();
        Task<(bool isSuccess, string message)> CreateCity(CreateCityViewModel model);
        Task<(bool isSuccess, string message)> RemoveCity(long id);
        Task<UpdateCityViewModel?> CityDetails(long id);
        Task<(bool isSuccess, string message)> UpdateCity(long id, UpdateCityViewModel model);
    }
}
