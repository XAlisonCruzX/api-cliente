using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Core.Services
{
    public class ClienteService : IClienteService
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
            //Para cada cliente adiciona suas listas de Telefones, Enderecos e Rede sociais
            foreach (var cadaCliente in resposta)
            {
                var respostaT = serviceT.GetTelefonesCliente(cadaCliente.ID);
                var respostaE = serviceE.GetEnderecoCliente(cadaCliente.ID);
                var respostaRS = serviceRS.GetRdSocialCliente(cadaCliente.ID);

                listaClientes.Add(new ClienteAllModel(cadaCliente, respostaT, respostaE, respostaRS));

            }

            // retorna um obj anonimo customizado como resposta
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
                if (repositorio.Get(id).ID <= 0)
                {
                    return new
                    {
                        status = 500,
                        message = "Registro não encontrado",
                        data = id
                    };
                }
                var resposta = repositorio.Get(id);
                // se nao encontrar
                if (resposta == null)
                {
                    return new
                    {
                        status = 500,
                        message = "Registro não encontrado",
                        data = id
                    };
                }

                var respostaT = serviceT.GetTelefonesCliente(id);
                var respostaE = serviceE.GetEnderecoCliente(id);
                var respostaRS = serviceRS.GetRdSocialCliente(id);

                // busca em todos repositorios a informaçoes do cliente baseado no id
                ClienteAllModel cliente = new ClienteAllModel(resposta, respostaT, respostaE, respostaRS);

                // se der certo
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
                // verifica se o cliente existe antes de apagar
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
                // verifica se existe algum campo vazio
                if (String.IsNullOrEmpty(cliente.NOME) || String.IsNullOrEmpty(cliente.RG) || String.IsNullOrEmpty(cliente.DATA_NASCIMENTO.ToString()) || String.IsNullOrEmpty(cliente.CPF))
                {
                    return new
                    {
                        status = 500,
                        message = "Campo vazio",
                        data = cliente
                    };
                }

                // validacao regex de cpf
                if (!isCpf(cliente.CPF))
                {
                    return new
                    {
                        status = 500,
                        message = "Cpf Invalido",
                        data = cliente
                    };
                }

                // validacao regex de rg
                if (!isRg(cliente.RG))
                {
                    return new
                    {
                        status = 500,
                        message = "Rg Invalido",
                        data = cliente
                    };
                }

                // Se chegar aqui, salva o cliente
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
                // verifica se existe algum campo vazio
                if (String.IsNullOrEmpty(cliente.NOME) || String.IsNullOrEmpty(cliente.RG) || String.IsNullOrEmpty(cliente.DATA_NASCIMENTO.ToString()) || String.IsNullOrEmpty(cliente.CPF))
                {
                    return new
                    {
                        status = 500,
                        message = "Campo vazio",
                        data = cliente
                    };


                    //verifica se id é valido
                }
                else if (id <= 0)
                {
                    return new
                    {
                        status = 500,
                        message = "ID Invalido",
                        data = cliente
                    };

                }

                // valida regex cpf
                if (!isCpf(cliente.CPF))
                {
                    return new
                    {
                        status = 500,
                        message = "Cpf Invalido",
                        data = cliente
                    };
                }

                // valida regex rg
                if (!isRg(cliente.RG))
                {
                    return new
                    {
                        status = 500,
                        message = "Rg Invalido",
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
                // tratamento de erro de registro duplicado(validacao apenas de id)
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
                //numero de paginas * a quantidade de registro por pagina = offset
                int offset = pag * quant;
                var resposta = repositorio.GetPag(offset, quant);
                // valida tamanho da lista para contar o fim da paginacao
                var tamanhoLista = repositorio.GetAll().Count();
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

        // busca de informações com paginacao e filtro de nome
        public dynamic GetPag(int pag, int quant, string nome)
        {
            try
            {
                //numero de paginas * a quantidade de registro por pagina = offset
                int offset = pag * quant;
                var resposta = repositorio.GetPag(offset, quant, nome);
                // valida tamanho da lista para contar o fim da paginacao
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

        // funcao de validacao para cpf utilizado regex
        private bool isCpf(string cpf)
        {
            Regex validador = new Regex(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$");

            MatchCollection matches = validador.Matches(cpf);
            return matches.Count > 0;
        }

        // funcao de validacao para rg utilizado regex
        private bool isRg(string rg)
        {
            Regex validador = new Regex(@"(^(\d{2}\x2E\d{3}\x2E\d{3}[-]\d{1})$|^(\d{2}\x2E\d{3}\x2E\d{3})$)");

            MatchCollection matches = validador.Matches(rg);
            return matches.Count > 0;

        }


    }
}
