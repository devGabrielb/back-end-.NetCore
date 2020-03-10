using Mwa.Domain.Comand.Results;
using Mwa.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mwa.Domain.Repositories
{
    public interface ICustumerRepository
    {

        Customer Get(Guid id);
        GetCustomerCommandResult Get(string username);
        //Customer GetByUserName(string username);
        bool DocumentExists(string document);
        void Update(Customer customer);
        void Save(Customer customer);

    }
}
