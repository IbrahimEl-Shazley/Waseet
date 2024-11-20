using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using Wasit.Context;
using Wasit.Services.ViewModels.FixedPages;

namespace Wasit.Dashboard.Controllers
{
    public class TermsAndConditionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IToastNotification _toastNotification;

        public TermsAndConditionsController(ApplicationDbContext context, IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _context.Settings
                .AsNoTracking()
                .Select(x => new UpdateTermsAndConditionsViewModel
                {
                    ConditionsOwnerAr = x.ConditionsOwnerAr,
                    CondtionsOwnerEn = x.CondtionsOwnerEn,

                    ConditionsDelegateAr = x.ConditionsDelegateAr,
                    ConditionsDelegateEn = x.ConditionsDelegateEn,

                    ConditionsBrokerAr = x.ConditionsBrokerAr,
                    ConditionsBrokerEn = x.ConditionsBrokerEn,

                    ConditionsDeveloperAr = x.ConditionsDeveloperAr,
                    ConditionsDeveloperEn = x.ConditionsDeveloperEn
                }).FirstOrDefaultAsync());
        }



        [HttpPost]
        public async Task<IActionResult> Update(UpdateTermsAndConditionsViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _context.Settings.FirstOrDefaultAsync();

            result.ConditionsOwnerAr = model.ConditionsOwnerAr;
            result.CondtionsOwnerEn = model.CondtionsOwnerEn;

            result.ConditionsDelegateAr = model.ConditionsDelegateAr;
            result.ConditionsDelegateEn = model.ConditionsDelegateEn;

            result.ConditionsBrokerAr = model.ConditionsBrokerAr;
            result.ConditionsBrokerEn = model.ConditionsBrokerEn;

            result.ConditionsDeveloperAr = model.ConditionsDeveloperAr;
            result.ConditionsDeveloperEn = model.ConditionsDeveloperEn;


            await _context.SaveChangesAsync();

            _toastNotification.AddSuccessToastMessage("تم التعديل بنجاح", new ToastrOptions { Title = "" });
            return RedirectToAction("Index", "Home");
        }

    }
}
