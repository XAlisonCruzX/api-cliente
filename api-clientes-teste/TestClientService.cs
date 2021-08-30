using api_clientes.Controllers;
using api_clientes.Database;
using api_clientes.Models;
using api_clientes.Repositories;
using api_clientes.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;

namespace api_clientes_teste
{
    // Mudar id nos testes que exigem dados ja criados
    public class TestsClientesService
    {
        static Conexao conn = new Conexao("Server=localhost;Database=base-clientes;Trusted_Connection=True;");
        static ClienteService serviceCliente = new ClienteService(new ClienteRepository(conn), new EnderecoRepository(conn), new TelefoneRepository(conn), new RdSocialRepository(conn));
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TestGetAll()
        {
            dynamic resposta = serviceCliente.GetAll();
            dynamic objResposta = JObject.Parse(JsonConvert.SerializeObject(resposta));
            var status = objResposta.status;
            Assert.IsTrue(status == 200);
        }

        [Test]
        [TestCase(44)] // id existente
        public void TestGetByIdTrue(int id)
        {
            dynamic resposta = serviceCliente.Get(id);
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
            dynamic resposta = serviceCliente.Get(id);
            dynamic objResposta = JObject.Parse(JsonConvert.SerializeObject(resposta));
            var status = objResposta.status;
            Assert.IsFalse(status == 200);
        }

        [Test]
        [TestCase("637.552.150-55")]
        public void TestValidaCpfTrue(string cpf)
        {
            Assert.IsTrue(serviceCliente.isCpf(cpf));
        }

        [Test]
        [TestCase("637.552.150-5522")]
        [TestCase("")]
        [TestCase("A")]
        public void TestValidaCpfFalse(string cpf)
        {
            Assert.IsFalse(serviceCliente.isCpf(cpf));

        }

        [Test]
        [TestCase("34.632.226-1")]
        public void TestValidaRgTrue(string rg)
        {
            Assert.IsTrue(serviceCliente.isRg(rg));
        }

        [Test]
        [TestCase("34.632.226-12")]
        [TestCase("")]
        [TestCase("A")]
        public void TestValidaRgFalse(string rg)
        {
            Assert.IsFalse(serviceCliente.isRg(rg));

        }

        [Test]
        [TestCase(0, "Alison", "1999-11-12", "637.552.150-55", "34.632.226-1")]
        public void TestePostClienteTrue(int id, string nome, string strDataNascimento, string cpf, string rg)
        {
            DateTime dataNascimento = DateTime.Parse(strDataNascimento);
            ClienteModel cliente = new ClienteModel(id, nome, dataNascimento, cpf, rg);

            dynamic resposta = serviceCliente.Post(cliente);
            dynamic objResposta = JObject.Parse(JsonConvert.SerializeObject(resposta));
            var status = objResposta.status;

            Assert.IsTrue(status == 200);
        }

        [Test]
        [TestCase(0, "Alison", "1999-11-12", "637552.150-55", "34.632.226-1")]
        [TestCase(0, "Alison", "1999-11-12", "637.552.150-55", "34.632226-1")]
        public void TestePostClienteFalse(int id, string nome, string strDataNascimento, string cpf, string rg)
        {
            DateTime dataNascimento = DateTime.Parse(strDataNascimento);
            ClienteModel cliente = new ClienteModel(id, nome, dataNascimento, cpf, rg);

            dynamic resposta = serviceCliente.Post(cliente);
            dynamic objResposta = JObject.Parse(JsonConvert.SerializeObject(resposta));
            var status = objResposta.status;

            Assert.IsFalse(status == 200);
        }

        [Test]
        [TestCase(44, "Claudio", "1999-11-12", "637.552.150-55", "34.632.226-1")]
        public void TesteUpdateClienteTrue(int id, string nome, string strDataNascimento, string cpf, string rg)
        {
            DateTime dataNascimento = DateTime.Parse(strDataNascimento);
            ClienteModel cliente = new ClienteModel(id, nome, dataNascimento, cpf, rg);

            dynamic resposta = serviceCliente.Update(cliente, id);
            dynamic objResposta = JObject.Parse(JsonConvert.SerializeObject(resposta));
            var status = objResposta.status;

            Assert.IsTrue(status == 200);
        }

        [Test]
        [TestCase(0, "Claudio", "1999-11-12", "637.552.150-55", "34.632.226-1")]
        [TestCase(0, "Claudio", "1999-11-12", "637.552.150-55", "34.632.226-1")]
        [TestCase(44, "Claudio", "1999-11-12", "637.552150-55", "34.632.226-1")]
        [TestCase(44, "Claudio", "1999-11-12", "637.552.150-55", "34632.226-1")]
        public void TesteUpdateClienteFalse(int id, string nome, string strDataNascimento, string cpf, string rg)
        {
            DateTime dataNascimento = DateTime.Parse(strDataNascimento);
            ClienteModel cliente = new ClienteModel(id, nome, dataNascimento, cpf, rg);

            dynamic resposta = serviceCliente.Update(cliente, id);
            dynamic objResposta = JObject.Parse(JsonConvert.SerializeObject(resposta));
            var status = objResposta.status;

            Assert.IsFalse(status == 200);
        }





    }
}