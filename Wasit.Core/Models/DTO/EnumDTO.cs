namespace Wasit.Core.Models.DTO
{
    public class EnumDTO : DTO<int?>
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
