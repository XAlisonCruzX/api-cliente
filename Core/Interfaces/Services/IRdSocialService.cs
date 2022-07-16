using Core.Models;

namespace Core.Interfaces.Services
{
    public interface IRdSocialService
    {
        dynamic GetAll();
        dynamic GetRdSocialCliente(int id_cliente);
        dynamic Get(int id);
        dynamic Delete(int id);
        dynamic Post(RedeSocialModel rdSocial);
        dynamic Update(RedeSocialModel rdSocial, int id);
    }
}
