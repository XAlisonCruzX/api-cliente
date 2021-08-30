using api_clientes.Models;
using api_clientes.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace api_clientes.Services
{
    public class TelefoneService
    {
        private ITelefoneRepository repositorio;

        public TelefoneService(ITelefoneRepository repositorio)
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


        public dynamic GetTelefonesCliente(int id_cliente)
        {
            try
            {
                return new
                {
                    status = 200,
                    message = "Registros listados com sucesso",
                    data = repositorio.GetTelefonesCliente(id_cliente)
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

        public dynamic Get(int id)
        {
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



        public dynamic Post(TelefoneModel telefone)
        {
            try
            {
                if (String.IsNullOrEmpty(telefone.NUMERO) || String.IsNullOrEmpty(telefone.TIPO))
                {
                    return new
                    {
                        status = 500,
                        message = "Campo vazio",
                        data = telefone
                    };
                }

                if (telefone.ID_CLIENTE <= 0)
                {
                    return new
                    {
                        status = 500,
                        message = "Sem associação ao cliente",
                        data = telefone
                    };

                }

                //validacao de telefone
                if (!isTelefone(telefone.NUMERO))
                {
                    return new
                    {
                        status = 500,
                        message = "Telefone invalido",
                        data = telefone.NUMERO
                    };
                }

                var resposta = repositorio.Add(telefone);

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
                        data = telefone
                    };
                }
                else
                {
                    return new
                    {
                        status = 500,
                        message = "Erro ao criar registro",
                        data = telefone
                    };
                }
            }
        }


        public dynamic Update(TelefoneModel telefone,int id)
        {
            try
            {
                if (String.IsNullOrEmpty(telefone.NUMERO) || String.IsNullOrEmpty(telefone.TIPO))
                {
                    return new
                    {
                        status = 500,
                        message = "Campo vazio",
                        data = telefone
                    };
                }

                if (telefone.ID_CLIENTE <= 0)
                {
                    return new
                    {
                        status = 500,
                        message = "Sem associação ao cliente",
                        data = telefone
                    };

                }

                if (!isTelefone(telefone.NUMERO))
                {
                    return new
                    {
                        status = 500,
                        message = "Telefone invalido",
                        data = telefone.NUMERO
                    };
                }

                var resposta = repositorio.Update(telefone, id);

                return new
                {
                    status = 200,
                    message = "Registro Atualizado com sucesso",
                    data = Get(telefone.ID).data
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
                        data = telefone
                    };
                }
                else
                {
                    return new
                    {
                        status = 500,
                        message = "Erro ao Atualizar registro",
                        data = telefone
                    };
                }
            }
        }

        public bool isTelefone(string telefone)
        {
            Regex validador = new Regex(@"(\(?\d{2}\)?\s)?(\d{4,5}\-\d{4})");
            MatchCollection matches = validador.Matches(telefone);
            return matches.Count > 0;
        }



    }
}
