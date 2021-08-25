using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_clientes.Models
{
    public class TelefoneModel
    {

        private int _id;
        private string _tipo;
        private string _numero;
        private int _idCliente;


        public TelefoneModel()
        {

        }

        public TelefoneModel(int id, string tipo, string numero, int idCliente)
        {
            ID = id;
            TIPO = tipo;
            NUMERO = numero;
            ID_CLIENTE = idCliente;
        }



        public int ID { get => _id; set => _id = value;}
        public string TIPO { get => _tipo; set => _tipo = value; }
        public string NUMERO { get => _numero; set => _numero = value; }

        public int ID_CLIENTE { get => _idCliente; set => _idCliente = value; }
    }
}
