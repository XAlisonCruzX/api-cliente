using api_clientes.Database;
using api_clientes.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_clientes.Repositories
{
    public class RdSocialRepository : IRdSocialRepository
    {
        public IConexao Conexao;

        public RdSocialRepository(IConexao Conexao)
        {
            this.Conexao = Conexao;
        }

        public int Add(RedeSocialModel redeSocial)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.ExecuteScalar<int>(@"INSERT INTO REDES_SOCIAIS (NOME, REFERENCIA, ID_CLIENTE) 
                                OUTPUT Inserted.ID
                                VALUES (@NOME, @REFERENCIA, @ID_CLIENTE)", new
                    {
                        redeSocial.NOME,
                        redeSocial.REFERENCIA,
                        redeSocial.ID_CLIENTE
                    });

                    return resposta;
                }
            }
            catch (Exception )
            {
                throw ;
            }
        }

        public int Delete(int id)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Execute(@"DELETE FROM REDES_SOCIAIS WHERE ID = @id", new { id });
                    return resposta;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public RedeSocialModel Get(int id)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<RedeSocialModel>(@"SELECT * FROM REDES_SOCIAIS WHERE ID = @id", new { id });

                    return resposta.FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<RedeSocialModel> GetAll()
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<RedeSocialModel>(@"SELECT * FROM REDES_SOCIAIS");

                    return resposta.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<RedeSocialModel> GetRdSocialCliente(int id_cliente)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<RedeSocialModel>(@"SELECT * FROM REDES_SOCIAIS WHERE ID_CLIENTE = @id", new { id = id_cliente });

                    return resposta.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Update(RedeSocialModel redeSocial, int id)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.ExecuteScalar<int>(@"UPDATE REDES_SOCIAIS SET NOME = @NOME, REFERENCIA = @REFERENCIA, ID_CLIENTE = @ID_CLIENTE WHERE ID = @id  ", new
                    {
                        id,
                        redeSocial.NOME,
                        redeSocial.REFERENCIA,
                        redeSocial.ID_CLIENTE
                    });

                    return resposta;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
