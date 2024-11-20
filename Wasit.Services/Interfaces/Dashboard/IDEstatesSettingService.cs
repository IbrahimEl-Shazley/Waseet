using Wasit.Services.ViewModels.EstateSettings;
using Wasit.Services.ViewModels.Shared;

namespace Wasit.Services.Interfaces.Dashboard
{
    public interface IDEstatesSettingService
    {
        Task<List<EstateTypeViewModel>> EstateTypes(long categoryId);
        Task<(bool isSuccess, string message)> CreateEstateType(CreateEstateTypeViewModel model);
        Task<(bool isSuccess, string message)> EditEstateType(long id, UpdateEstateTypeViewModel model);
        Task<UpdateEstateTypeViewModel?> EstateTypeById(long id);
        Task<EstateTypeSpecificationsPageViewModel?> EstateTypeSpecifications(long estateTypeId);
        Task<List<SpecificationViewModel>> Specifications();
        Task<List<SharedViewModel>> SpecificationTypes();
        Task<EditSpecificationViewModel?> GetSpecificationDetails(long id);
        Task<(bool isSuccess, string message)> CreateSpecification(CreateSpecificationViewModel model);
        Task<(bool isSuccess, string message)> EditSpecification(long id, EditSpecificationViewModel model);
        Task<(bool isSuccess, string message)> Remove(long estateTypeId);
        Task<(bool isSuccess, string message)> RemoveSpecification(long id);
    }
}
