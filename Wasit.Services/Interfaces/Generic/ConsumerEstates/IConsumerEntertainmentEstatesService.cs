using Wasit.Core.Entities.EntertainmentEstateSection;
using Wasit.Core.Enums;
using Wasit.Services.DTOs.Schema.Entertainment.EntertainmentEstate;

namespace Wasit.Services.Interfaces.Generic.ConsumerEstates
{
    public interface IConsumerEntertainmentEstatesService : IBaseService<EntertainmentEstate, EntertainmentEstateDto, CreateEntertainmentEstateDto, UpdateEntertainmentEstateDto>
    {
        Task<ConsumerEntertainmentEstateInfoDto> EntertainmentEstateInfo(long estateId);
        Task<bool> AddEntertainmentRentRequest(string userId, long estateId, DateTime startDate, DateTime endDate, TypePay paymentMethod);
    }
}
