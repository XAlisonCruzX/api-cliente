using Core.Models;
using System.Collections.Generic;

namespace Core.Interfaces.Repositories
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
