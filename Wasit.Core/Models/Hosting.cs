using Microsoft.AspNetCore.Hosting;

namespace Wasit.Core.Models
{
    public static class Hosting
    {
        public static IWebHostEnvironment Environment { get; set; }
        public static string EnvironmentName => Environment.EnvironmentName;
        public static string ContentRootPath => Environment.ContentRootPath;
        public static string WebRootPath => Path.Combine(Environment.ContentRootPath, "wwwroot");
    }


    public enum Environments
    {
        Production,
        Testing,
        Development,
    }
}
