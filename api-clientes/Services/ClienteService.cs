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

        private IEnderecoRepository serviceE;
        private ITelefoneRepository serviceT;
        private IRdSocialRepository serviceRS;

        public ClienteService(IClienteRepository repositorio, IEnderecoRepository serviceE, ITelefoneRepository serviceT, IRdSocialRepository serviceRS)
        {
            this.repositorio = repositorio;
            this.serviceE = serviceE;
            this.serviceT = serviceT;
            this.serviceRS = serviceRS;
        }


        public dynamic GetAll()
        {
            var resposta = repositorio.GetAll();
            List<ClienteAllModel> listaClientes = new List<ClienteAllModel>();
            foreach (var cadaCliente in resposta)
            {
                var respostaT = serviceT.GetTelefonesCliente(cadaCliente.ID);
                var respostaE = serviceE.GetEnderecoCliente(cadaCliente.ID);
                var respostaRS = serviceRS.GetRdSocialCliente(cadaCliente.ID);

                listaClientes.Add(new ClienteAllModel(cadaCliente, respostaT, respostaE, respostaRS));
                
            }
            

            try
            {
                return new
                {
                    status = 200,
                    message = "Registros listados com sucesso",
                    data = listaClientes
                };
            }
            catch (Exception e)
            {
                return new
                {
                    status = 500,
                    message = "Erro ao listar registros: " + e.Message,
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
                var respostaE = serviceE.GetEnderecoCliente(id);
                var respostaRS = serviceRS.GetRdSocialCliente(id);

                ClienteAllModel cliente = new ClienteAllModel(resposta, respostaT,respostaE, respostaRS);

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
                        Cliente = cliente
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


        public dynamic Update(ClienteModel cliente, int id)
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
                } else if (id <= 0)
                {
                    return new
                    {
                        status = 500,
                        message = "ID Invalido",
                        data = cliente
                    };

                }

                var resposta = repositorio.Update(cliente, id);

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


        public dynamic GetPag(int pag, int quant)
        {
            try
            {
                int offset = pag * quant;
                var resposta = repositorio.GetPag(offset, quant);
                var tamanhoLista = repositorio.GetAll().Count();
                return new
                {
                    status = 200,
                    message = "Registro encontrado",
                    data = new { 
                        listaCliente = resposta,
                        quantTamLista = tamanhoLista
                    }
                };
            }
            catch (Exception)
            {
                return new
                {
                    status = 500,
                    message = "Erro ao encontrar registro",
                    data = pag 
                };
            }
        }

        public dynamic GetPag(int pag, int quant, string nome)
        {
            try
            {
                int offset = pag * quant;
                var resposta = repositorio.GetPag(offset, quant, nome);
                var tamanhoLista = repositorio.GetAll(nome).Count();
                return new
                {
                    status = 200,
                    message = "Registro encontrado",
                    data = new
                    {
                        listaCliente = resposta,
                        quantTamLista = tamanhoLista
                    }
                };
            }
            catch (Exception)
            {
                return new
                {
                    status = 500,
                    message = "Erro ao encontrar registro",
                    data = pag
                };
            }
        }


    }
}
