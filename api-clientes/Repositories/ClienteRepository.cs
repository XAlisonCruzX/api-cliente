using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_clientes.Database;
using api_clientes.Models;
using Dapper;

namespace api_clientes.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        public IConexao Conexao;

        //Ao instanciar recebe conexao com o DB
        public ClienteRepository(IConexao Conexao)
        {
            this.Conexao = Conexao;
        }

        // Insere os dados do cliente
        public int Add(ClienteModel cliente)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    // retorna o id do cliente
                    var resposta = cn.ExecuteScalar<int>(@"INSERT INTO CLIENTES (NOME, DATA_NASCIMENTO, CPF, RG) 
                                OUTPUT Inserted.ID
                                VALUES (@NOME, @DATA_NASCIMENTO, @CPF, @RG)", new
                    {
                        cliente.NOME,
                        cliente.DATA_NASCIMENTO,
                        cliente.CPF,
                        cliente.RG
                    });

                    return resposta;
                }
            }
            catch (Exception)
            {
                throw ;
            }
        }

        // Deleta clietne pelo id
        public int Delete(int id)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Execute(@"DELETE FROM CLIENTES WHERE ID = @id", new { id });
                    return resposta;
                }
            }
            catch (Exception)
            {
                throw;
            }
 
        }

       
        // Retorna cliente pelo id
        public ClienteModel Get(int id)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<ClienteModel>(@"SELECT * FROM CLIENTES WHERE ID = @id", new { id });

                    return resposta.FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Lista todos clientes
        public List<ClienteModel> GetAll()
        {

            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<ClienteModel>(@"SELECT * FROM CLIENTES");

                    return resposta.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        // lista todos cliente com filtro no nome
        public List<ClienteModel> GetAll(string nome)
        {

            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<ClienteModel>(@"SELECT * FROM CLIENTES WHERE NOME LIKE @nome", new { nome = "%"+nome+"%" });

                    return resposta.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }

        }



        // Atualiza dados do cliente pelo id
        public int Update(ClienteModel cliente, int id)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.ExecuteScalar<int>(@"UPDATE CLIENTES SET NOME = @NOME, DATA_NASCIMENTO = @DATA_NASCIMENTO, CPF = @CPF , RG = @RG WHERE ID = @id  ", new
                    {   
                        id,
                        cliente.NOME,
                        cliente.DATA_NASCIMENTO,
                        cliente.CPF,
                        cliente.RG
                    });

                    return resposta;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Retorna lista de clientes com paginacao
        // offset= numero do registro que começa a busca,
        // quant = quantidade de registros a buscar
        public List<ClienteModel> GetPag(int offset, int quant)
        {

            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<ClienteModel>(@"Select * from CLIENTES order by id OFFSET @offset ROWS FETCH NEXT @quant ROWS ONLY", new { 
                        offset,
                        quant

                    });
                    return resposta.ToList();
                }
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Retorna lista de clientes com paginacao com filtro pelo nome
        // offset= numero do registro que começa a busca,
        // quant = quantidade de registros a buscar
        // nome = filtro
        public List<ClienteModel> GetPag(int offset, int quant, string nome)
        {

            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<ClienteModel>(@"Select * from CLIENTES WHERE NOME LIKE @nome ORDER BY ID OFFSET @offset ROWS FETCH NEXT @quant ROWS ONLY", new
                    {
                        nome = "%"+nome+"%",
                        offset,
                        quant

                    });
                    return resposta.ToList();
                }

            }
            catch (Exception)
            {
                throw;
            }
        }



    }
}
