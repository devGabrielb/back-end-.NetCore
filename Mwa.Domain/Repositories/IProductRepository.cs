using Mwa.Domain.Comand.Results;
using Mwa.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mwa.Domain.Repositories
{
    public interface IProductRepository
    {
        Product GetU(Guid id);
        IEnumerable<GetProductListCommandResult> Get();
        
    }
}
