using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace api_clientes.Database
{
    public interface IConexao
    {
        IDbConnection AbrirConexao();
    }
}