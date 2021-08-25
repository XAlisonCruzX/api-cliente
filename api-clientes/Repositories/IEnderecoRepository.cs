using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_clientes.Models;

namespace api_clientes.Repositories
{
    public interface IEnderecoRepository
    {

        int Add(EnderecoModel endereco);

        List<EnderecoModel> GetAll();

        EnderecoModel Get(int id);

        List<EnderecoModel> GetEnderecoCliente(int id_cliente);

        int Update(EnderecoModel endereco);

        int Delete(int id);

    }
}
