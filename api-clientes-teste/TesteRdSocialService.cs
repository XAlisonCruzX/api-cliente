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
    // Mudar id nos testes que exigem dados 
    class TesteRdSocialService
    {
        static Conexao conn = new Conexao("Server=localhost;Database=base-clientes;Trusted_Connection=True;");
        static RdSocialService serviceRedeSocial = new RdSocialService(new RdSocialRepository(conn));
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TestGetAll()
        {
            dynamic resposta = serviceRedeSocial.GetAll();
            dynamic objResposta = JObject.Parse(JsonConvert.SerializeObject(resposta));
            var status = objResposta.status;
            Assert.IsTrue(status == 200);
        }

        [Test]
        [TestCase(44)] // id existente
        public void TestGetByIdTrue(int id)
        {
            dynamic resposta = serviceRedeSocial.Get(id);
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
            dynamic resposta = serviceRedeSocial.Get(id);
            dynamic objResposta = JObject.Parse(JsonConvert.SerializeObject(resposta));
            var status = objResposta.status;
            Assert.IsFalse(status == 200);
        }

        [Test]
        [TestCase(0, "adand.wee", "Facebook", 44)]
        public void TestePostRdSocialTrue(int id, string nome, string referencia, int idCliente)
        {

            RedeSocialModel redeSocial = new RedeSocialModel(id, nome, referencia, idCliente);

            dynamic resposta = serviceRedeSocial.Post(redeSocial);
            dynamic objResposta = JObject.Parse(JsonConvert.SerializeObject(resposta));
            var status = objResposta.status;

            Assert.IsTrue(status == 200);
        }

        [Test]
        [TestCase(0, "", "", 44)]
        [TestCase(0, "Facebook", "dasda", 0)]
        [TestCase(0, "Facebook", "asdadasda", 0)]
        public void TestePostRdSocialFalse(int id, string nome, string referencia, int idCliente)
        {
            RedeSocialModel redeSocial = new RedeSocialModel(id, nome, referencia, idCliente);

            dynamic resposta = serviceRedeSocial.Post(redeSocial);
            dynamic objResposta = JObject.Parse(JsonConvert.SerializeObject(resposta));
            var status = objResposta.status;

            Assert.IsFalse(status == 200);
        }

        [Test]
        [TestCase(44, "adand.wee", "Facebook", 44)]
        public void TesteUpdateClienteTrue(int id, string nome, string referencia, int idCliente)
        {

            RedeSocialModel redeSocial = new RedeSocialModel(id, nome, referencia, idCliente);

            dynamic resposta = serviceRedeSocial.Update(redeSocial, id);
            dynamic objResposta = JObject.Parse(JsonConvert.SerializeObject(resposta));
            var status = objResposta.status;

            Assert.IsTrue(status == 200);
        }

        [Test]
        [TestCase(0, "", "", 44)]
        [TestCase(0, "", "dasda", 0)]
        [TestCase(0, "Facebook", "asdadasda", 0)]
        public void TesteUpdateClienteFalse(int id, string nome, string referencia, int idCliente)
        {
            RedeSocialModel redeSocial = new RedeSocialModel(id, nome, referencia, idCliente);

            dynamic resposta = serviceRedeSocial.Update(redeSocial, id);
            dynamic objResposta = JObject.Parse(JsonConvert.SerializeObject(resposta));
            var status = objResposta.status;

            Assert.IsFalse(status == 200);
        }
    }
}
