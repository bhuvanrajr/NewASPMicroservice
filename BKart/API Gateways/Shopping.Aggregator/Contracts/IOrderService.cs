﻿using Shopping.Aggregator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Contracts
{
    public interface IOrderService
    {
        Task<List<OrderModel>> GetAllOrdersByUserName(string userName);
    }
}
