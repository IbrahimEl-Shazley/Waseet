using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Wasit.Context;
using Wasit.Core.Enums;
using Wasit.Core.Helpers;
using Wasit.Core.Models;
using Wasit.Core.Models.IO;
using Wasit.Services.Interfaces.Dashboard;
using Wasit.Services.ViewModels.EstateSettings;
using Wasit.Services.ViewModels.Shared;

namespace Wasit.Services.Implementations.Dashboard
{
    public class DSharedService : IDSharedService
    {
        private readonly ApplicationDbContext _context;

        public DSharedService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<List<SharedViewModel>> Categories()
        {
            return await _context.Categories
                .Where(x => x.IsActive)
                .AsNoTracking()
                .Select(x => new SharedViewModel
                {
                    Id = x.Id,
                    Name = x.NameAr
                }).ToListAsync();
        }

        public async Task<List<SelectListItem>> Specifications()
        {
            var data= await _context.Specifications
                .Where(x => x.IsActive)
                .AsNoTracking()
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.NameAr
                }).ToListAsync();
            return data;
        }

        public async Task<List<SharedViewModel>> EstateTypeSpecifications(long id)
        {
            return await _context.EstateTypeSpecifications
                .Where(x => x.EstateTypeId == id && x.Specification.IsActive)
                .AsNoTracking()
                .Select(x => new SharedViewModel
                {
                    Id = x.SpecificationId,
                    Name = x.Specification.NameAr
                }).ToListAsync();
        }


        public async Task<List<SharedViewModel>> Cities()
        {
            return await _context.Cities
                .Where(x => x.IsActive)
                .AsNoTracking()
                .Select(x => new SharedViewModel
                {
                    Id = x.Id,
                    Name = x.NameAr
                }).ToListAsync();
        }


        public async Task<List<SharedViewModel>> RegionsByCity(long cityId)
        {
            return await _context.Regions
                .Where(x => x.IsActive && x.CityId == cityId)
                .AsNoTracking()
                .Select(x => new SharedViewModel
                {
                    Id = x.Id,
                    Name = x.NameAr
                }).ToListAsync();
        }

        public async Task<List<SharedViewModel>> EstateTypes(CategoryType category)
        {
            var data = await _context.CategoryEstateTypes
                .Where(x => x.Category.Type == category)
                .Include(x => x.EstateType)
                .AsNoTracking()
                .Select(x => new SharedViewModel
                {
                    Id = x.EstateTypeId,
                    Name = x.EstateType.NameAr
                }).ToListAsync();

            return data;
        }



        public async Task<string> UploadFileUsingApi(UploadImageUsingApiDto model)
        {
            var apiUrl = $"{MyConstants.DomainUrl}api/General/UploadImage";
            //var apiUrl = $"https://localhost:44357/api/General/UploadImage";
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var formData = new MultipartFormDataContent();

                    // Use the stream without explicitly disposing it
                    var fileContent = new StreamContent(model.image.OpenReadStream());
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue("multipart/form-data");
                    formData.Add(fileContent, "image", model.image.FileName);

                    formData.Add(new StringContent(model.fileName.ToString()), "fileName");

                    var response = await httpClient.PostAsync($"{apiUrl}", formData);

                    string jsonString = await response.Content.ReadAsStringAsync();
                    var responseObject = JsonConvert.DeserializeAnonymousType(jsonString, new { data = "" });
                    return responseObject.data;

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
