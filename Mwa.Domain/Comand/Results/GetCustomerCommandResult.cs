using System;
using Mwa.Shared.Comands;

namespace Mwa.Domain.Comand.Results
{
    public class GetCustomerCommandResult : IComandResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }


    }
}