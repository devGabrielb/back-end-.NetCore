using System;
using System.Collections.Generic;
using System.Text;

namespace Mwa.Domain.Services
{
    public interface IEmailService
    {
        //sendGrid
        void Send(string name, string email, string subject, string body);
    }
}
