using ClientsAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientsAPI.Test
{
    public class ClientBuilder
    {
        private Client _client;

        public ClientBuilder()
        {
            _client = new Client();
        }

        public ClientBuilder(string name, string Cpf)
        {          
            _client = new Client();
            _client.Name = name;
            _client.Cpf = Cpf;
        }

        public ClientBuilder(string Cpf) : base()
        {
            _client = new Client();
            _client.Cpf = Cpf;
        }

        public ClientBuilder withValidCPF()
        {
            if (string.IsNullOrEmpty(_client.Cpf))
                _client.Cpf = "673.285.351-13";

            return this;
        }

        public ClientBuilder withValidAddress()
        {
            var clientAddress = new List<Address>();
            clientAddress.Add(new Address { Street = "Gold street, 1500", City = "Belo Horizonte", State = "MG", PostalCode = "30300-300" });
            _client.Address = clientAddress;

            return this;
        }

        public ClientBuilder withPhone()
        {
            var clientPhone = new List<string>();
            clientPhone.Add("91999-9090");
            _client.Phone = clientPhone;

            return this;
        }

        public ClientBuilder withMandatoryData()
        {
            if (string.IsNullOrEmpty(_client.Name))
                _client.Name = "Joao da Silva";
            _client.MaritalStatus = "Married";
            _client.Email = "joao@gmail.com";

            return this;
        }

        public ClientBuilder withEverythingValid()
        {
            this.withMandatoryData();
            this.withPhone();
            this.withValidAddress();
            this.withValidCPF();

            return this;
        }

        public Client build()
        {
            return _client;
        }
    }
}
