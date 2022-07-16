using Core.Models;
using System.Collections.Generic;

namespace Core.Interfaces.Repositories
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
