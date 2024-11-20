using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Wasit.Controllers;
using Wasit.Core.Enums;
using Wasit.Services.Interfaces.Generic.ConsumerEstates;

namespace Wasit.API.Controllers.ConsumerEstates
{
    [ApiExplorerSettings(GroupName = "ConsumerEstates")]
    public class ConsumerSaleEstatesController : BaseController
    {
        private readonly IConsumerSaleEstatesService _consumerSaleEstatesService;

        public ConsumerSaleEstatesController(IConsumerSaleEstatesService consumerSaleEstatesService)
        {
            _consumerSaleEstatesService = consumerSaleEstatesService;
        }


        [HttpGet("SaleEstateInfo")]
        public async Task<IActionResult> SaleEstateInfo([Required] long saleEstateId)
        {
            return _OK(await _consumerSaleEstatesService.SaleEstateInfo(UserId, saleEstateId));
        }


        [HttpGet("ListP4AddPriceToEstate")]
        public async Task<IActionResult> ListP4AddPriceToEstate()
        {
            return _OK(await _consumerSaleEstatesService.ListP4AddPriceToEstate());
        }


        [HttpPost("SubscribeToAddPriceToEstatePackage")]
        public async Task<IActionResult> SubscribeToAddPriceToEstatePackage([Required] long packageId, [Required] TypePay paymentMethod) // To-Do Payment
        {
            return _OK(await _consumerSaleEstatesService.SubscribeToAddPriceToEstatePackage(packageId, paymentMethod));
        }


        [HttpPost("AddPriceToEstate")]
        public async Task<IActionResult> AddPriceToEstate([Required] long estateId, [Required] double price)
        {
            return _OK(await _consumerSaleEstatesService.AddPriceToEstate(estateId, price, UserId), "ItemCreatedSuccessfully");
        }


        [HttpPost("AddPurchaseRequest")]
        public async Task<IActionResult> AddPurchaseRequest([Required] long estateId)
        {
            return _OK(new
            {
                requestId = await _consumerSaleEstatesService.AddPurchaseRequest(estateId, UserId)
            });
        }



    }
}
