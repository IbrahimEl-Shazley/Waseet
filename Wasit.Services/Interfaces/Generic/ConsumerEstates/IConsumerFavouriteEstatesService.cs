using Wasit.Core.Enums;

namespace Wasit.Services.Interfaces.Generic.ConsumerEstates
{
    public interface IConsumerFavouriteEstatesService : IBaseService
    {
        Task<dynamic> ListFavouriteEstates(string userId, CategoryType category, long? estateTypeId, int pageNumber);
    }
}
