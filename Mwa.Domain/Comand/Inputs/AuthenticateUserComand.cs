using Mwa.Shared.Comands;

namespace Mwa.Domain.Comand.Inputs
{
    public class AuthenticateUserComand : Icomand
    {
        public string UserName { get; set; }
        public string password { get; set; }
    }
}