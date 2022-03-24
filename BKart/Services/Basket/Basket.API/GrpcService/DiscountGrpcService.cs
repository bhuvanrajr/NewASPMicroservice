using Discount.GRPC.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.GrpcService
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _client = null;
        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient)
        {
            this._client = discountProtoServiceClient;
        }

        public async Task<CouponModel> GetCoupon(string productName)
        {
            return await _client.GetDiscountAsync(new GetDiscountRequest { ProductName = productName });
        }
    }
}
