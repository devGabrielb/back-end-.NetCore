using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mwa.Domain.ValueObjects
{
    public class Name : Notifiable
    {
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;



            AddNotifications(
               new Contract()
               .Requires()
               .HasMaxLen(FirstName, 120, "FirstName", "O nome deve ter no máximo 120 caracteres")
               .HasMinLen(FirstName, 3, "FirstName", "O nome deve ter no mínimo 3 caracteres")
               .HasMaxLen(LastName, 120, "LastName", "O ultimo nome deve ter no máximo 120 caracteres")
               .HasMinLen(LastName, 3, "LastName", "O ultimo nome deve ter no mínimo 3 caracteres")
               );

        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }






        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }

    }
}
