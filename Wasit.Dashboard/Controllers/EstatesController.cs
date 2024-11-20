using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Wasit.Core.Enums;
using Wasit.Services.Interfaces.Dashboard;
using Wasit.Services.ViewModels.Estates.MyEstates;

namespace Wasit.Dashboard.Controllers
{
    public class EstatesController : Controller
    {
        private readonly IDEstateService _estateService;
        private readonly IDSharedService _sharedService;

        public EstatesController(IDEstateService estateService, IDSharedService sharedService)
        {
            _estateService = estateService;
            _sharedService = sharedService;
        }

        #region MyEstates
        public async Task<IActionResult> MyEstates(CategoryType category = CategoryType.Sale, long type = 0)
        {
            var data = await _estateService.MyEstates(category, type);
            ViewBag.Types = await _estateService.EstateTypes(category);

            return View(data);
        }

        public async Task<IActionResult> MyEstateDetails(long id, CategoryType category)
        {
            var data = await _estateService.MyEstateDetails(id, category);
            ViewBag.Category = category;
            return View(data);
        }

        public async Task<IActionResult> SelectCategory()
        {
            return View();
        }

        public async Task<IActionResult> Create(CategoryType category)
        {
            ViewBag.CategoryType = category;
            ViewBag.EstateTypes = new SelectList(await _sharedService.EstateTypes(category), "Id", "Name");
            ViewBag.Cities = new SelectList(await _sharedService.Cities(), "Id", "Name");

            return View();
        }


        public async Task<IActionResult> GetRegionsByCityId(long id)
        {
            var result = await _sharedService.RegionsByCity(id);
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateEstateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CategoryType = model.CategoryType;
                ViewBag.EstateTypes = new SelectList(await _sharedService.EstateTypes(model.CategoryType), "Id", "Name", model.EstateTypeId);
                ViewBag.Cities = new SelectList(await _sharedService.Cities(), "Id", "Name", model.CityId);
                return View(model);
            }

            var result = await _estateService.Create(model);
            if (!result.isSuccess)
            {
                ViewBag.CategoryType = model.CategoryType;
                ViewBag.EstateTypes = new SelectList(await _sharedService.EstateTypes(model.CategoryType), "Id", "Name", model.EstateTypeId);
                ViewBag.Cities = new SelectList(await _sharedService.Cities(), "Id", "Name", model.CityId);
                //return View(model);
                return Json(new { isSuccess = result.isSuccess, message = result.message, category = model.CategoryType, id = result.estateId, estateTypeId = result.estateTypeId });
            }

            return Json(new { isSuccess = result.isSuccess, message = result.message, category = model.CategoryType, id = result.estateId, estateTypeId = result.estateTypeId });
        }


        public async Task<IActionResult> ContinueAddMoreSpecs(CategoryType category, long estateId, long estateTypeId)
        {
            ViewBag.EstateId = estateId;
            ViewBag.Category = category;
            var result = await _estateService.GetSpecsForm(estateTypeId);
            return View(result);
        }


        [HttpPost]
        public async Task<IActionResult> ContinueAddMoreSpecs(CreateUpdateEstateSpecsViewModel model)
        {
            var result = await _estateService.CreateSpecs(model);
            return Json(new { isSuccess = result.isSuccess, message = result.message });
        }



        public async Task<IActionResult> UpdateSaleEstate(long id)
        {
            var result = await _estateService.SaleEstateDetails(id);
            ViewBag.EstateTypes = new SelectList(await _sharedService.EstateTypes(CategoryType.Sale), "Id", "Name", result.EstateTypeId);
            ViewBag.Cities = new SelectList(await _sharedService.Cities(), "Id", "Name", result.CityId);
            ViewBag.Regions = new SelectList(await _sharedService.RegionsByCity(result.CityId), "Id", "Name", result.RegionId);

            return View(result);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateSaleEstate(UpdateSaleEstateViewModel model)
        {
            var result = await _estateService.UpdateSaleEstate(model);
           
            return Json(new { isSuccess = result.isSuccess, message = result.message, id = result.estateId });
        }
        
        
        public async Task<IActionResult> UpdateRentEstate(long id)
        {
            var result = await _estateService.RentEstateDetails(id);
            ViewBag.EstateTypes = new SelectList(await _sharedService.EstateTypes(CategoryType.Sale), "Id", "Name", result.EstateTypeId);
            ViewBag.Cities = new SelectList(await _sharedService.Cities(), "Id", "Name", result.CityId);
            ViewBag.Regions = new SelectList(await _sharedService.RegionsByCity(result.CityId), "Id", "Name", result.RegionId);

            return View(result);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateRentEstate(UpdateRentEstateViewModel model)
        {
            var result = await _estateService.UpdateRentEstate(model);
           
            return Json(new { isSuccess = result.isSuccess, message = result.message, id = result.estateId });
        }


        public async Task<IActionResult> UpdateDailyRentEstate(long id)
        {
            var result = await _estateService.DailyRentEstateDetails(id);
            ViewBag.EstateTypes = new SelectList(await _sharedService.EstateTypes(CategoryType.Sale), "Id", "Name", result.EstateTypeId);
            ViewBag.Cities = new SelectList(await _sharedService.Cities(), "Id", "Name", result.CityId);
            ViewBag.Regions = new SelectList(await _sharedService.RegionsByCity(result.CityId), "Id", "Name", result.RegionId);

            return View(result);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateDailyRentEstate(UpdateDailyRentEstateViewModel model)
        {
            var result = await _estateService.UpdateDailyRentEstate(model);

            return Json(new { isSuccess = result.isSuccess, message = result.message, id = result.estateId });
        }


        public async Task<IActionResult> UpdateEntertainmentRentEstate(long id)
        {
            var result = await _estateService.DailyRentEstateDetails(id);
            ViewBag.EstateTypes = new SelectList(await _sharedService.EstateTypes(CategoryType.Sale), "Id", "Name", result.EstateTypeId);
            ViewBag.Cities = new SelectList(await _sharedService.Cities(), "Id", "Name", result.CityId);
            ViewBag.Regions = new SelectList(await _sharedService.RegionsByCity(result.CityId), "Id", "Name", result.RegionId);

            return View(result);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateEntertainmentRentEstate(UpdateEntertainmentRentEstateViewModel model)
        {
            var result = await _estateService.UpdateEntertainmentRentEstate(model);

            return Json(new { isSuccess = result.isSuccess, message = result.message, id = result.estateId });
        }


        public async Task<IActionResult> ContinueEditMoreSpecs(CategoryType category, long estateId)
        {
            ViewBag.EstateId = estateId;
            ViewBag.Category = category;
            var result = await _estateService.GetEstateSpecsForm(estateId);
            return View(result);
        }


        [HttpPost]
        public async Task<IActionResult> ContinueEditMoreSpecs(CreateUpdateEstateSpecsViewModel model)
        {
            var result = await _estateService.UpdateSpecs(model);
            return Json(new { isSuccess = result.isSuccess, message = result.message });
        }

        public IActionResult MyRequests()
        {
            return View();
        }

        #endregion


        #region MyRequests
        public async Task<IActionResult> MyReservationRequests(CategoryType category, long estateId, ReservationStatus status = ReservationStatus.Current)
        {
            ViewBag.category = category;
            var data = await _estateService.MyReservationRequests(category, estateId, status);
            return View(data);
        }

        public async Task<IActionResult> MyEvaluationRequests(CategoryType category, long estateId, RatingStatus status = RatingStatus.Current)
        {
            ViewBag.category = category;
            var data = await _estateService.MyEvaluationRequests(category, estateId, status);
            return View(data);
        }
        
        
        public async Task<IActionResult> MyPurchaseRequests(long estateId)
        {
            ViewBag.category = CategoryType.Sale;
            var data = await _estateService.MyPurchaseRequests(estateId);
            return View(data);
        }

        public async Task<IActionResult> MyRentRequests(long estateId)
        {
            ViewBag.category = CategoryType.Rent;
            var data = await _estateService.MyRentRequests(estateId);
            return View(data);
        }

        public async Task<IActionResult> MyDailyRentRequests(long estateId, DailyRentStatus status)
        {
            ViewBag.category = CategoryType.DailyRent;
            var data = await _estateService.MyDailyRentRequests(estateId, status);
            return View(data);
        }
        
        
        public async Task<IActionResult> MyEntertainmentRentRequests(long estateId, DailyRentStatus status)
        {
            ViewBag.category = CategoryType.DailyRent;
            var data = await _estateService.MyEntertainmentRentRequests(estateId, status);
            return View(data);
        }

        public async Task<IActionResult> MyRequestDetails(long requestId, CategoryType category, int requestType)
        {
            ViewBag.type = requestType;
            ViewBag.category = category;
            var data = await _estateService.MyRequestDetails(requestId, category, requestType);
            return View(data);
        }


        [HttpPut]
        public async Task<IActionResult> AcceptPurchaseRequest(long id)
        {
            var result = await _estateService.AcceptPurchaseRequest(id);
            return Json(new { isSuccess = result.isSuccess, message = result.message });
        }
        
        
        [HttpPut]
        public async Task<IActionResult> RejectPurchaseRequest(long id)
        {
            var result = await _estateService.RejectPurchaseRequest(id);
            return Json(new { isSuccess = result.isSuccess, message = result.message });
        }


        [HttpPut]
        public async Task<IActionResult> ConfirmEstateIsSold(long estateId, double price, long requestId)
        {
            var result = await _estateService.ConfirmEstateIsSold(estateId, price, requestId);
            return Json(new { isSuccess = result.isSuccess, message = result.message });
        }
        
        
        [HttpPut]
        public async Task<IActionResult> AcceptRentRequest(long id)
        {
            var result = await _estateService.AcceptRentRequest(id);
            return Json(new { isSuccess = result.isSuccess, message = result.message });
        }
        
        
        [HttpPut]
        public async Task<IActionResult> RejectRentRequest(long id)
        {
            var result = await _estateService.RejectRentRequest(id);
            return Json(new { isSuccess = result.isSuccess, message = result.message });
        }


        [HttpPut]
        public async Task<IActionResult> ConfirmEstateIsRented(long estateId, long requestId)
        {
            var result = await _estateService.ConfirmEstateRented(estateId, requestId);
            return Json(new { isSuccess = result.isSuccess, message = result.message });
        }
        #endregion


        #region UsersEstates

        public async Task<IActionResult> UsersEstates(CategoryType category = CategoryType.Sale, long type = 0)
        {
            var data = await _estateService.UsersEstates(category, type);
            ViewBag.Types = await _estateService.EstateTypes(category);

            return View(data);
        }


        public async Task<IActionResult> UserEstateDetails(long id, CategoryType category)
        {
            var data = await _estateService.UserEstateDetails(id, category);
            ViewBag.Category = category;
            return View(data);
        }


        [HttpPut]
        public async Task<IActionResult> AcceptEstate(long id, string userId ,CategoryType category, double deposit)
        {
            var result = await _estateService.AcceptEstate(id, userId, category, deposit);
            ViewBag.Category = category;
            return Json(new { isSuccess = result.isSuccess, message = result.message });
        }
        
        
        [HttpPut]
        public async Task<IActionResult> RejectEstate(long id, string userId, CategoryType category, string reason)
        {
            var result = await _estateService.RejectEstate(id, userId, category, reason);
            ViewBag.Category = category;
            return Json(new { isSuccess = result.isSuccess, message = result.message });
        }


        #endregion



        #region Shared
        [HttpPut]
        public async Task<IActionResult> ChangeVisibility(long id, CategoryType category)
        {
            var result = await _estateService.ChangeVisibility(id, category);
            return Json(result);
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteUsersPrices(long id)
        {
            var result = await _estateService.DeleteUsersPrices(id);
            return Json(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Remove(long id, CategoryType category)
        {
            var result = await _estateService.Remove(id, category);
            return Json(result);
        }
        #endregion
    }
}
