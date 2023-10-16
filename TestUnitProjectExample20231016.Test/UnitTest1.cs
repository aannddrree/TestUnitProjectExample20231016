using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TestUnitProjectExample20231016.Controllers;
using TestUnitProjectExample20231016.Data;
using TestUnitProjectExample20231016.Models;
using Xunit;

namespace TestUnitProjectExample20231016.Test
{
    public class UnitTest1
    {

        private DbContextOptions<TestUnitProjectExample20231016Context> options;

        private void InitializeDataBase()
        {
            // Create a Temporary Database
            //Precisa instalar a dependencia pelo nuget: Microsoft.EntityFrameworkCore.InMemory
            options = new DbContextOptionsBuilder<TestUnitProjectExample20231016Context>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            // Insert data into the database using one instance of the context
            using (var context = new TestUnitProjectExample20231016Context(options))
            {
                context.Client.Add(new Client { Id = 1, Name = "André", Telephone = "12345678"});
                context.Client.Add(new Client { Id = 2, Name = "Luiz", Telephone = "12345678" });
                context.Client.Add(new Client { Id = 3, Name = "Silva", Telephone = "12345678" });
                context.SaveChanges();
            }
        }


        [Fact]
        public void GetAll()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new TestUnitProjectExample20231016Context(options))
            {
                ClientsController clientController = new ClientsController(context);
                IEnumerable<Client> clients = clientController.GetClient().Result.Value;
                Assert.Equal(3, clients.Count());
            }
        }

        [Fact]
        public void GetbyId()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new TestUnitProjectExample20231016Context(options))
            {
                int clientId = 2;
                ClientsController clientController = new ClientsController(context);
                Client client = clientController.GetClient(clientId).Result.Value;
                Assert.Equal(2, client.Id);
            }
        }

        [Fact]
        public async void Create()
        {
            InitializeDataBase();

            Client client = new Client()
            {
                Id = 4,
                Name = "Jose",
                Telephone = "1234566"
            };

            // Use a clean instance of the context to run the test
            using (
                var context = new TestUnitProjectExample20231016Context(options))
            {
                ClientsController clientController = new ClientsController(context);
                await clientController.PostClient(client);
                Client clientReturn = clientController.GetClient(4).Result.Value;
                Assert.Equal("Jose", clientReturn.Name);
            }
        }

        [Fact]
        public async void Update()
        {
            InitializeDataBase();

            Client client = new Client()
            {
                Id = 2,
                Name = "Jose",
                Telephone = "1234566"
            };

            // Use a clean instance of the context to run the test
            using (var context = new TestUnitProjectExample20231016Context(options))
            {
                ClientsController clientController = new ClientsController(context);
                await clientController.PutClient(2, client);
                Client clientReturn = clientController.GetClient(2).Result.Value;
                Assert.Equal("Jose", clientReturn.Name);
            }
        }

        [Fact]
        public void Delete()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new TestUnitProjectExample20231016Context(options))
            {
                ClientsController clientsController = new ClientsController(context);
                Client client = clientsController.DeleteClient(2).Result.Value;
                Assert.Null(client);
            }
        }
    }
}