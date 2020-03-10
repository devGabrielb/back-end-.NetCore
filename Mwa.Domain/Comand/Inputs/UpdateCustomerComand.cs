using Mwa.Shared.Comands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mwa.Domain.Comand.Inputs
{
    class UpdateCustomerComand : Icomand
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
