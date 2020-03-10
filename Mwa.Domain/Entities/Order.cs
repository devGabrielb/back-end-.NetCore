using Flunt.Validations;
using Mwa.Domain.Enums;
using Mwa.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mwa.Domain.Entities
{
    public class Order : Entity

    {
        private readonly List<OrderItem> _items;
        public Order(Customer customer, decimal deliveryFee, decimal discount)
        {
            Customer = customer;
            CreateDate = DateTime.Now;
            Number = Guid.NewGuid().ToString().Substring(0, 8);
            Status = EOrderStatus.Created;
            _items = new List<OrderItem>();
            DeliveryFee = deliveryFee;
            Discount = discount;



            AddNotifications(
                 new Contract()
                 .IsGreaterThan(deliveryFee, 0, "preço entrega", "O preço da entrega tem q ser maior q zero")
                 .IsGreaterThan(discount, -1, "desconto", "O desconto não pode ser menor que 0")
                 

                );

        }
        public Customer Customer { get; private set; }
        public DateTime CreateDate { get; private set; }

        public string Number { get; private set; }

        public EOrderStatus Status { get; private set; }

        public IReadOnlyCollection<OrderItem> Items => _items.ToArray();

        public decimal DeliveryFee { get; private set; }

        public decimal Discount { get; private set; }



        public decimal SubTotal() => Items.Sum(x => x.Total());
        public decimal Total() => SubTotal() + DeliveryFee - Discount;

        public void AddItem(OrderItem item)
        {
            AddNotifications(item.Notifications);
            if (item.Valid)
                _items.Add(item);   
        }
       

       

    }
}
