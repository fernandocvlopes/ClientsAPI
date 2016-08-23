using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ClientsAPI.Domain.Models;
using ClientsAPI.Data.Repositories;
using ClientsAPI.Business.Services;
using System.Collections.Generic;
using System.Linq;

namespace ClientsAPI.Test
{
    [TestClass]
    public class ClientDataTest
    {
        private void PrepareDateToTest(ClientRepository repo, ClientServices clientServices)
        {
            repo.ClearData();

            var Client1 = new ClientBuilder("Client Test 1", "391.004.767-06")
                            .withEverythingValid()
                            .build();

            var Client2 = new ClientBuilder("Client Test 2", "347.165.654-59")
                            .withEverythingValid()
                            .build();

            var Client3 = new ClientBuilder("Client Test 3", "076.226.262-10")
                .withEverythingValid()
                .build();

            clientServices.Insert(Client1);
            clientServices.Insert(Client2);
            clientServices.Insert(Client3);
        }

        [TestMethod]
        public void Get_Client_By_CPF_Is_Working()
        {
            //Arrange
            var repo = new ClientRepository();
            var clientServices = new ClientServices(repo);

            //Act
            this.PrepareDateToTest(repo, clientServices);

            // using formatted CPF
            var client1 = clientServices.GetClientByCpf("391.004.767-06");
            // using unformatted CPF
            var client2 = clientServices.GetClientByCpf("07622626210");

            //Assert
            Assert.IsNotNull(client1);
            Assert.IsNotNull(client2);
        }

        [TestMethod]
        public void Get_All_Clients_Is_Working()
        {
            //Arrange
            var repo = new ClientRepository();
            var clientServices = new ClientServices(repo);

            //Act
            this.PrepareDateToTest(repo, clientServices);
            var clients = clientServices.GetAllClients();

            //Assert
            Assert.AreEqual(clients.Count(), 3);
        }

        [TestMethod]
        public void Get_Clients_By_Name_Is_Working()
        {
            //Arrange
            var repo = new ClientRepository();
            var clientServices = new ClientServices(repo);

            //Act
            this.PrepareDateToTest(repo, clientServices);
            var clients = clientServices.GetClientsByName("Client Test 2");

            //Assert
            Assert.AreEqual(clients.Count(), 1);
        }

        [TestMethod]
        public void Update_Client_Is_Working()
        {
            //Arrange
            var repo = new ClientRepository();
            var clientServices = new ClientServices(repo);

            //Act
            this.PrepareDateToTest(repo, clientServices);
            var client = new ClientBuilder("Updated Client Test 1", "391.004.767-06")
                            .withEverythingValid()
                            .build();
            clientServices.Update(client);
            var updatedClient = clientServices.GetClientByCpf("391.004.767-06");

            //Assert            
            Assert.AreEqual(updatedClient, client);
        }
    }
}
