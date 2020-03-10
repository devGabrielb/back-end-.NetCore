using Mwa.Shared.Comands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mwa.Domain.Comand.Results
{
    class RegisterOderComandResult : IComandResult
    {

        public RegisterOderComandResult()
        {

        }
        public RegisterOderComandResult(string number)
        {
            Number = number;
        }

        public string Number { get; set; }
    }
}
