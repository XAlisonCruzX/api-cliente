using Core.Interfaces.Services;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Api.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class ClienteController : Controller
    {

        private IClienteService service;

        public ClienteController(IClienteService service)
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

        [HttpPut("{id}")]
        [AllowAnonymous]
        public ActionResult<dynamic> Put([FromBody] ClienteModel cliente, int id)
        {

            return service.Update(cliente, id);
        }

        [HttpPost("Paginado")]
        [AllowAnonymous]
        public ActionResult<dynamic> GetPag([FromBody] dynamic paginacao)
        {
            //Nome vazio '' busca todos, caso preenchido buscara na query %nome%, caso não mande a propriedade nome = null retornara vazio
            var nome = "";
            try
            {
                nome = paginacao.GetProperty("NOME");

            }
            catch (Exception)
            {
                nome = " ";
            }

            return service.GetPag(paginacao.GetProperty("PAG").GetInt32(), paginacao.GetProperty("QUANT").GetInt32(),
                paginacao.GetProperty("NOME").ToString());


        }


    }
}
