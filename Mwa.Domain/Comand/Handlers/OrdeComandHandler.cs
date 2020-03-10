using Flunt.Notifications;
using Mwa.Domain.Comand.Inputs;
using Mwa.Domain.Comand.Results;
using Mwa.Domain.Entities;
using Mwa.Domain.Repositories;
using Mwa.Shared.Comands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mwa.Domain.Comand.Handlers
{
    public class OrdeComandHandler : Notifiable, IComandHandler<RegisterOrderComand>
    {

        private readonly ICustumerRepository _custumerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;


        public OrdeComandHandler
            (ICustumerRepository custumerRepository,
            IProductRepository productRepository,
            IOrderRepository orderRepository
            )
        {
            _custumerRepository = custumerRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }



        public IComandResult Handle(RegisterOrderComand comand)
        {
            var customer = _custumerRepository.Get(comand.Customer);

            var order = new Order(customer, comand.DeliveryFee, comand.Discount);

            foreach (var item in comand.Items)
            {
                var product = _productRepository.GetU(item.Product);
                order.AddItem(new OrderItem(product, item.Quantity));
            }

            AddNotifications(order.Notifications);

            if (Valid)
                _orderRepository.Save(order);

            return new RegisterOderComandResult(order.Number);
           

        }
    }
}
