namespace Wasit.Services.ViewModels.Users
{
    public class DelegateViewModel : UserViewModel
    {
        public string IdentityNo { get; set; }
    }


    public class AdditionalDelegateInfoViewModel : AdditionalInfoViewModel
    {
        public string WorkingDocumentNo { get; set; }
    }
}
