using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_clientes.Models;

namespace api_clientes.Repositories
{
    public interface ITelefoneRepository
    {
        int Add(TelefoneModel telefone);

        List<TelefoneModel> GetAll();

        List<TelefoneModel> GetTelefonesCliente(int id_cliente);
        TelefoneModel Get(int id);

        int Update(TelefoneModel telefone, int id);

        int Delete(int id);
    }
}
