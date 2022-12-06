using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;
using Ordering.Application.Features.Orders.Commands.DeleteOrder;
using Ordering.Application.Features.Orders.Commands.UpdateOrder;
using Ordering.Application.Features.Orders.Queries.GetOrdersList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Ordering.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("[action]/{userName}")]
        [ProducesResponseType(typeof(List<OrdersDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<OrdersDTO>>> GetOrdersList(string userName)
        {
            var query = new GetOrdersListRequest(userName);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CheckOutOrder(CheckoutOrderCommand checkoutOrderCommand)
        {
            var result = await _mediator.Send(checkoutOrderCommand);
            return Ok(result);
        }

        [HttpPut]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateOrder(UpdateOrderCommand updateOrderCommand)
        {
            await _mediator.Send(updateOrderCommand);
            return NoContent();
        }

        [HttpDelete]
        [Route("[action]/{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            DeleteOrderCommand cmd = new DeleteOrderCommand { ID = id };
            await _mediator.Send(cmd);
            return NoContent();
        }
    }
}

