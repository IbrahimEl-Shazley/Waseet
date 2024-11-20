using Wasit.Core.Enums;
using Wasit.Services.DTOs.Schema.PropertiesManagement.PayApartmentRent;

namespace Wasit.Services.Interfaces.Generic.PropertiesManagement
{
    public interface IPayApartmentRentService : IBaseService
    {
        Task<RentPayerApartmentInfoDto> GetApartmentInfo(int apartmentNo);
        Task<bool> PayRent(string userId, long apartmentId, TypePay paymentMethod);
    }
}
