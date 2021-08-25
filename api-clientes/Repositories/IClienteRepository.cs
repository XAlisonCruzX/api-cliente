using api_clientes.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_clientes.Repositories
{
    public interface IClienteRepository
    {

        int Add(ClienteModel cliente);

        List<ClienteModel> GetAll();
        ClienteModel Get(int id);

        int Update(ClienteModel cliente);

        int Delete(int id);

        

    }
}
