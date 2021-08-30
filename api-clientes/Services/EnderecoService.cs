using api_clientes.Models;
using api_clientes.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace api_clientes.Services
{
    public class EnderecoService
    {

        private IEnderecoRepository repositorio;

        public EnderecoService(IEnderecoRepository repositorio)
        {
            this.repositorio = repositorio;
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
            catch (Exception)
            {
                return new
                {
                    status = 500,
                    message = "Erro ao listar registros",
                    data = ""
                };
            }
        }


        public dynamic GetEnderecosCliente(int id_cliente)
        {
            try
            {
                return new
                {
                    status = 200,
                    message = "Registros listados com sucesso",
                    data = repositorio.GetEnderecoCliente(id_cliente)
                };
            }
            catch (Exception)
            {
                return new
                {
                    status = 500,
                    message = "Erro ao listar registros",
                    data = ""
                };
            }

        }

        public dynamic Get(int id) {
            try
            {
                var resposta = repositorio.Get(id);

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
                    data = resposta
                };
            }
            catch (Exception)
            {
                return new
                {
                    status = 500,
                    message = "Erro ao encontrar registro",
                    data = id
                };
            }
        }



        public dynamic Delete(int id)
        {
            try
            {
                if (Get(id).data.ID <= 0)
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
            catch (Exception)
            {
                return new
                {
                    status = 500,
                    message = "Erro ao realizar exclusão",
                    data = id,
                };
            }
        }



        public dynamic Post(EnderecoModel endereco)
        {
            try
            {
                if (String.IsNullOrEmpty(endereco.NUMERO) || String.IsNullOrEmpty(endereco.RUA) || String.IsNullOrEmpty(endereco.TIPO) || String.IsNullOrEmpty(endereco.CEP))
                {
                    return new
                    {
                        status = 500,
                        message = "Campo vazio",
                        data = endereco
                    };
                }
                //validacao de cep
                if (!isCep(endereco.CEP))
                {
                    return new
                    {
                        status = 500,
                        message = "Cep invalido",
                        data = endereco.CEP
                    };

                }

                var resposta = repositorio.Add(endereco);

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
                        data = endereco
                    };
                }
                else
                {
                    return new
                    {
                        status = 500,
                        message = "Erro ao criar registro: " + e.Message,
                        data = endereco
                    };
                }
            }
        }


        public dynamic Update(EnderecoModel endereco, int id)
        {
            try
            {
                if (String.IsNullOrEmpty(endereco.NUMERO) || String.IsNullOrEmpty(endereco.RUA) || String.IsNullOrEmpty(endereco.TIPO) || String.IsNullOrEmpty(endereco.CEP))
                {
                    return new
                    {
                        status = 500,
                        message = "Campo vazio",
                        data = endereco
                    };
                }

                if(endereco.ID_CLIENTE <= 0)
                {
                    return new
                    {
                        status = 500,
                        message = "Sem associação ao cliente",
                        data = endereco
                    };

                }

                if (!isCep(endereco.CEP))
                {
                    return new
                    {
                        status = 500,
                        message = "Cep invalido",
                        data = endereco.CEP
                    };

                }

                var resposta = repositorio.Update(endereco, id);

                return new
                {
                    status = 200,
                    message = "Registro Atualizado com sucesso",
                    data = Get(endereco.ID).data
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
                        data = endereco
                    };
                }
                else
                {
                    return new
                    {
                        status = 500,
                        message = "Erro ao Atualizar registro",
                        data = endereco
                    };
                }
            }
        }

        // validacao de cep por regex
        public bool isCep(string cep)
        {
            Regex validador = new Regex(@"(^(\d{5})-(\d{3})$)");
            MatchCollection matches = validador.Matches(cep);
            return matches.Count > 0;
        }



    }
}
