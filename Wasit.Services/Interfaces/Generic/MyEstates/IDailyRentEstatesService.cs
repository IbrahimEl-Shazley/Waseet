using Wasit.Core.Entities.DailyRentEstateSection;
using Wasit.Core.Models.DTO;
using Wasit.Services.DTOs.Schema.DailyRent.DailyRentEstates;
using Wasit.Services.DTOs.Schema.DailyRent.DailyRentRequest;
using Wasit.Services.DTOs.Schema.Shared.MyEstates;

namespace Wasit.Services.Interfaces.Generic.MyEstates
{
    public interface IDailyRentEstatesService : IBaseService<DailyRentEstate, DailyRentEstateDto, CreateDailyRentEstateDto, UpdateDailyRentEstateDto>
    {
        Task<bool> AddNewDailyRentEstate(string userId, CreateDailyRentEstateDto model);
        Task<DailyRentEstateInfoDto> MyDailyRentEstateInfo(string userId, long estateId);
        Task<bool> EditDailyRentEstate(string userId, UpdateDailyRentEstateDto model);
        Task<IEnumerable<SpecificationValueDto>> GetSpecificationValues(string userId, long estateId);
        Task<PageDTO<DailyRentRequestDto>> ListDailyRentRequests(ListDailyRentRequestsPayload model);
        Task<DailyRentRequestInfoDto> DailyRentRequestInfo(long requestId);
    }
}
