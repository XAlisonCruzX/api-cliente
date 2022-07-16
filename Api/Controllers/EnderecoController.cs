using Core.Interfaces.Services;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EnderecoController
    {

        private IEnderecoService service;

        public EnderecoController(IEnderecoService service)
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
        public ActionResult<dynamic> GetEnderecoCliente(int id)
        {
            return service.GetEnderecosCliente(id);
        }



        [HttpPost]
        [AllowAnonymous]
        public ActionResult<dynamic> Post([FromBody] EnderecoModel endereco)
        {
            return service.Post(endereco);
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public ActionResult<dynamic> Put([FromBody] EnderecoModel endereco, int id)
        {
            return service.Update(endereco, id);
        }

    }
}
