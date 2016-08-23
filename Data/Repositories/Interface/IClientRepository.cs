using ClientsAPI.Domain.Models;
using System.Collections.Generic;

namespace ClientsAPI.Data.Repositories
{
    public interface IClientRepository : IRepository<Client>
    {
        IEnumerable<Client> GetClientsByName(string name);
    }
}
