using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wasit.Core.Entities.EstateCategories;
using Wasit.Core.Entities.Shared;
using Wasit.Core.Entities.UserTables;
using Wasit.Core.Enums;

namespace Wasit.Core.Entities.EstateServiceSection
{
    public class EstateService :Entity
    { 
        public string UserName { get; set; }
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationDbUser User { get; set; }
        public string PhoneCode { get; set; }
        public string PhoneNumber{ get; set; }
        public int Code { get; set; }
        public bool ActiveCode { get; set; }
        [ForeignKey(nameof(EstateTypeId))]
        public virtual EstateType EstateType { get; set; }
        public long EstateTypeId { get; set; }
        public int EstateAge { get; set; }
        public double BudgetFrom  { get; set; }
        public double BudgetTo  { get; set; }
        public EstateServicePaymentType ServicePaymentType { get; set; }
        public EstateServicePurpose EstateServicePurpose { get; set; } 
        public double SpaceFrom { get; set; }
        public double SpaceTo { get; set; }
        public string RequiredDescription { get; set; }
        public int RoomsCount  { get; set; }
        public bool IsReviewed { get; set; }
        public long EstateRegionId { get; set; }
        [ForeignKey(nameof(EstateRegionId))]
        public virtual Region EstateRegion { get; set; }

        public bool IsWorkersHousing { get; set; }
        public string CompanyName  { get; set; }
        public string CommercialNumber   { get; set; }
        public long CompanyRegionId  { get; set; }
        [ForeignKey(nameof(CompanyRegionId))]
        public virtual Region CompanyRegion { get; set; }
        public int WorkersCount { get; set; }
        public int SupervisorsCount { get; set; }
        public string ExtraService { get; set; }

    }
}
