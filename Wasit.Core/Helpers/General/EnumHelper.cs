using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Wasit.Core.Entities.RentEstateSection;
using Wasit.Core.Entities.SaleEstateSection;
using Wasit.Core.Enums;

namespace Wasit.Core.Helpers.General
{
    public static class EnumHelper
    {

        #region Reservation Request Status
        public static string ReservationRequestStatus(ReservationStatus status, Language lang) => status switch
        {
            ReservationStatus.Current => lang == Language.Ar ? "حالي" : "Current",
            ReservationStatus.Finished => lang == Language.Ar ? "منتهي" : "Finished",
            _ => lang == Language.Ar ? "حالة غير موجودة" : "Invalid Status"
        };
        #endregion

        #region Evaluation Request Status
        public static string EvaluationRequestStatus(RatingStatus status, Language lang) => status switch
        {
            RatingStatus.New => lang == Language.Ar ? "جديد" : "New",
            RatingStatus.Current => lang == Language.Ar ? "حالي" : "Current",
            RatingStatus.Finished => lang == Language.Ar ? "منتهي" : "Finished",
            _ => lang == Language.Ar ? "حالة غير موجودة" : "Invalid Status"
        };
        #endregion

        #region Purchase Request Status
        public static string PurchaseRequestStatus(PurchaseStatus status, Language lang) => status switch
        {
            PurchaseStatus.New => lang == Language.Ar ? "جديد" : "New",
            PurchaseStatus.Current => lang == Language.Ar ? "حالي" : "Current",
            PurchaseStatus.Finished => lang == Language.Ar ? "منتهي" : "Finished",
            _ => lang == Language.Ar ? "حالة غير موجودة" : "Invalid Status"
        };


        public static string PublisherPurchaseRequestStatus(PurchaseRequest request, Language lang)
        {
            if (request.Status == PurchaseStatus.New && !request.IsAccepted)
                return lang == Language.Ar ? "قيد المراجعة" : "Waiting to be reviewed";

            if (request.IsAccepted)
                return lang == Language.Ar ? "مقبول" : "Accepted";

            if (request.Status == PurchaseStatus.Finished && !request.IsAccepted)
                return lang == Language.Ar ? "مرفوض" : "Rejected";

            return lang == Language.Ar ? "حالة غير موجودة" : "Invalid Status";
        }
        #endregion

        #region Rent Request Status
        public static string RentRequestStatus(RentRequest request, Language lang)
        {
            if (request.Status == RentStatus.New)
                return lang == Language.Ar ? "جديد" : "New";

            if (request.Status == RentStatus.Current)
                return lang == Language.Ar ? "حالي" : "Current";

            if (request.IsAccepted && request.Status == RentStatus.Finished)
                return lang == Language.Ar ? "منتهي" : "Finished";

            if (!request.IsAccepted && request.Status == RentStatus.Finished)
                return lang == Language.Ar ? "مرفوض" : "Rejected";

            return string.Empty;
        }

        public static string PublisherRentRequestStatus(RentRequest request, Language lang)
        {
            if (request.Status == RentStatus.New && !request.IsAccepted)
                return lang == Language.Ar ? "قيد المراجعة" : "Waiting to be reviewed";

            if (request.IsAccepted && !request.IsEstateRented && request.Status == RentStatus.Current)
                return lang == Language.Ar ? "مقبول" : "Accepted";

            if (request.IsAccepted && request.IsEstateRented && request.Status == RentStatus.Finished)
                return lang == Language.Ar ? "منتهي" : "Finished";

            if (!request.IsAccepted && request.Status == RentStatus.Finished)
                return lang == Language.Ar ? "مرفوض" : "Rejected";

            return lang == Language.Ar ? "حالة غير موجودة" : "Invalid Status";
        }
        #endregion

        #region Entertainment Rent Request Status
        public static string EntertainmentRentRequestStatus(DailyRentStatus status, Language lang) => status switch
        {
            DailyRentStatus.New => lang == Language.Ar ? "جديد" : "New",
            DailyRentStatus.Current => lang == Language.Ar ? "حالي" : "Current",
            DailyRentStatus.Finished => lang == Language.Ar ? "منتهي" : "Finished",
            DailyRentStatus.Canceled => lang == Language.Ar ? "ملغي" : "Canceled",
            _ => lang == Language.Ar ? "حالة غير موجودة" : "Invalid Status"
        };


        #endregion

        #region PropertiesManagement
        public static string PaymentDeadline(PaymentDeadline deadline, Language lang) => deadline switch
        {
            Enums.PaymentDeadline.FirstOfMonth => lang == Language.Ar ? "بداية الشهر" : "First of the month",
            Enums.PaymentDeadline.MiddleOfMonth => lang == Language.Ar ? "منتصف الشهر" : "Middle of the month",
            Enums.PaymentDeadline.EndOfMonth => lang == Language.Ar ? "نهاية الشهر" : "End of the month",
            _ => string.Empty
        };
        
        public static string RentManagementRequestStatus(RentalManagementOrderStatus status, Language lang) => status switch
        {
            RentalManagementOrderStatus.New => lang == Language.Ar ? "جديد" : "New",
            RentalManagementOrderStatus.Current => lang == Language.Ar ? "حالي" : "Current",
            RentalManagementOrderStatus.Finished => lang == Language.Ar ? "منتهي" : "Finished",
            _ => string.Empty
        };

        #endregion


        public static string GetEnumDisplayName<TEnum>(long enumValue) where TEnum : Enum
        {
           
            // Convert the integer to the enum value
            var enumInstance = (TEnum)Enum.ToObject(typeof(TEnum), enumValue);

            // Get the member info of the enum
            var memberInfo = enumInstance.GetType().GetMember(enumInstance.ToString());

            // Retrieve the DisplayAttribute from the member info
            var displayAttribute = memberInfo[0].GetCustomAttribute<DisplayAttribute>();

            // Return the Display name or the enum value name if no DisplayAttribute is present
            return displayAttribute != null ? displayAttribute.Name : enumInstance.ToString();
        }


        public static string UserTypeName(string type, Language lang) => type switch
        {
            nameof(UserType.Owner) => lang == Language.Ar ? "مالك العقار" : "Owner",
            nameof(UserType.Broker) => lang == Language.Ar ? "وسيط عقاري" : "Broker",
            nameof(UserType.Delegate) => lang == Language.Ar ? "مندوب عقاري" : "Delegate",
            nameof(UserType.Developer) => lang == Language.Ar ? "مطور عقاري" : "Developer",
            _ => string.Empty
        };
        
        
        public static string BrokerAccountType(AccountType? type, Language lang) => type switch
        {
            AccountType.Individual => lang == Language.Ar ? "فرد" : "Individual",
            AccountType.Facility => lang == Language.Ar ? "منشأة" : "Facility",
            _ => string.Empty
        };
        
        
        public static string BrokerFacilityType(FacilityType? type, Language lang) => type switch
        {
            FacilityType.Company => lang == Language.Ar ? "شركة" : "Company",
            FacilityType.Institution => lang == Language.Ar ? "مؤسسة" : "Institution",
            FacilityType.Office => lang == Language.Ar ? "مكتب" : "Office",
            _ => string.Empty
        };
        
        
        public static string SpecificationTypeName(SpecificationType type, Language lang) => type switch
        {
            SpecificationType.Text => lang == Language.Ar ? "نص" : "Text",
            SpecificationType.Integer => lang == Language.Ar ? "رقم صحيح" : "Integer Number",
            SpecificationType.Double => lang == Language.Ar ? "رقم عشري" : "Double Number",
            SpecificationType.Boolean => lang == Language.Ar ? "اختيار" : "Check Box",
            _ => string.Empty
        };
        
        
        public static string ContactUsSubjectName(ContactUsSubject subject, Language lang) => subject switch
        {
           ContactUsSubject.Complaint => lang == Language.Ar ? "شكوي" : "Complaint",
           ContactUsSubject.Suggestion => lang == Language.Ar ? "مقترح" : "Suggestion",
           ContactUsSubject.Other => lang == Language.Ar ? "موضوع اخر" : "Other",
            _ => string.Empty
        };



    }
}
