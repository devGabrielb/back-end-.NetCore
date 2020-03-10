using System;
using System.Collections.Generic;
using System.Text;

namespace Mwa.Domain.Enums
{
    public enum EOrderStatus
    {
        Created = 1,
        Inprogress = 2,
        OutForDelivery = 3,
        Delivered = 4,
        Cancel = 5,
    }
}
