using Core.Models;

namespace Core.Interfaces.Services
{
    public interface IEnderecoService
    {
        dynamic GetAll();
        dynamic GetEnderecosCliente(int id_cliente);
        dynamic Get(int id);
        dynamic Delete(int id);
        dynamic Post(EnderecoModel endereco);
        dynamic Update(EnderecoModel endereco, int id);
    }
}
