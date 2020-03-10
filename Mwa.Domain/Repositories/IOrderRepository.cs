using Mwa.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mwa.Domain.Repositories
{
    public interface IOrderRepository
    {

        void Save(Order order);
    }
}
