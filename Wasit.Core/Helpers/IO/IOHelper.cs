using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Wasit.Core.Models;
using Wasit.Core.Models.IO;

namespace Wasit.Core.Helpers.IO
{
    public static class IOHelper
    {
        public static string Upload(IFormFile Photo, int FileName)
        {
            string folderName = System.Enum.GetName(typeof(Enums.FileName), FileName);
            string uniqueFileName = string.Empty;
            if (Photo != null)
            {
                string uploadsFolder = Path.Combine(Hosting.WebRootPath, $"images/{folderName}");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                uniqueFileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(Photo.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fileStream);
                }
            }
            return $"images/{folderName}/" + uniqueFileName;
        }

        public static bool Remove(string filePath)
        {
            try
            {
                var absolutePath = Path.Combine(Hosting.WebRootPath, filePath);
                if (FileExists(absolutePath))
                {
                    File.Delete(absolutePath);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static void CreateDirectoryIfNotExist(string path)
        {
            try { if (!Directory.Exists(path)) Directory.CreateDirectory(path); } catch { }
        }

        public static string ReadFile(string path)
        {
            if (FileExists(path))
                return File.ReadAllText(path);
            else return string.Empty;
        }

        public static void WriteToFile(string path, string contents)
        {
            CreateFileIfNotExist(path);
            File.WriteAllText(path, contents);
        }

        public static void CreateFileIfNotExist(string path)
        {
            try { if (!File.Exists(path)) File.Create(path).Dispose(); } catch { }
        }

        public static string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private static bool FileExists(string path)
        {
            return File.Exists(path);
        }

        private static Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"},
                {".webp", "image/webp"}
            };
        }

        public static string ValidateImage(IFormFile? image)
        {
            if (image != null)
            {
                var allowedExtensions = new List<string> { ".jpg", ".png", ".jpeg" };

                if (!allowedExtensions.Contains(Path.GetExtension(image.FileName).ToLower()))
                {
                    return "غير مسموح سوي بالامتدادت التالية: JPG، PNG, JPEG";
                }

                if (image.Length > 1048576)
                {
                    return "لا يمكنك رفع صورة حجمها اكبر من 1 ميجابايت";
                }
            }
            return string.Empty;
        }
    }
}
