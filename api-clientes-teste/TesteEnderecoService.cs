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
    class TesteEnderecoService
    {
        static Conexao conn = new Conexao("Server=localhost;Database=base-clientes;Trusted_Connection=True;");
        static EnderecoService serviceEndereco = new EnderecoService(new EnderecoRepository(conn));
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TestGetAll()
        {
            dynamic resposta = serviceEndereco.GetAll();
            dynamic objResposta = JObject.Parse(JsonConvert.SerializeObject(resposta));
            var status = objResposta.status;
            Assert.IsTrue(status == 200);
        }

        [Test]
        [TestCase(10)] // id existente
        public void TestGetByIdTrue(int id)
        {
            dynamic resposta = serviceEndereco.Get(id);
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
            dynamic resposta = serviceEndereco.Get(id);
            dynamic objResposta = JObject.Parse(JsonConvert.SerializeObject(resposta));
            var status = objResposta.status;
            Assert.IsFalse(status == 200);
        }

        [Test]
        [TestCase("40330-050")]
        public void TestValidaTelefoneTrue(string cep)
        {
            Assert.IsTrue(serviceEndereco.isCep(cep));
        }

        [Test]
        [TestCase("450-0000")]
        [TestCase("")]
        [TestCase("A")]
        public void TestValidaTelefoneFalse(string telefone)
        {
            Assert.IsFalse(serviceEndereco.isCep(telefone));
        }

        [Test]
        [TestCase(0, "Residencial", "40330-050", "Rua F", "66", 44)]     
        public void TestePostEnderecoTrue(int id, string tipo, string cep, string rua, string numero, int idCliente)
        {

            EnderecoModel telefone = new EnderecoModel(id, tipo, cep, rua, numero, idCliente);

            dynamic resposta = serviceEndereco.Post(telefone);
            dynamic objResposta = JObject.Parse(JsonConvert.SerializeObject(resposta));
            var status = objResposta.status;

            Assert.IsTrue(status == 200);
        }

        [Test]
        [TestCase(10, "Residencial", "40330050", "Rua F", "66", 44)]
        [TestCase(10, "Residencial", "40330-050", "Rua F", "66", 0)]
        [TestCase(10, "", "40330-050", "Rua F", "66", 44)]
        public void TestePostEnderecoFalse(int id, string tipo, string cep, string rua, string numero, int idCliente)
        {
            EnderecoModel endereco = new EnderecoModel(id, tipo, cep, rua, numero, idCliente);

            dynamic resposta = serviceEndereco.Post(endereco);
            dynamic objResposta = JObject.Parse(JsonConvert.SerializeObject(resposta));
            var status = objResposta.status;

            Assert.IsFalse(status == 200);
        }

        [Test]
        [TestCase(0, "Residencial", "40330-050", "Rua F", "66", 44)]
        public void TesteUpdateEnderecoClienteTrue(int id, string tipo, string cep, string rua, string numero, int idCliente)
        {

            EnderecoModel endereco = new EnderecoModel(id, tipo, cep, rua, numero, idCliente);

            dynamic resposta = serviceEndereco.Update(endereco, id);
            dynamic objResposta = JObject.Parse(JsonConvert.SerializeObject(resposta));
            var status = objResposta.status;

            Assert.IsTrue(status == 200);
        }

        [Test]
        [TestCase(10, "Residencial", "40330050", "Rua F", "66", 44)]
        [TestCase(10, "Residencial", "40330-050", "Rua F", "66", 0)]
        [TestCase(10, "", "40330-050", "Rua F", "66", 44)]
        public void TesteUpdateEnderecoFalse(int id, string tipo, string cep, string rua, string numero, int idCliente)
        {
            EnderecoModel endereco = new EnderecoModel(id, tipo, cep, rua, numero, idCliente);

            dynamic resposta = serviceEndereco.Update(endereco, id);
            dynamic objResposta = JObject.Parse(JsonConvert.SerializeObject(resposta));
            var status = objResposta.status;

            Assert.IsFalse(status == 200);
        }
    }
}
