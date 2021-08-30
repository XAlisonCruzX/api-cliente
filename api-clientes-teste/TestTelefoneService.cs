using api_clientes.Database;
using api_clientes.Models;
using api_clientes.Repositories;
using api_clientes.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_clientes_teste
{
    // Mudar id nos testes que exigem dados ja criados
    class TestTelefoneService
    {
        static Conexao conn = new Conexao("Server=localhost;Database=base-clientes;Trusted_Connection=True;");
        static TelefoneService serviceTelefone = new TelefoneService(new TelefoneRepository(conn));
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TestGetAll()
        {
            dynamic resposta = serviceTelefone.GetAll();
            dynamic objResposta = JObject.Parse(JsonConvert.SerializeObject(resposta));
            var status = objResposta.status;
            Assert.IsTrue(status == 200);
        }

        [Test]
        [TestCase(68)] // id existente
        public void TestGetByIdTrue(int id)
        {
            dynamic resposta = serviceTelefone.Get(id);
            dynamic objResposta = JObject.Parse(JsonConvert.SerializeObject(resposta));
            var status = objResposta.status;
            Assert.IsTrue(status == 200);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(1)]
        public void TestGetByIdFalse(int id)
        {
            dynamic resposta = serviceTelefone.Get(id);
            dynamic objResposta = JObject.Parse(JsonConvert.SerializeObject(resposta));
            var status = objResposta.status;
            Assert.IsFalse(status == 200);
        }

        [Test]
        [TestCase("(75) 74218-0986")]
        public void TestValidaTelefoneTrue(string telefone)
        {
            Assert.IsTrue(serviceTelefone.isTelefone(telefone));
        }

        [Test]
        [TestCase("450.")]
        [TestCase("")]
        [TestCase("A")]
        public void TestValidaTelefoneFalse(string telefone)
        {
            Assert.IsFalse(serviceTelefone.isTelefone(telefone));
        }

        [Test]
        [TestCase(0, "(75) 74218-0986", "Residencial", 44)]
        public void TestePostClienteTrue(int id, string numero, string tipo, int idCliente)
        {
            
            TelefoneModel telefone = new TelefoneModel(id, tipo, numero, idCliente);

            dynamic resposta = serviceTelefone.Post(telefone);
            dynamic objResposta = JObject.Parse(JsonConvert.SerializeObject(resposta));
            var status = objResposta.status;

            Assert.IsTrue(status == 200);
        }

        [Test]
        [TestCase(0, "(75) 74218-0986", "", 44)]
        [TestCase(0, "(75) 74218-0986", "Residencial", 0)]
        [TestCase(0, "(11", "Residencial", 0)]
        [TestCase(0, "(75) 74218-09", "Residencial", 44)]
        public void TestePostClienteFalse(int id, string numero, string tipo, int idCliente)
        {
            TelefoneModel telefone = new TelefoneModel(id, numero, tipo, idCliente);

            dynamic resposta = serviceTelefone.Post(telefone);
            dynamic objResposta = JObject.Parse(JsonConvert.SerializeObject(resposta));
            var status = objResposta.status;

            Assert.IsFalse(status == 200);
        }

        [Test]
        [TestCase(68, "(75) 74218-0986", "Residencial", 44)]
        public void TesteUpdateClienteTrue(int id, string numero, string tipo, int idCliente)
        {

            TelefoneModel telefone = new TelefoneModel(id, tipo, numero, idCliente);

            dynamic resposta = serviceTelefone.Update(telefone, id);
            dynamic objResposta = JObject.Parse(JsonConvert.SerializeObject(resposta));
            var status = objResposta.status;

            Assert.IsTrue(status == 200);
        }

        [Test]
        [TestCase(68, "(75) 74218-0986", "", 44)]
        [TestCase(68, "(75) 74218-0986", "Residencial", 0)]
        [TestCase(68, "(11", "Residencial", 0)]
        [TestCase(68, "(75) 74218-09", "Residencial", 44)]
        public void TesteUpdateClienteFalse(int id, string numero, string tipo, int idCliente)
        {
            TelefoneModel telefone = new TelefoneModel(id, numero, tipo, idCliente);

            dynamic resposta = serviceTelefone.Update(telefone, id);
            dynamic objResposta = JObject.Parse(JsonConvert.SerializeObject(resposta));
            var status = objResposta.status;

            Assert.IsFalse(status == 200);
        }


    }
}
