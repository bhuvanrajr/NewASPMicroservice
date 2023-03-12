using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Models
{
    public class ShoppingModel
    {
        public string  UserName { get; set; }
        public BasketModel BaseketWithProducts { get; set; }
        public List<OrderModel> Orders { get; set; }
    }
}
