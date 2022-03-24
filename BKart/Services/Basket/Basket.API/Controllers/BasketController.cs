using Basket.API.Entities;
using Basket.API.GrpcService;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly DiscountGrpcService _discountGrpcService = null;
        public BasketController(IBasketRepository basketRepository, DiscountGrpcService grpcService)
        {
            _basketRepository = basketRepository;
            _discountGrpcService = grpcService;
        }
        
        [HttpGet]
        [Route("[action]/{userName}")]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            return Ok(await _basketRepository.GetBasket(userName));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody]ShoppingCart basket)
        {
            foreach(var item in basket.Items)
            {
                var coupon = await _discountGrpcService.GetCoupon(item.ProductName);
                item.Price -= coupon.Amount;
            }
            return Ok(await _basketRepository.UpdateShoppingCart(basket));
        }

        [HttpDelete]
        [Route("[action]/{userName}")]
        public async Task<ActionResult> DeleteBasket(string userName)
        {
            await _basketRepository.DeleteShoppingCart(userName);
            return Ok();
        }

    }
}
