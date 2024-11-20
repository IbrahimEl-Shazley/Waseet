using Wasit.Core.Models.DTO;

namespace Wasit.Services.DTOs.Schema.SEC
{
    public class UserProfileDto : DTO<string>
    {
        public override string Id { get; set; }
        public string ProfilePicture { get; set; }
        public string User_Name { get; set; }
        public string PhoneNumber { get; set; }
        public string UserType { get; set; }
        public string IdentityNumber { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int RegionId { get; set; }
        public string RegionName { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public string Location { get; set; }
        public string BankName { get; set; }
        public string AccountOwnerName { get; set; }
        public string AccountNumber { get; set; }
        public string Iban { get; set; }
        public bool AllowNotify { get; set; }
        public bool ActiveCode { get; set; }
        public string Token { get; set; }
    }
}
