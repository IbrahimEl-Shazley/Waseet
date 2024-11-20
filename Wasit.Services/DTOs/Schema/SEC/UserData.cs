namespace Wasit.Services.DTOs.Schema.SEC
{
    public class UserData
    {
        public string Id { get; set; }
        public string User_Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string UserType { get; set; }
        public bool ActiveCode { get; set; }
    }


    public class UserData<T> where T : class
    {
        public T Profile { get; set; }
    }

}
