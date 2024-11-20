using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wasit.Context;
using Wasit.Services.Interfaces.Dashboard;
using Wasit.Services.ViewModels.City;
using Wasit.Services.ViewModels.FixedPages;

namespace Wasit.Dashboard.Controllers
{
    public class Q_AController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IDQAService _service;


        public Q_AController(ApplicationDbContext context, IDQAService service)
        {
            _context = context;
            _service = service;
        }



        public async Task<IActionResult> Index()
        {
            var data = await _service.GetQA();

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(QuestionsAndAnswersViewModel model)
        {
            var result = await _service.CreateQA(model);
            return Json(new { success = result.isSuccess, message = result.message });
        }
    }
}
