﻿using ClientsAPI.Domain.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Configuration;

namespace ClientsAPI.Data.Repositories
{
    public class ClientFileRepository : IClientRepository
    {
        private readonly string FILEPATH = ConfigurationManager.AppSettings["StorageFilePath"].ToString();

        private IList<Client> clients;
        public IList<Client> Clients
        {
            get
            {
                if (clients == null)
                    clients = JsonConvert.DeserializeObject<IList<Client>>(File.ReadAllText(FILEPATH));

                if (clients == null)
                    clients = new List<Client>();

                return clients;
            }
        }

        public ClientFileRepository()
        {
            this.EnsureCreated();
        }

        private void EnsureCreated()
        {
            if (!File.Exists(FILEPATH))
                File.WriteAllText(FILEPATH, "");
        }

        private void SaveChanges()
        {
            var json = JsonConvert.SerializeObject(clients.ToArray(), Formatting.Indented);
            //write string to file
            File.WriteAllText(FILEPATH, json);
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
            if (File.Exists(FILEPATH))
                File.Delete(FILEPATH);

            clients = null;

            this.EnsureCreated();
        }

    }
}
