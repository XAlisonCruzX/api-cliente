using api_clientes.Database;
using api_clientes.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_clientes.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        public IConexao Conexao;

        public EnderecoRepository(IConexao Conexao)
        {
            this.Conexao = Conexao;
        }


        public int Add(EnderecoModel endereco)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.ExecuteScalar<int>(@"INSERT INTO ENDERECOS (TIPO, CEP, RUA, NUMERO, ID_CLIENTE) 
                                OUTPUT Inserted.ID
                                VALUES (@TIPO, @CEP, @RUA, @NUMERO, @ID_CLIENTE)", new
                    {
                        endereco.TIPO,
                        endereco.CEP,
                        endereco.RUA,
                        endereco.NUMERO,
                        endereco.ID_CLIENTE
                    });

                    return resposta;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Delete(int id)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Execute(@"DELETE FROM ENDERECOS WHERE ID = @id", new { id });
                    return resposta;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public EnderecoModel Get(int id)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<EnderecoModel>(@"SELECT * FROM ENDERECOS WHERE ID = @id", new { id });

                    return resposta.FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<EnderecoModel> GetAll()
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<EnderecoModel>(@"SELECT * FROM ENDERECOS");

                    return resposta.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<EnderecoModel> GetEnderecoCliente(int id_cliente)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<EnderecoModel>(@"SELECT * FROM ENDERECOS WHERE ID_CLIENTE = @id", new { id = id_cliente });

                    return resposta.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Update(EnderecoModel endereco)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.ExecuteScalar<int>(@"UPDATE ENDERECOS SET TIPO = @TIPO, CEP = @CEP, RUA = @RUA, NUMERO = @NUMERO, ID_CLIENTE = @ID_CLIENTE WHERE ID = @ID  ", new
                    {
                        endereco.ID,
                        endereco.TIPO,
                        endereco.CEP,
                        endereco.RUA,
                        endereco.NUMERO,
                        endereco.ID_CLIENTE
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
