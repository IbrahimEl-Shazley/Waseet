namespace Wasit.Services.ViewModels.Users
{
    public class DeveloperViewModel : UserViewModel
    {

    }


    public class AdditionalDeveloperInfoViewModel : AdditionalInfoViewModel
    {
        public string? Email { get; set; }
        public string? CoverPhoto { get; set; }
        public string? Description { get; set; }
    }
}   
