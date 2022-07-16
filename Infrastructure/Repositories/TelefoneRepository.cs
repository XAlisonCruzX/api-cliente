using Core.Interface.Database;
using Core.Interfaces.Repositories;
using Core.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace api_clientes.Repositories
{
    public class TelefoneRepository : ITelefoneRepository
    {
        public IConexao Conexao;

        public TelefoneRepository(IConexao Conexao)
        {
            this.Conexao = Conexao;
        }

        // Adiciona telefone amarrada a algum cliente
        public int Add(TelefoneModel telefone)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.ExecuteScalar<int>(@"INSERT INTO TELEFONES (TIPO, NUMERO, ID_CLIENTE) 
                                OUTPUT Inserted.ID
                                VALUES (@TIPO, @NUMERO, @ID_CLIENTE)", new
                    {
                        telefone.TIPO,
                        telefone.NUMERO,
                        telefone.ID_CLIENTE
                    });

                    return resposta;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Deleta telefone pelo id
        public int Delete(int id)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Execute(@"DELETE FROM TELEFONES WHERE ID = @id", new { id });
                    return resposta;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // retorna telefone pelo id
        public TelefoneModel Get(int id)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<TelefoneModel>(@"SELECT * FROM TELEFONES WHERE ID = @id", new { id });

                    return resposta.FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // lista todos telefones
        public List<TelefoneModel> GetAll()
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<TelefoneModel>(@"SELECT * FROM TELEFONES");

                    return resposta.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //lista todos telefones amarrados a um cliente
        public List<TelefoneModel> GetTelefonesCliente(int id_cliente)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<TelefoneModel>(@"SELECT * FROM TELEFONES WHERE ID_CLIENTE = @id", new { id = id_cliente });

                    return resposta.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Atualiza telefone pelo id
        public int Update(TelefoneModel telefone, int id)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.ExecuteScalar<int>(@"UPDATE TELEFONES SET TIPO = @TIPO, NUMERO = @NUMERO, ID_CLIENTE = @ID_CLIENTE WHERE ID = @id  ", new
                    {
                        id,
                        telefone.TIPO,
                        telefone.NUMERO,
                        telefone.ID_CLIENTE
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
