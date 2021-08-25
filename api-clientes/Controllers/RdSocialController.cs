using api_clientes.Models;
using api_clientes.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_clientes.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RdSocialController
    {

        private RdSocialService service;

        public RdSocialController(RdSocialService service)
        {
            this.service = service;
        }


        [HttpDelete("{id}")]
        [AllowAnonymous]
        public ActionResult<dynamic> Delele(int id)
        {
            return service.Delete(id);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<dynamic> Get()
        {
            return service.GetAll();
        }


        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<dynamic> Get(int id)
        {


            return service.Get(id);
        }
        [HttpGet("cliente/{id}")]
        [AllowAnonymous]
        public ActionResult<dynamic> GetRdSocialCliente(int id)
        {
            return service.GetRdSocialCliente(id);
        }



        [HttpPost]
        [AllowAnonymous]
        public ActionResult<dynamic> Post([FromBody] RedeSocialModel rdSocial)
        {
            return service.Post(rdSocial);
        }

        [HttpPut]
        [AllowAnonymous]
        public ActionResult<dynamic> Put([FromBody] RedeSocialModel rdSocial)
        {
            return service.Update(rdSocial);
        }


    }
}
