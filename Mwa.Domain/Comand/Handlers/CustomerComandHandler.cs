using Flunt.Notifications;
using Mwa.Domain.Comand.Inputs;
using Mwa.Domain.Comand.Results;
using Mwa.Domain.Entities;
using Mwa.Domain.Repositories;
using Mwa.Domain.Resources;
using Mwa.Domain.Services;
using Mwa.Domain.ValueObjects;
using Mwa.Shared.Comands;

namespace Mwa.Domain.Comand.Handlers
{
    public class CustomerComandHandler : Notifiable,
        IComandHandler<RegisterCustomerComand>
    {
        private readonly ICustumerRepository _custumerRepository;
        private readonly IEmailService _emailService;

        public CustomerComandHandler(ICustumerRepository custumerRepository, IEmailService emailService)
        {
            _custumerRepository = custumerRepository;
            _emailService = emailService;
        }


        

        public IComandResult Handle(RegisterCustomerComand comand)
        {
           


            if (_custumerRepository.DocumentExists(comand.Document))
            {

                AddNotification("Document", "Documento já está em uso!");
                return null; 
            }
            var name = new Name(comand.FirstName, comand.LastName);
            var email = new Email(comand.Email);
            var document = new Document(comand.Document);
            var user = new User(comand.UserName, comand.Password,comand.ConfirmPassword);
            var customer = new Customer(name, email, document, user);

            AddNotifications(customer.Notifications);

            if (!Valid)
                return null;

            _custumerRepository.Save(customer);
            

            //fundation.Zurb

           // _emailService.Send(
            //customer.Name.ToString(),
           // customer.Email.Address,
           // string.Format(EmailTemplate.WelcomeEmailTitle, customer.Name),
           // string.Format(EmailTemplate.WelcomeEmailBody, customer.Name)
           // );



            return new RegisterCustomerComandResult(customer.Id, customer.Name.ToString());
            


        }
    } 
}
