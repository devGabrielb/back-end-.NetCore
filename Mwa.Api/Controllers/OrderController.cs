using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mwa.Domain.Comand.Handlers;
using Mwa.Domain.Comand.Inputs;
using Mwa.Infra.Repositories;
using Mwa.Infra.Transactions;

namespace Mwa.Api.Controllers
{
    public class OrderController : BaseController
    {

        private readonly OrdeComandHandler _handle;
       
        public OrderController(OrdeComandHandler handle,IUoW uow)
         : base(uow)
        {
            _handle = handle;
        }



        [HttpPost]
        [Route("v1/orders")]
        public async Task<IActionResult> Post([FromBody] RegisterOrderComand command)
        {
           var result = _handle.Handle(command);
            
            return  await Response(result,_handle.Notifications);
        }
    }
}