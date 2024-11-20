using Wasit.Core.Entities.UserTables;
using Wasit.Core.Enums;

namespace Wasit.Service.Interfaces.General
{

    public interface ICurrentUserService 
    {
        string UserId { get; }
        Language Language { get; }
        bool IsArabic { get; }
    }
}
