using Wasit.Core.Entities.EntertainmentEstateSection;
using Wasit.Core.Models.DTO;
using Wasit.Services.DTOs.Schema.Entertainment.EntertainmentEstate;
using Wasit.Services.DTOs.Schema.Entertainment.EntertainmentRentRequest;
using Wasit.Services.DTOs.Schema.Shared.MyEstates;

namespace Wasit.Services.Interfaces.Generic.MyEstates
{
    public interface IEntertainmentEstatesService : IBaseService<EntertainmentEstate, EntertainmentEstateDto, CreateEntertainmentEstateDto, UpdateEntertainmentEstateDto>
    {
        Task<bool> AddNewEntertainmentEstate(string userId, CreateEntertainmentEstateDto model);
        Task<EntertainmentEstateInfoDto> MyEntertainmentEstateInfo(string userId, long estateId);
        Task<IEnumerable<SpecificationValueDto>> GetSpecificationValues(string userId, long estateId);
        Task<bool> EditEntertainmentEstate(string userId, UpdateEntertainmentEstateDto model);
        Task<PageDTO<EntertainmentRentRequestDto>> ListEntertainmentRentRequests(ListEntertainmentRentRequestPayload model);
        Task<EntertainmentRentRequestInfoDto> EntertainmentRentRequestInfo(long requestId);
    }
}
