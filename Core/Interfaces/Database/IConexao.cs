using System.Data;

namespace Core.Interface.Database
{
    public interface IConexao
    {
        IDbConnection AbrirConexao();
    }
}