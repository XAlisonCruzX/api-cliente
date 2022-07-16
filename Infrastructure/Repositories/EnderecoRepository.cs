using Core.Interface.Database;
using Core.Interfaces.Repositories;
using Core.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace api_clientes.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        public IConexao Conexao;

        public EnderecoRepository(IConexao Conexao)
        {
            this.Conexao = Conexao;
        }

        // Adiciona endereco amarrado a algum cliente
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

        // Deleta endereco pelo id
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

        //Retornar endereco pelo id
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

        // lista todos enderecos
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

        //lista todos enderecos por id cliente
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

        // atualiza dados do endereco
        public int Update(EnderecoModel endereco, int id)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.ExecuteScalar<int>(@"UPDATE ENDERECOS SET TIPO = @TIPO, CEP = @CEP, RUA = @RUA, NUMERO = @NUMERO, ID_CLIENTE = @ID_CLIENTE WHERE ID = @id  ", new
                    {
                        id,
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
