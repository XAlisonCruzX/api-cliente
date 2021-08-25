using api_clientes.Models;
using api_clientes.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_clientes.Services
{
    public class ClienteService
    {

        private IClienteRepository repositorio;

        private EnderecoService serviceE;
        private TelefoneService serviceT;
        private RdSocialService serviceRS;

        public ClienteService(IClienteRepository repositorio, EnderecoService serviceE, TelefoneService serviceT, RdSocialService serviceRS)
        {
            this.repositorio = repositorio;
            this.serviceE = serviceE;
            this.serviceT = serviceT;
            this.serviceRS = serviceRS;
        }


        public dynamic GetAll()
        {
            try
            {
                return new
                {
                    status = 200,
                    message = "Registros listados com sucesso",
                    data = repositorio.GetAll()
                };
            }
            catch (Exception e)
            {
                return new
                {
                    status = 500,
                    message = "Erro ao listar registros: " + e.Message ,
                    data = ""
                };
            }
        }
       


        public dynamic Get(int id)
        {
            try
            {


                var resposta = repositorio.Get(id);

                var respostaT = serviceT.GetTelefonesCliente(id);

                var respostaE = serviceE.GetEnderecosCliente(id);

                var respostaRS = serviceRS.GetRdSocialCliente(id);


                if (resposta == null)
                {
                    return new
                    {
                        status = 500,
                        message = "Registro não encontrado",
                        data = id
                    };
                }

                return new
                {
                    status = 200,
                    message = "Registro encontrado",
                    data = new
                    {
                        Cliente = resposta,
                        Telefones = respostaT.data,
                        Enderecos = respostaE.data,
                        RedesSociais = respostaRS.data
                    }
                };
            }
            catch (Exception e)
            {
                return new
                {
                    status = 500,
                    message = "Erro ao encontrar registro: " + e.Message,
                    data = id
                };
            }
        }


       


        public dynamic Delete(int id)
        {
            try
            {
                if (Get(id).data.Cliente.ID <= 0)
                {
                    return new
                    {
                        status = 500,
                        message = "Registro não encontrado",
                        data = id,
                    };
                }

                repositorio.Delete(id);

                return new
                {
                    status = 200,
                    message = "Exclusão realizada com sucesso",
                    data = id,
                };
            }
            catch (Exception e)
            {
                return new
                {
                    status = 500,
                    message = "Erro ao realizar exclusão" + e.Message,
                    data = id,
                };
            }
        }



        public dynamic Post(ClienteModel cliente)
        {
            try
            {
                if (String.IsNullOrEmpty(cliente.NOME) || String.IsNullOrEmpty(cliente.RG) || String.IsNullOrEmpty(cliente.DATA_NASCIMENTO.ToString()) || String.IsNullOrEmpty(cliente.CPF))
                {
                    return new
                    {
                        status = 500,
                        message = "Campo vazio",
                        data = cliente
                    };
                }

                var resposta = repositorio.Add(cliente);

                return new
                {
                    status = 200,
                    message = "Registro criado com sucesso",
                    data = Get(resposta).data
                };
            }
            catch (Exception e)
            {
                if (e.Message.Contains("Violação da restrição UNIQUE KEY"))
                {
                    return new
                    {
                        status = 500,
                        message = "Erro ao criar registro: Registro duplicado",
                        data = cliente
                    };
                }
                else
                {
                    return new
                    {
                        status = 500,
                        message = "Erro ao criar registro",
                        data = cliente
                    };
                }
            }
        }


        public dynamic Update(ClienteModel cliente)
        {
            try
            {
                if (String.IsNullOrEmpty(cliente.NOME) || String.IsNullOrEmpty(cliente.RG) || String.IsNullOrEmpty(cliente.DATA_NASCIMENTO.ToString()) || String.IsNullOrEmpty(cliente.CPF))
                {
                    return new
                    {
                        status = 500,
                        message = "Campo vazio",
                        data = cliente
                    };
                }

                var resposta = repositorio.Update(cliente);

                return new
                {
                    status = 200,
                    message = "Registro Atualizado com sucesso",
                    data = Get(cliente.ID).data
                };
            }
            catch (Exception e)
            {
                if (e.Message.Contains("Violação da restrição UNIQUE KEY"))
                {
                    return new
                    {
                        status = 500,
                        message = "Erro ao Atualizar registro: Registro duplicado",
                        data = cliente
                    };
                }
                else
                {
                    return new
                    {
                        status = 500,
                        message = "Erro ao Atualizar registro",
                        data = cliente
                    };
                }
            }
        }


    }
}
