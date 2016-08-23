using ClientsAPI.Domain.Models;
using System.Collections.Generic;

namespace ClientsAPI.Business.Services
{
    public interface IClientServices
    {
        ClientResult Insert(Client client);
        ClientResult Update(Client client);
        IEnumerable<Client> GetAllClients();
        IEnumerable<Client> GetClientsByName(string name);
        Client GetClientByCpf(string cpf);

    }
}
