namespace Wasit.Services.DTOs.Schema.SEC
{
    public class UserInfo
    {
        public string Message { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<string> Roles { get; set; }
        public string Token { get; set; }
       // public DateTime ExpiresOn { get; set; }
        public bool ActiveCode { get; set; }
    }
}
