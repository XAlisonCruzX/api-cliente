using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

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
                var cn = new SqlConnection(_connectionString);
                return cn;


            }
            catch(Exception)
            {
                throw;
            }

        }
    }
}
