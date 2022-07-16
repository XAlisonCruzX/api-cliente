using Core.Models;

namespace Core.Interfaces.Services
{
    public interface ITelefoneService
    {
        dynamic GetAll();
        dynamic GetTelefonesCliente(int id_cliente);
        dynamic Get(int id);
        dynamic Delete(int id);
        dynamic Post(TelefoneModel telefone);
        dynamic Update(TelefoneModel telefone, int id);
    }
}
