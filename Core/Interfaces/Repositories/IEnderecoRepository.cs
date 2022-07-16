using Core.Models;
using System.Collections.Generic;

namespace Core.Interfaces.Repositories
{
    public interface IEnderecoRepository
    {

        int Add(EnderecoModel endereco);

        List<EnderecoModel> GetAll();

        EnderecoModel Get(int id);

        List<EnderecoModel> GetEnderecoCliente(int id_cliente);

        int Update(EnderecoModel endereco, int id);

        int Delete(int id);

    }
}
