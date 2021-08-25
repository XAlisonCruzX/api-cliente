using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_clientes.Models;
namespace api_clientes.Repositories
{
    public interface IRdSocialRepository
    {
        int Add(RedeSocialModel redeSocial);

        List<RedeSocialModel> GetAll();

        List<RedeSocialModel> GetRdSocialCliente(int id_cliente);

        RedeSocialModel Get(int id);

        int Update(RedeSocialModel redeSocial);

        int Delete(int id);
    }
}
