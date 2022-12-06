using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList
{
    public class GetOrdersListRequest : IRequest<List<OrdersDTO>>
    {
        public string UserName { get; set; }
        public GetOrdersListRequest(string userName)
        {
            UserName = userName;
        }
    }
}
