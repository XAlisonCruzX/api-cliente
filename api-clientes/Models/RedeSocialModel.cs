using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_clientes.Models
{
    public class RedeSocialModel
    {
        private int _id;
        private string _nome;
        private string _referencia;
        private int _idCliente;

        public RedeSocialModel()
        {


        }

        public RedeSocialModel(int Id, string Nome, string Referencia, int IdCliente)
        {
            ID = Id;
            NOME = Nome;
            REFERENCIA = Referencia;
            ID_CLIENTE = IdCliente;

        }

        public int ID { get => _id; set => _id = value;}
        public string NOME { get => _nome; set => _nome = value;}
        public string REFERENCIA { get => _referencia; set => _referencia = value;}
        public int ID_CLIENTE { get => _idCliente; set => _idCliente = value;}



    }
}
