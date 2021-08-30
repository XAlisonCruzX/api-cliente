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
    public class TelefoneController
    {

        private TelefoneService service;

        public TelefoneController(TelefoneService service)
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
        public ActionResult<dynamic> GetTelefoneCliente(int id)
        {
            return service.GetTelefonesCliente(id);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult<dynamic> Post([FromBody] TelefoneModel telefone)
        {
            return service.Post(telefone);
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public ActionResult<dynamic> Put([FromBody] TelefoneModel telefone, int id)
        {
            return service.Update(telefone, id);
        }

    }
}
