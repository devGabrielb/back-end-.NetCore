using Mwa.Shared.Comands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mwa.Domain.Comand.Inputs
{
    public class RegisterCustomerComand : Icomand
    {

        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Document { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

    }
}
