using Core.Models;

namespace Core.Interfaces.Services
{
    public interface IClienteService
    {
        dynamic GetAll();
        dynamic Get(int id);
        dynamic Delete(int id);
        dynamic Post(ClienteModel cliente);
        dynamic Update(ClienteModel cliente, int id);
        dynamic GetPag(int pag, int quant);
        dynamic GetPag(int pag, int quant, string nome);
    }
}
