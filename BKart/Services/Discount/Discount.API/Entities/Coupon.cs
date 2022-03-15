using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.API.Entities
{
    [Table("coupon")]
    public class Coupon
    {
        [Column("id")]
        public int ID { get; set; }
        [Column("productname")]
        public string ProductName { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("amount")]
        public int Amount { get; set; }
    }
}
