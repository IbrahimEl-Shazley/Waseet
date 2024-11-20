using Microsoft.AspNetCore.Http;

namespace Wasit.Core.Models.IO
{
    public class UploadImageUsingApiDto
    {
        public IFormFile image { get; set; }
        public int fileName { get; set; }
    }
}
