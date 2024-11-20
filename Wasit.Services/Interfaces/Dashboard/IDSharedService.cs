using Microsoft.AspNetCore.Mvc.Rendering;
using Wasit.Core.Enums;
using Wasit.Core.Models.IO;
using Wasit.Services.ViewModels.Shared;

namespace Wasit.Services.Interfaces.Dashboard
{
    public interface IDSharedService
    {
        Task<List<SharedViewModel>> Categories();
        Task<List<SelectListItem>> Specifications();
        Task<List<SharedViewModel>> EstateTypeSpecifications(long id);
        Task<List<SharedViewModel>> Cities();
        Task<List<SharedViewModel>> RegionsByCity(long cityId);
        Task<List<SharedViewModel>> EstateTypes(CategoryType category);
        Task<string> UploadFileUsingApi(UploadImageUsingApiDto model);
    }
}
