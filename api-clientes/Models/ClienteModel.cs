using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_clientes.Models
{
    public class ClienteModel
    {
        private int _id;
        private string _nome;
        private DateTime _dataNascimento;
        private string _cpf;
        private string _rg;



        public ClienteModel()
        {


        }


        public ClienteModel(int Id, string Nome, DateTime DataNascimento, string Cpf, string Rg)
        {
            ID = Id;
            NOME = Nome;
            DATA_NASCIMENTO = DataNascimento;
            CPF = Cpf;
            RG = Rg;
        }


        public int ID { get => _id; set => _id = value;}
        public string NOME { get => _nome; set => _nome = value; }
        public DateTime DATA_NASCIMENTO { get => _dataNascimento; set => _dataNascimento = value; }
        public string CPF { get => _cpf; set => _cpf = value; }
        public string RG { get => _rg; set => _rg = value; }
    }
}
