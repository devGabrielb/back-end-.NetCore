using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mwa.Domain.ValueObjects
{
    public class Email : Notifiable
    {
        public Email(string address)
        {
            Address = address;

            AddNotifications(
                new Contract()
                .IsEmail(Address, "Email", "email inválido"));
        }

        public string Address { get; private set; }
    }
}
