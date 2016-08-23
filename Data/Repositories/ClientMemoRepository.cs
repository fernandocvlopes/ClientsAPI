using ClientsAPI.Domain.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace ClientsAPI.Data.Repositories
{
    public class ClientMemoRepository : IClientRepository
    {
        private string _jsonData;

        private IList<Client> clients;
        public IList<Client> Clients
        {
            get
            {
                if (clients == null)
                    clients = JsonConvert.DeserializeObject<IList<Client>>(_jsonData);

                if (clients == null)
                    clients = new List<Client>();

                return clients;
            }
        }

        public ClientMemoRepository()
        {
            _jsonData = string.Empty;
        }

        private void SaveChanges()
        {
            _jsonData = JsonConvert.SerializeObject(clients.ToArray(), Formatting.Indented);
        }

        public void Insert(Client client)
        {
            this.Clients.Add(client);
            this.SaveChanges();
        }

        public void Update(Client client)
        {
            this.Clients[this.Clients.ToList().FindIndex(c => c.Cpf == client.Cpf)] = client;
            this.SaveChanges();
        }

        public IEnumerable<Client> GetAll()
        {
            return this.Clients.OrderBy(t => t.Name).ToList();
        }

        public IEnumerable<Client> GetClientsByName(string name)
        {
            return this.Clients
                                .OrderBy(c => c.Name)
                                .Where(c => c.Name == name)
                                .ToList();
        }

        public Client FindById(string cpf)
        {
            return this.Clients.Where(c => c.Cpf == cpf).SingleOrDefault();
        }

        public void ClearData()
        {
            _jsonData = string.Empty;
            clients = null;

        }

    }
}
