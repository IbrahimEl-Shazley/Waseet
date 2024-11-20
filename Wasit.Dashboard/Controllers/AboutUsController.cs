using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using Wasit.Context;
using Wasit.Services.ViewModels.FixedPages;

namespace Wasit.Dashboard.Controllers
{
    public class AboutUsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IToastNotification _toastNotification;

        public AboutUsController(ApplicationDbContext context, IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _context.Settings
                .AsNoTracking()
                .Select(x => new UpdateAboutUsViewModel
                {
                    AboutUsAr = x.AboutUsAr,
                    AboutUsEn = x.AboutUsEn
                }).FirstOrDefaultAsync());
        }



        [HttpPost]
        public async Task<IActionResult> Update(UpdateAboutUsViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var aboutUs = await _context.Settings.FirstOrDefaultAsync();

            aboutUs.AboutUsAr = model.AboutUsAr;
            aboutUs.AboutUsEn = model.AboutUsEn;

            await _context.SaveChangesAsync();

            _toastNotification.AddSuccessToastMessage("تم التعديل بنجاح", new ToastrOptions { Title = "" });
            return RedirectToAction("Index", "Home");
        }
    }
}
