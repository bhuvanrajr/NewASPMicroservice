using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Exceptions;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrder
{
    public class DeleteOrderRequestHandler : IRequestHandler<DeleteOrderCommand>
    {
        private IOrderRepository _orderRepository;
        private IMapper _mapper;
        private ILogger<DeleteOrderRequestHandler> _logger;

        public DeleteOrderRequestHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<DeleteOrderRequestHandler> logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.ID);
            
            if(order != null)
            {
                await _orderRepository.DeleteAsync(order);

            }
            else
            {
                _logger.LogError("Order doesnt exist in the collection");
                throw new NotFoundException(nameof(Order), request.ID);
            }
            _logger.LogInformation($"Order with Id {request.ID} got deleted");
            return Unit.Value;
        }
    }
}
