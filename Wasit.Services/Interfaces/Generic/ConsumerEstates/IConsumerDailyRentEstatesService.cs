using Wasit.Core.Entities.DailyRentEstateSection;
using Wasit.Core.Enums;
using Wasit.Services.DTOs.Schema.DailyRent.DailyRentEstates;

namespace Wasit.Services.Interfaces.Generic.ConsumerEstates
{
    public interface IConsumerDailyRentEstatesService : IBaseService<DailyRentEstate, DailyRentEstateDto, CreateDailyRentEstateDto, UpdateDailyRentEstateDto>
    {
        Task<ConsumerDailyRentEstateInfoDto> DailyRentEstateInfo(long estateId);
        Task<bool> AddDailyRentRequest(string userId, long estateId, DateTime startDate, DateTime endDate, TypePay paymentMethod);
    }
}
