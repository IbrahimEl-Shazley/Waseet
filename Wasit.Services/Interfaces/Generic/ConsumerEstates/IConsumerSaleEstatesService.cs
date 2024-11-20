using Wasit.Core.Entities.SaleEstateSection;
using Wasit.Core.Enums;
using Wasit.Services.DTOs.Schema.Sale.SaleEstate;
using Wasit.Services.DTOs.Schema.Sale.UserPriceToEstate;

namespace Wasit.Services.Interfaces.Generic.ConsumerEstates
{
    public interface IConsumerSaleEstatesService : IBaseService<SaleEstate, SaleEstateDto, CreateSaleEstateDto, UpdateSaleEstateDto>
    {
        Task<ConsumerSaleEstateInfoDto> SaleEstateInfo(string userId, long estateId);
        Task<List<AddPricePackageItemDto>> ListP4AddPriceToEstate();
        Task<bool> SubscribeToAddPriceToEstatePackage(long packageId, TypePay paymentMethod);
        Task<bool> AddPriceToEstate(long estateId, double price, string userId);
        Task<long> AddPurchaseRequest(long estateId, string userId);

    }
}
