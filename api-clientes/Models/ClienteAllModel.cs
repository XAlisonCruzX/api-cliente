using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_clientes.Models
{
    public class ClienteAllModel
    {
        private int _id;
        private string _nome;
        private DateTime _dataNascimento;
        private string _cpf;
        private string _rg;
        private List<TelefoneModel> _listaTelefone;
        private List<EnderecoModel> _listaEndereco;
        private List<RedeSocialModel> _listaRdSocial;


        public ClienteAllModel()
        {


        }


        public ClienteAllModel(int Id, string Nome, DateTime DataNascimento, string Cpf, string Rg)
        {
            ID = Id;
            NOME = Nome;
            DATA_NASCIMENTO = DataNascimento;
            CPF = Cpf;
            RG = Rg;



        }
        public ClienteAllModel(ClienteModel cliente, List<TelefoneModel> listaTelefone, List<EnderecoModel> listaEndereco, List<RedeSocialModel> listaRdSocial)
        {
            ID = cliente.ID;
            NOME = cliente.NOME;
            DATA_NASCIMENTO = cliente.DATA_NASCIMENTO;
            CPF = cliente.CPF;
            RG = cliente.RG;
            Telefones = listaTelefone;
            Enderecos = listaEndereco;
            RedesSociais = listaRdSocial;
        }


        public int ID { get => _id; set => _id = value;}
        public string NOME { get => _nome; set => _nome = value; }
        public DateTime DATA_NASCIMENTO { get => _dataNascimento; set => _dataNascimento = value; }
        public string CPF { get => _cpf; set => _cpf = value; }
        public string RG { get => _rg; set => _rg = value; }
        public List<TelefoneModel> Telefones { get => _listaTelefone; set => _listaTelefone = value; }
        public List<EnderecoModel> Enderecos { get => _listaEndereco; set => _listaEndereco = value; }
        public List<RedeSocialModel> RedesSociais { get => _listaRdSocial; set => _listaRdSocial = value; }
    }
}
