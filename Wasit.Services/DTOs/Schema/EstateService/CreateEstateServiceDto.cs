using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wasit.Core.Entities.EstateCategories;
using Wasit.Core.Entities.UserTables;
using Wasit.Core.Enums;
using Wasit.Core.Models.DTO;

namespace Wasit.Services.DTOs.Schema.EstateService
{
    public class CreateEstateServiceDto : DTO<long>
    {
        public string UserName { get; set; }
        public string UserId { get; set; }  
        public string PhoneCode { get; set; }
        public string PhoneNumber { get; set; }
        public int Code { get; set; }
        public bool ActiveCode { get; set; }
        public long EstateType { get; set; }
        public int EstateAge { get; set; }
        public double BudgetFrom { get; set; }
        public double BudgetTo { get; set; }
        public EstateServicePaymentType ServicePaymentType { get; set; }
        public EstateServicePurpose EstateServicePurpose { get; set; }
        public double SpaceFrom { get; set; }
        public double SpaceTo { get; set; }
        public string RequiredDescription { get; set; }
        public int RoomsCount { get; set; }
        public bool IsReviewed { get; set; }
        public long EstateRegion { get; set; }
      
        
        public bool IsWorkersHousing { get; set; }
        public string CompanyName { get; set; }
        public string CommercialNumber { get; set; }
        public long CompanyRegion { get; set; }
        public int WorkersCount { get; set; }
        public int SupervisorsCount { get; set; }
        public string ExtraService { get; set; }
    }
}
