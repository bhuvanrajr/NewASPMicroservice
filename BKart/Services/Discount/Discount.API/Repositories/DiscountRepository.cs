using Discount.API.Data;
using Discount.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.API.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly DiscountContext _discountContext;
        public DiscountRepository(DiscountContext discountContext)
        {
            this._discountContext = discountContext;
        }
        public async Task<bool> CreateDiscount(Coupon coupon) 
        {
            var result = await _discountContext.Coupon.AddAsync(coupon);
            await _discountContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            var coupon = await _discountContext.Coupon.FirstOrDefaultAsync(item => item.ProductName == productName);

            if (coupon != null)
            {
                _discountContext.Coupon.Remove(coupon);
                await _discountContext.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            return await _discountContext.Coupon.FirstOrDefaultAsync(item => item.ProductName == productName);
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            var result = _discountContext.Coupon.Update(coupon);
            await _discountContext.SaveChangesAsync();
            return true;
        }
    }
}
