namespace Wasit.Services.DTOs.Schema.SEC.Developer
{
    public class DeveloperInfoDto : UserProfileDto
    {
        public string Email { get; set; }
        public string CoverPhoto { get; set; }
        public string Description { get; set; }
    }
}
