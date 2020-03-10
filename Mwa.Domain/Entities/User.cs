using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mwa.Domain.Entities
{
    public class User : Notifiable
    {
        public User(string userName, string password, string ConfirmPassword)
        {
            Id = Guid.NewGuid();
            UserName = userName;
            Password = EncryptPassword(password);
            Active = true;

            AddNotifications(
                 new Contract()
                 .AreEquals(Password,EncryptPassword(ConfirmPassword),"Password","as senhas não coicidem")
                 );
        }

        public Guid Id  { get; private set; }
        public string UserName  { get; private set; }
        public string Password  { get; private set; }
        public bool Active { get; private set; }

        public void UserActivate() => Active = true;
        public void UserDeactivate() => Active = false;




        public bool Authenticate(string username, string password)
        {
            //gambiarra
            var pass = EncryptPassword(password);
            if (UserName == username && Password == EncryptPassword(pass))
                return true;

            AddNotification("User", "Usuário ou senha inválidos");
            return false;
        }

        private string EncryptPassword(string pass)
        {
            if (string.IsNullOrEmpty(pass)) return "";
            var password = (pass += "|2d331cca-f6c0-40c0-bb43-6e32989c2881");
            var md5 = System.Security.Cryptography.MD5.Create();
            var data = md5.ComputeHash(Encoding.Default.GetBytes(password));
            var sbString = new StringBuilder();
            foreach (var t in data)
                sbString.Append(t.ToString("x2"));

            return sbString.ToString();
        }


        public int boolean_bit()
        {
            if(this.Active)
                return 1;
            return 0;
        }


    }
}
