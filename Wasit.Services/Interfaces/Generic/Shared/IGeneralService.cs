using Wasit.Core.Enums;
using Wasit.Core.Models.IO;

namespace Wasit.Services.Interfaces.Generic.Shared
{
    public interface IGeneralService : IBaseService
    {
        Task<string> TermsAndConditions(Language lang, string userType);
        Task<string> AboutUs(Language lang);
        Task<double> CalcAverageUserRating(string userId);
        Task<double> CalcAverageEstateRating(long estateId);
    }
}
