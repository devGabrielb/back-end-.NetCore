using Flunt.Validations;
using Mwa.Shared.Entities;

namespace Mwa.Domain.Entities
{
    public class OrderItem : Entity
    {
        public OrderItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
            Price = Product.Price;

            AddNotifications(
                 new Contract()
                 .IsGreaterThan(Quantity, 0,"preço", "A quantidade não pode ser igual a zero")
                 .IsGreaterOrEqualsThan(Product.QuantityOnHand, quantity,"Quantidade","Não temos essa quantidade")
                 
                 
                );

            Product.DecreaseQuantity(Quantity);
        }

        public Product Product { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }

        
                

        public decimal Total() => Price * Quantity;
    }
}