using AutoMapper;
using Basket.API.Entities;
using Basket.API.GrpcService;
using Basket.API.Repositories;
using EventBus.Messages.Events;
using MassTransit;
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
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;
        public BasketController(IBasketRepository basketRepository, DiscountGrpcService grpcService, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _basketRepository = basketRepository;
            _discountGrpcService = grpcService;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
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

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> Checkout([FromBody]BasketCheckout basketCheckout)
        {
            var basket = await _basketRepository.GetBasket(basketCheckout.UserName);
            if (basket == null)
            {
                return BadRequest();
            }

            BasketCheckoutEvent basketCheckoutEventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
            basketCheckoutEventMessage.TotalPrice = basket.TotalPrice;
            await _publishEndpoint.Publish(basketCheckoutEventMessage);

            await _basketRepository.DeleteShoppingCart(basketCheckout.UserName);

            return Ok();
        }

    }
}
