using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flunt.Notifications;
using Microsoft.AspNetCore.Mvc;
using Mwa.Infra.Transactions;

namespace Mwa.Api.Controllers
{
    public class BaseController : Controller
    {
        private readonly IUoW _uow;

        public BaseController(IUoW uow)
        {
            _uow = uow;
        }


    public async Task<IActionResult> Response(object result, IEnumerable<Notification> notifications )
    {
        if(!notifications.Any())
        {
            try
            {

                _uow.Commit();
                return  Ok(new
                {
                sucess = true,
                data = result
                }); 



            }
            catch 
            {
                _uow.Rollback();
                return  BadRequest(new
                {
                sucess = false,
                errors = new [] {"Ocorreu uma falha no servidor."}
                }); 
            }
        }else
        {
            _uow.Rollback();
            return BadRequest(new
                {
                sucess = false,
                errors = notifications 
                }); 
        }
    }



    }
}