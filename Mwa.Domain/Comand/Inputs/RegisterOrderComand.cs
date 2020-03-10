using Mwa.Shared.Comands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mwa.Domain.Comand.Inputs
{
    public class RegisterOrderComand : Icomand
    {
        public Guid Customer { get; set; }
        public decimal DeliveryFee { get; set; }
        public decimal Discount { get; set; }
        public IEnumerable<RegisterOrderItemComand> Items { get; set; }

    }
}
