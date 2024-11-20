using Wasit.Services.ViewModels.Region;
using Wasit.Services.ViewModels.Shared;

namespace Wasit.Services.Interfaces.Dashboard
{
    public interface IDRegionService
    {
        Task<RegionViewModel> Regions();
        Task<List<SharedViewModel>> Cities();
        Task<(bool isSuccess, string message)> CreateRegion(CreateRegionViewModel model);
        Task<(bool isSuccess, string message)> RemoveRegion(long id);
        Task<UpdateRegionViewModel?> RegionDetails(long id);
        Task<(bool isSuccess, string message)> UpdateRegion(long id, UpdateRegionViewModel model);
    }
}
