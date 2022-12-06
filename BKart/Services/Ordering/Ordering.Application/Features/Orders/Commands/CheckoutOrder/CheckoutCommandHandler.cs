using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Infrastructure;
using Ordering.Application.Models;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    public class CheckoutCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
    {
        private IOrderRepository _orderRepository;
        public IMapper _mapper;
        public IEmailService _emailService;
        public ILogger<CheckoutCommandHandler> _logger;
        public CheckoutCommandHandler(IOrderRepository orderRepository, IMapper mapper, IEmailService emailService, ILogger<CheckoutCommandHandler> logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = _mapper.Map<Order>(request);
            var newOrder = await _orderRepository.AddAsync(orderEntity);

            _logger.LogInformation($"Order with Id - {newOrder.ID} is successfully created.");
            await SendEmail(newOrder);
            return newOrder.ID;
        }

        private async Task SendEmail(Order order)
        {
            var email = new Email() { To = "bhuvan.raj.r@outlook.com", Body = $"Your order got created successfully", Subject = "Order got created" };
            try
            {
                await _emailService.SendEmail(email);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Email didnt go through for order with Id {order.ID}");
            }
        }
    }
}
