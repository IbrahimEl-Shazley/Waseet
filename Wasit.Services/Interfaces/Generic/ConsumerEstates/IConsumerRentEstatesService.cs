using Wasit.Core.Entities.RentEstateSection;
using Wasit.Services.DTOs.Schema.Rent.RentEstate;

namespace Wasit.Services.Interfaces.Generic.ConsumerEstates
{
    public interface IConsumerRentEstatesService : IBaseService<RentEstate, RentEstateDto, CreateRentEstateDto, UpdateRentEstateDto>
    {
        Task<ConsumerRentEstateInfoDto> RentEstateInfo(long estateId);
        Task<bool> AddRentRequest(string userId, long estateId, DateTime startDate, int monthCount);
    }
}
