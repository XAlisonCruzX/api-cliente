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
        int Update(ClienteModel cliente, int id);
        int Delete(int id);
        ClienteModel Get(int id);
        List<ClienteModel> GetAll();
        List<ClienteModel> GetAll(string nome);
        List<ClienteModel> GetPag(int pag, int quant);
        List<ClienteModel> GetPag(int pag, int quant, string nome);

    }
}
