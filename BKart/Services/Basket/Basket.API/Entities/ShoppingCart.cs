using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Entities
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {

        }
        public ShoppingCart(string userName)
        {
            this.UserName = userName;
        }

        public string UserName { get; set; }
        public List<ShoppingCartItem> Items { get; set; }
        public decimal TotalPrice
        {
            get { return GetTotalPrice(this.Items); }

        }

        private static decimal GetTotalPrice(List<ShoppingCartItem> items)
        {
            decimal totalPrice = 0;
            foreach(ShoppingCartItem item in items)
            {
                totalPrice += item.Price * item.Quantity;
            }
            return totalPrice;
        }
    }
}
