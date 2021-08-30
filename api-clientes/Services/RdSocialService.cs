using api_clientes.Models;
using api_clientes.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_clientes.Services
{
    public class RdSocialService
    {

        private IRdSocialRepository repositorio;

        public RdSocialService(IRdSocialRepository repositorio)
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


        public dynamic GetRdSocialCliente(int id_cliente)
        {
            try
            {
                return new
                {
                    status = 200,
                    message = "Registros listados com sucesso",
                    data = repositorio.GetRdSocialCliente(id_cliente)
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



        public dynamic Post(RedeSocialModel rdSocial)
        {
            try
            {
                if (String.IsNullOrEmpty(rdSocial.NOME) || String.IsNullOrEmpty(rdSocial.REFERENCIA))
                {
                    return new
                    {
                        status = 500,
                        message = "Campo vazio",
                        data = rdSocial
                    };
                }

                if (rdSocial.ID_CLIENTE <= 0)
                {
                    return new
                    {
                        status = 500,
                        message = "Sem associação ao cliente",
                        data = rdSocial
                    };

                }

                var resposta = repositorio.Add(rdSocial);

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
                        data = rdSocial
                    };
                }
                else
                {
                    return new
                    {
                        status = 500,
                        message = "Erro ao criar registro",
                        data = rdSocial
                    };
                }
            }
        }


        public dynamic Update(RedeSocialModel rdSocial, int id)
        {
            try
            {
                if (String.IsNullOrEmpty(rdSocial.NOME) || String.IsNullOrEmpty(rdSocial.REFERENCIA))
                {
                    return new
                    {
                        status = 500,
                        message = "Campo vazio",
                        data = rdSocial
                    };
                }

                if (rdSocial.ID_CLIENTE <= 0)
                {
                    return new
                    {
                        status = 500,
                        message = "Sem associação ao cliente",
                        data = rdSocial
                    };

                }

                var resposta = repositorio.Update(rdSocial, id);

                return new
                {
                    status = 200,
                    message = "Registro Atualizado com sucesso",
                    data = Get(rdSocial.ID).data
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
                        data = rdSocial
                    };
                }
                else
                {
                    return new
                    {
                        status = 500,
                        message = "Erro ao Atualizar registro",
                        data = rdSocial
                    };
                }
            }
        }

    }
}
