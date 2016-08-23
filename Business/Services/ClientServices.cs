using ClientsAPI.Data.Repositories;
using ClientsAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace ClientsAPI.Business.Services
{
    public class ClientServices : IClientServices
    {
        private IClientRepository _repo;
        private CpfServices _cpfServices = new CpfServices();

        public ClientServices(IClientRepository repo)
        {
            _repo = repo;
        }

        public ClientServices()
        {
            _repo = new ClientFileRepository();
        }

        private bool IsValid(Client client, ClientResult result)
        {
            var validationContext = new ValidationContext(client);

            // Validate considering the annotations from the model
            Validator.TryValidateObject(client, validationContext, result.ValidationResults, true);

            if (!_cpfServices.ValidateCpf(client.Cpf))
                result.ValidationResults.Add(new ValidationResult(Messages.ErrMsgInvalidCPF));

            if (client.Address != null && client.Address.Count() > 1)
                result.ValidationResults.Add(new ValidationResult(Messages.ErrMsgMultipleAddress));

            result.Success = result.ValidationResults.Count() == 0;
            return (result.Success);
        }

        public ClientResult Insert(Client client)
        {
            var result = new ClientResult();
            try
            {
                if (this.IsValid(client, result) && _repo.FindById(client.Cpf) == null)
                {
                    client.Cpf = _cpfServices.FormatCpf(client.Cpf);
                    _repo.Insert(client);
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ValidationResults.Add(new ValidationResult(ex.Message));
            }
            
            return result;
        }

        public ClientResult Update(Client client)
        {
            var result = new ClientResult();
            try
            {
                if (this.IsValid(client, result))
                {
                    client.Cpf = _cpfServices.FormatCpf(client.Cpf);
                    _repo.Update(client);
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ValidationResults.Add(new ValidationResult(ex.Message));
            }

            return result;
        }

        public Client GetClientByCpf(string Cpf)
        {
            return _repo.FindById(_cpfServices.FormatCpf(Cpf));
        }

        public IEnumerable<Client> GetAllClients()
        {
            return _repo.GetAll();
        }

        public IEnumerable<Client> GetClientsByName(string name)
        {
            return _repo.GetClientsByName(name);
        }
    }
}
