using Core.Interface.Database;
using System;
using System.Data;
using System.Data.SqlClient;

namespace api_clientes.Database
{
    public class Conexao : IConexao
    {

        private string _connectionString;


        public Conexao(string ConnectionString)
        {
            _connectionString = ConnectionString;
        }

        public IDbConnection AbrirConexao()
        {
            try
            {
                // retorna conexao com o banco de dados apartir da string recebida
                var cn = new SqlConnection(_connectionString);
                return cn;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
