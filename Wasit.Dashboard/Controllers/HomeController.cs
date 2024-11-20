using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Wasit.Core.Entities.UserTables;
using Wasit.Services.Interfaces.Dashboard;

namespace Wasit.Dashboard.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IDHomeService  _homeService;
        private readonly SignInManager<ApplicationDbUser> _signInManager;
        public HomeController(IDHomeService homeService, SignInManager<ApplicationDbUser> signInManager)
        {
            _homeService = homeService;
            _signInManager = signInManager;
        }


        public IActionResult Index()
        {
            var data = _homeService.HomeIndex();
            return View(data);
        }



        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            //if (returnUrl != null)
            //{
            //    return LocalRedirect(returnUrl);
            //}
            //else
            //{
                return LocalRedirect("/Identity/Account/Login");
            //}
        }
    }
}
