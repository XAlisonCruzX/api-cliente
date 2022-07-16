using Core.Interfaces.Services;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RdSocialController
    {

        private IRdSocialService service;

        public RdSocialController(IRdSocialService service)
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

        [HttpPut("{id}")]
        [AllowAnonymous]
        public ActionResult<dynamic> Put([FromBody] RedeSocialModel rdSocial, int id)
        {
            return service.Update(rdSocial, id);
        }


    }
}
