using Core.Models;
using System.Collections.Generic;

namespace Core.Interfaces.Repositories
{
    public interface IRdSocialRepository
    {
        int Add(RedeSocialModel redeSocial);

        List<RedeSocialModel> GetAll();

        List<RedeSocialModel> GetRdSocialCliente(int id_cliente);

        RedeSocialModel Get(int id);

        int Update(RedeSocialModel redeSocial, int id);

        int Delete(int id);
    }
}
