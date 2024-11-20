using System.ComponentModel.DataAnnotations;

namespace Wasit.Core.Entities
{
    public class AppImg
    {
        [Key]
        public int Id { get; set; }
        public string Img { get; set; }
        public bool IsActive { get; set; }

    }
}
