using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Models
{
    public class EnderecoModel
    {

        private int _id;
        private string _tipo;
        private string _cep;
        private string _rua;
        private string _numero;
        private int _idCliente;


        public EnderecoModel()
        {


        }

        public EnderecoModel(int Id, string Tipo, string Cep, string Rua, string Numero, int IdCliente)
        {
            ID = Id;
            TIPO = Tipo;
            CEP = Cep;
            RUA = Rua;
            NUMERO = Numero;
            ID_CLIENTE = IdCliente;

        }



        public int ID { get => _id; set => _id = value;}
        public string TIPO { get => _tipo; set => _tipo = value; }
        public string CEP { get => _cep; set => _cep = value; }
        public string RUA { get => _rua; set => _rua = value; }
        public string NUMERO { get => _numero; set => _numero = value; }
        public int ID_CLIENTE { get => _idCliente; set => _idCliente = value; }



    }
}
