using Flunt.Validations;
using Mwa.Domain.ValueObjects;
using Mwa.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mwa.Domain.Entities
{
    public class Customer : Entity
    {
        public Customer
            (

            Name name,
            Email email,
            Document document,
            User user
            )
        {
            Name = name;
            BirthDate = null;         
            Email = email;
            Document = document;
            User = user;

            AddNotifications(Name.Notifications);
            AddNotifications(Email.Notifications);
            AddNotifications(Document.Notifications);
            AddNotifications(User.Notifications);


        }

        public Name Name { get; private set; }
       
        public DateTime? BirthDate { get; private set; }
        public Email Email { get; private set; }
        public Document Document { get; private set; }
        public User User { get; private set; }





        
           
        public void Update(Name name, DateTime birthDate)
        {
            AddNotifications(name.Notifications);
            Name = name;
            BirthDate = birthDate;
        }




        public override string ToString()
        {
            return $"{Name.ToString()}";
        }
    }


   
}
