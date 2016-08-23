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
    public class ClientBusinessTest
    {
        private Mock<IClientRepository> ArrangeMockedRepo()
        {
            return new Mock<IClientRepository>();
        }

        [TestMethod]
        public void Client_With_Valid_CPF_Is_Inserted()
        {
            //Arrange
            var repo = this.ArrangeMockedRepo();
            var clientServices = new ClientServices(repo.Object);

            //Act
            var validClient1 = new ClientBuilder("673.285.351-13")
                                        .withValidAddress()
                                        .withPhone()
                                        .withMandatoryData()
                                        .build();

            var validClient2 = new ClientBuilder("99658238122")
                                        .withValidAddress()
                                        .withPhone()
                                        .withMandatoryData()
                                        .build();

            clientServices.Insert(validClient1);
            clientServices.Insert(validClient2);

            //Assert
            repo.Verify(c => c.Insert(validClient1), Times.Once());
            repo.Verify(c => c.Insert(validClient2), Times.Once());
        }

        [TestMethod]
        public void Client_Without_Valid_CPF_Is_Not_Inserted()
        {
            //Arrange
            var repo = this.ArrangeMockedRepo();
            var clientServices = new ClientServices(repo.Object);

            //Act
            var invalidClient1 = new ClientBuilder("123.456.789-00")
                                        .withValidAddress()
                                        .withPhone()
                                        .withMandatoryData()
                                        .build();

            var invalidClient2 = new ClientBuilder("123.456.789")
                                        .withValidAddress()
                                        .withPhone()
                                        .withMandatoryData()
                                        .build();

            var invalidClient3 = new ClientBuilder("FFF.FFF.FFF-FF")
                                        .withValidAddress()
                                        .withPhone()
                                        .withMandatoryData()
                                        .build();

            clientServices.Insert(invalidClient1);
            clientServices.Insert(invalidClient2);
            clientServices.Insert(invalidClient3);

            //Assert
            repo.Verify(c => c.Insert(invalidClient1), Times.Never());
            repo.Verify(c => c.Insert(invalidClient2), Times.Never());
            repo.Verify(c => c.Insert(invalidClient3), Times.Never());
        }

        [TestMethod]
        public void Client_With_More_Than_One_Adress_Is_Not_Inserted()
        {
            //Arrange
            var repo = this.ArrangeMockedRepo();
            var clientServices = new ClientServices(repo.Object);

            //Act
            var clientAddress = new List<Address>();
            clientAddress.Add(new Address { Street = "Gold street, 1500", City = "Belo Horizonte", State = "MG", PostalCode = "30300-300" });
            clientAddress.Add(new Address { Street = "Silver street, 1500", City = "Belo Horizonte", State = "MG", PostalCode = "30300-200" });
            var client = new ClientBuilder()
                                .withValidCPF()
                                .withPhone()
                                .withMandatoryData()
                                .build();
            client.Address = clientAddress;

            clientServices.Insert(client);

            //Assert
            repo.Verify(c => c.Insert(client), Times.Never());
        }

        [TestMethod]
        public void Client_Without_Adress_Is_Not_Inserted()
        {
            //Arrange
            var repo = this.ArrangeMockedRepo();
            var clientServices = new ClientServices(repo.Object);

            //Act
            var client = new ClientBuilder()
                                .withValidCPF()
                                .withPhone()
                                .withMandatoryData()
                                .build();

            clientServices.Insert(client);

            //Assert
            repo.Verify(c => c.Insert(client), Times.Never());
        }

        [TestMethod]
        public void Duplicated_Client_Is_Not_Inserted()
        {
            //Arrange
            var repo = this.ArrangeMockedRepo();
            var clientServices = new ClientServices(repo.Object);

            //Act
            var client = new ClientBuilder()
                                .withValidCPF()
                                .withPhone()
                                .withMandatoryData()
                                .withValidAddress()
                                .build();

            repo.Setup(c => c.FindById(client.Cpf)).Returns(client);

            clientServices.Insert(client);

            //Assert
            repo.Verify(c => c.Insert(client), Times.Never());
        }

        [TestMethod]
        public void Client_With_Correct_Data_Is_Inserted()
        {
            //Arrange
            var repo = this.ArrangeMockedRepo();
            var clientServices = new ClientServices(repo.Object);

            //Act
            var client = new ClientBuilder()
                                .withValidCPF()
                                .withPhone()
                                .withMandatoryData()
                                .withValidAddress()
                                .build();

            clientServices.Insert(client);

            //Assert
            repo.Verify(c => c.Insert(client), Times.Once());
        }

        [TestMethod]
        public void Client_Without_Mandatory_Data_Is_Not_Inserted()
        {
            //Arrange
            var repo = this.ArrangeMockedRepo();
            var clientServices = new ClientServices(repo.Object);

            //Act
            var client = new ClientBuilder()
                                .withValidCPF()
                                .withPhone()
                                .withValidAddress()
                                .build();

            clientServices.Insert(client);

            //Assert
            repo.Verify(c => c.Insert(client), Times.Never());
        }
    }
}




// Pensar se vamos ou não usar DB de vdd
// Pensar se cpf services vale uma interface
// Comentar método público


//******Feito
// Será necessário completar os testes
// Será necessário validar se devo montar uma classe de retorno
// Será necessário revisar os testes
// Colocar mensagens em resourse
// Parametrizar o caminho fisico que gravará o arquivo JSON
// Formatar um padrão para o cpf (com ou sem pontuação)
// Fazer interface dde Repo genérica usando T


//******Não foi feito
// Tem q ter uma rest para testar


