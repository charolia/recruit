using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recruit.WebApi.Repository;
using Recruit.WebApi.Repository.Models;
using Recruit.WebApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recruit.WebApi.Tests.Repositories
{
    [TestClass]
    public class CreditCardRespositoryTest
    {
        private IServiceCollection servicesCollection;
        private IServiceProvider serviceProvider;

        [TestInitialize]
        public void TestInit()
        {
            this.servicesCollection = new ServiceCollection();

            //// Here I am testing actual repository as this is already dummy
            this.servicesCollection.AddTransient<ICreditCardRepository, CreditCardRepository>();

            this.serviceProvider = this.servicesCollection.BuildServiceProvider();
        }

        [TestMethod]
        public async Task CreateAsync_Success()
        {
            var cc = new CreditCardEntity()
            {
                CreditCardNumber = "9876-6534-1234-5249",
                Name = "Micheal Smith",
                CVC = 534,
                Expiry = DateTime.Parse("2023-05-01")
            };

            // Here I am testing actual repository as this is already dummy
            var creditCardRepository = this.serviceProvider.GetService<ICreditCardRepository>();

            await creditCardRepository.CreateAsync(cc);
            var allCC = await creditCardRepository.GetAllAsync();
            Assert.IsTrue(1 == allCC.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task CreateAsync_Fail_When_Null()
        {
            CreditCardEntity cc = null;

            var creditCardRepository = this.serviceProvider.GetService<ICreditCardRepository>();

            await creditCardRepository.CreateAsync(cc);
        }

        [TestMethod]
        public async Task GetAsync_Success()
        {
            var cc = new CreditCardEntity()
            {
                CreditCardNumber = "9876-6534-1234-5249",
                Name = "Micheal Smith",
                CVC = 534,
                Expiry = DateTime.Parse("2023-05-01")
            };

            var ccNumber = "9876-6534-1234-5249";

            // Here I am testing actual repository as this is already dummy
            var creditCardRepository = this.serviceProvider.GetService<ICreditCardRepository>();

            await creditCardRepository.CreateAsync(cc);

            // act
            var result = await creditCardRepository.GetAsync(ccNumber);
            Assert.IsNotNull(result);
            Assert.AreEqual(ccNumber, result.CreditCardNumber);
        }

        [TestMethod]
        public async Task CreateAsync_Fail_Return_Null()
        {
            var ccNumber = "9876-6534-1234-5249";

            // Here I am testing actual repository as this is already dummy
            var creditCardRepository = this.serviceProvider.GetService<ICreditCardRepository>();

            // act
            var result = await creditCardRepository.GetAsync(ccNumber);
            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task CreateAsync_Fail_When_Arg_Null()
        {
            string ccNumber = null;

            var creditCardRepository = this.serviceProvider.GetService<ICreditCardRepository>();

            // act
            var result = await creditCardRepository.GetAsync(ccNumber);
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task GetAllAsync_Success()
        {
            var cc1 = new CreditCardEntity()
            {
                CreditCardNumber = "9876-6534-1234-5249",
                Name = "Micheal Smith",
                CVC = 534,
                Expiry = DateTime.Parse("2023-05-01")
            };

            var cc2 = new CreditCardEntity()
            {
                CreditCardNumber = "9876-6534-1234-1234",
                Name = "Micheal",
                CVC = 514,
                Expiry = DateTime.Parse("2021-05-01")
            };

            var cc3 = new CreditCardEntity()
            {
                CreditCardNumber = "9876-6534-1234-3214",
                Name = "Smith",
                CVC = 025,
                Expiry = DateTime.Parse("2022-05-01")
            };

            // Here I am testing actual repository as this is already dummy
            var creditCardRepository = this.serviceProvider.GetService<ICreditCardRepository>();

            await creditCardRepository.CreateAsync(cc1);
            await creditCardRepository.CreateAsync(cc2);
            await creditCardRepository.CreateAsync(cc3);

            // act
            var result = await creditCardRepository.GetAllAsync();
            Assert.IsTrue(result.Any());
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public async Task GetAllAsync_Success_When_NoData()
        {
            // Here I am testing actual repository as this is already dummy
            var creditCardRepository = this.serviceProvider.GetService<ICreditCardRepository>();

            // act
            var result = await creditCardRepository.GetAllAsync();
            Assert.IsFalse(result.Any());
            Assert.AreEqual(0, result.Count());
        }
    }
}
