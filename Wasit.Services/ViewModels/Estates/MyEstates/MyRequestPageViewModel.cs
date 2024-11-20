namespace Wasit.Services.ViewModels.Estates.MyEstates
{
    public class MyRequestPageViewModel
    {
        public long EstateId { get; set; }
        public List<MyRequestViewModel> Requests { get; set; } = [];
    }
}
