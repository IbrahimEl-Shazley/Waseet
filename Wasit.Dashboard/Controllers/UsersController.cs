using Microsoft.AspNetCore.Mvc;
using Wasit.Core.Enums;
using Wasit.Services.Interfaces.Dashboard;

namespace Wasit.Dashboard.Controllers
{
    public class UsersController : Controller
    {
        private readonly IDUsersServices _usersService;

        public UsersController(IDUsersServices usersService)
        {
            _usersService = usersService;
        }

        #region Owners
        public async Task<IActionResult> Owners()
        {
            var owners = await _usersService.Owners();
            return View(owners);
        }


        [HttpGet]
        public async Task<IActionResult> AdditionalOwnerInfo(string userId)
        {
            var user = await _usersService.GetUserById(userId);
            if (user == null)
                return NotFound();

            return Json(new { data = await _usersService.AdditionalOwnerInfo(userId) });
        }
        #endregion

        #region Brokers
        public async Task<IActionResult> Brokers()
        {
            var brokers = await _usersService.Brokers();
            return View(brokers);
        }

        [HttpGet]
        public async Task<IActionResult> AdditionalBrokerInfo(string userId)
        {
            var user = await _usersService.GetUserById(userId);
            if (user == null)
                return NotFound();

            if (user.AccountType == AccountType.Individual)
                return Json(new { data = await _usersService.AdditionalIndividualBrokerInfo(userId) });

            else if (user.AccountType == AccountType.Facility)
                return Json(new { data = await _usersService.AdditionalFaciltyBrokerInfo(userId) });

            else
                return BadRequest();
        }
        #endregion

        #region Delegates
        public async Task<IActionResult> Delegates()
        {
            var delegates = await _usersService.Delegates();
            return View(delegates);
        }


        [HttpGet]
        public async Task<IActionResult> AdditionalDelegateInfo(string userId)
        {
            var user = await _usersService.GetUserById(userId);
            if (user == null)
                return NotFound();

            return Json(new { data = await _usersService.AdditionalDelegateInfo(userId) });
        }
        #endregion

        #region Developers
        public async Task<IActionResult> Developers()
        {
            var developers = await _usersService.Developers();
            return View(developers);
        }


        [HttpGet]
        public async Task<IActionResult> AdditionalDeveloperInfo(string userId)
        {
            var user = await _usersService.GetUserById(userId);
            if (user == null)
                return NotFound();

            return Json(new { data = await _usersService.AdditionalDeveloperInfo(userId) });
        }
        #endregion


        #region Shared
        [HttpPut]
        public async Task<IActionResult> ChangeActivation(string userId)
        {
            (bool isSuccess, string message) result = await _usersService.ChangeActivation(userId);
            return Json(new { success = result.isSuccess, message = result.message });

        }


        [HttpDelete]
        public async Task<IActionResult> Remove(string userId)
        {
            (bool isSuccess, string message) result = await _usersService.Remove(userId);
            return Json(new { success = result.isSuccess, message = result.message });

        }


        [HttpPost]
        public async Task<IActionResult> SendNotification(string userId, string title, string content)
        {
            (bool isSuccess, string message) result = await _usersService.SendNotification(userId, title, content);
            return Json(result);
        }
        #endregion
    }
}
