using api_clientes.Models;
using api_clientes.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api_clientes.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class ClienteController : Controller
    {

        private ClienteService service;
  
        public ClienteController(ClienteService service)
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

        

        [HttpPost]
        [AllowAnonymous]
        public ActionResult<dynamic> Post([FromBody] ClienteModel cliente)
        {
            return service.Post(cliente);
        }

        [HttpPut]
        [AllowAnonymous]
        public ActionResult<dynamic> Put([FromBody] ClienteModel cliente)
        {
            return service.Update(cliente);
        }



    }
}
