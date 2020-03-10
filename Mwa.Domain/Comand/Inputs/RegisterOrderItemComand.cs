using Mwa.Shared.Comands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mwa.Domain.Comand.Inputs
{
    public class RegisterOrderItemComand : Icomand
    {

        public Guid Product { get; set; }
        public int Quantity { get; set; }

    }
}
