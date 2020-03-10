using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mwa.Domain.Repositories;

namespace Mwa.Api.Controllers
{
    public class ProductController : Controller
    {

        private readonly IProductRepository _repository;



        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("v1/products")]
        public IActionResult Get(){

            return Ok(_repository.Get());
        }


        
       

    }
}