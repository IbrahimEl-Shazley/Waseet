using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wasit.Core.Entities.EstateServiceSection;
using Wasit.Services.DTOs.Schema.EstateService;
using Wasit.Services.ViewModels.Profits.AllPackages;

namespace Wasit.Services.Interfaces.Generic.EstateServiceSection
{
    public interface IEstateServiceService :IBaseService
    {
        Task<IEnumerable<EstateServicePackagesDto>> ServicesPackages();

    }
}
