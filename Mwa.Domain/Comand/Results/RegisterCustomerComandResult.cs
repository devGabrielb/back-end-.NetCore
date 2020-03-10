using Mwa.Shared.Comands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mwa.Domain.Comand.Results
{
    class RegisterCustomerComandResult : IComandResult
    {
        public RegisterCustomerComandResult()
        {

        }

        public RegisterCustomerComandResult(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

    }
}
