using Wasit.Core.Entities.DailyRentEstateSection;
using Wasit.Core.Entities.EntertainmentEstateSection;

namespace Wasit.Core.ExtensionsMethods
{
    public static partial class ExtensionMethods
    {
        public static bool IsDatesInRange(this IEnumerable<DailyRentRequest> collection, DateTime startDate, DateTime endDate) 
            => collection.Any(x => (x.ArrivalDate <= endDate && x.ArrivalDate >= startDate) || (x.LeaveDate <= endDate && x.LeaveDate >= startDate));

        public static bool IsDatesInRange(this DailyRentEstate estate, DateTime startDate, DateTime endDate)
            => startDate >= estate.BookingFrom && endDate <= estate.BookingTo;

        public static bool IsDatesInRange(this IEnumerable<EntertainmentRequest> collection, DateTime startDate, DateTime endDate) 
            => collection.Any(x => (x.ArrivalDate <= endDate && x.ArrivalDate >= startDate) || (x.LeaveDate <= endDate && x.LeaveDate >= startDate));

        public static bool IsDatesInRange(this EntertainmentEstate estate, DateTime startDate, DateTime endDate)
            => startDate >= estate.BookingFrom && endDate <= estate.BookingTo;

    }
}
