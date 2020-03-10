using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mwa.Domain.Comand.Handlers;
using Mwa.Domain.Comand.Inputs;
using Mwa.Domain.Entities;
using Mwa.Domain.Repositories;
using Mwa.Infra.Contexts;
using Mwa.Infra.Transactions;

namespace Mwa.Api.Controllers
{
    public class CustomerController : BaseController
    {
       // private readonly MwaDataContext _context;
        private readonly CustomerComandHandler _handler;
        private readonly ICustumerRepository _repository;
        public CustomerController(ICustumerRepository repository, IUoW uow, CustomerComandHandler handler)
            :base(uow)
        {
            _repository = repository;
            _handler = handler;
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("v1/customers")]
        public async Task<IActionResult> Post([FromBody]RegisterCustomerComand command)
        {
           var result = _handler.Handle(command);
            
            return  await Response(result,_handler.Notifications);
        }


        //  [HttpGet]
        //  [AllowAnonymous]
        // [Route("v1/customers/{username}")]
        // public Customer get(string username)
        // {
        //     return _repository.GetByUserName(username);
        // }
    }
}