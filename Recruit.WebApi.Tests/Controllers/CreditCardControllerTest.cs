using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Recruit.WebApi.Controllers;
using Recruit.WebApi.Services.Interfaces;
using Recruit.WebApi.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recruit.WebApi.Tests.Controllers
{
    [TestClass]
    public class CreditCardControllerTest
    {
        private IServiceCollection servicesCollection;
        private IServiceProvider serviceProvider;
        private Mock<ICreditCardService> creditCardServiceMock;

        [TestInitialize]
        public void TestInit()
        {
            this.servicesCollection = new ServiceCollection();

            var autoMapperConfig = new AutoMapper.MapperConfiguration(cfg =>
                                        cfg.AddProfile<Recruit.WebApi.Services.AutoMapperProfiles.AutoMapperProfile>());

            autoMapperConfig.AssertConfigurationIsValid();
            this.servicesCollection.AddSingleton<AutoMapper.IMapper>(autoMapperConfig.CreateMapper());

            this.creditCardServiceMock = new Mock<ICreditCardService>();
            this.servicesCollection.AddSingleton<ICreditCardService>(this.creditCardServiceMock.Object);

            this.servicesCollection.AddSingleton<CreditCardController>();

            
            this.serviceProvider = this.servicesCollection.BuildServiceProvider();
        }

        [TestMethod]
        public async Task GetAllCreditCards_Success()
        {
            var creditCards = new List<CreditCardDto> {
                    new CreditCardDto()
                    {
                        CreditCardNumber = "1234-6534-5249-9876",
                        Name = "Naeem Charolia",
                        CVC = "843",
                        Expiry = DateTime.Parse("2022-02-01")
                    },
                    new CreditCardDto()
                    {
                        CreditCardNumber = "9876-6534-1234-5249",
                        Name = "Micheal Smith",
                        CVC = "534",
                        Expiry = DateTime.Parse("2023-05-01")
                    }
                };

            
            this.creditCardServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(await Task.FromResult(creditCards));

            var creditCardController = this.serviceProvider.GetService<CreditCardController>();

            var okResult = (OkObjectResult) await creditCardController.GetAllAsync() ;

            var cc = okResult.Value as List<CreditCardDto>;

            Assert.IsTrue(cc.Count == creditCards.Count);
        }

        [TestMethod]
        public async Task GetAllCreditCards_Success_When_No_Data()
        {
            var creditCards = new List<CreditCardDto> {
                    //new CreditCardDto()
                    //{
                    //    CreditCardNumber = "1234-6534-5249-9876",
                    //    Name = "Naeem Charolia",
                    //    CVC = "843",
                    //    Expiry = DateTime.Parse("2022-02-01")
                    //},
                    //new CreditCardDto()
                    //{
                    //    CreditCardNumber = "9876-6534-1234-5249",
                    //    Name = "Micheal Smith",
                    //    CVC = "534",
                    //    Expiry = DateTime.Parse("2023-05-01")
                    //}
                };


            this.creditCardServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(await Task.FromResult(creditCards));

            var creditCardController = this.serviceProvider.GetService<CreditCardController>();

            var okResult = (OkObjectResult)await creditCardController.GetAllAsync();

            var cc = okResult.Value as List<CreditCardDto>;

            Assert.IsTrue(cc.Count == creditCards.Count);
        }

        [TestMethod]
        public async Task GetCreditCard_Success()
        {
            var creditCards = new List<CreditCardDto> {
                    new CreditCardDto()
                    {
                        CreditCardNumber = "1234-6534-5249-9876",
                        Name = "Naeem Charolia",
                        CVC = "843",
                        Expiry = DateTime.Parse("2022-02-01")
                    },
                    new CreditCardDto()
                    {
                        CreditCardNumber = "9876-6534-1234-5249",
                        Name = "Micheal Smith",
                        CVC = "534",
                        Expiry = DateTime.Parse("2023-05-01")
                    }
                };

            var ccNumber = "9876-6534-1234-5249";


            this.creditCardServiceMock.Setup(x => x.GetAsync(ccNumber))
                .ReturnsAsync(await Task.FromResult(creditCards.FirstOrDefault(c => c.CreditCardNumber.Equals(ccNumber))));

            var creditCardController = this.serviceProvider.GetService<CreditCardController>();

            var okResult = (OkObjectResult)await creditCardController.GetByCCNumberAsync(ccNumber);

            var cc = okResult.Value as CreditCardDto;

            Assert.IsNotNull(cc);
            Assert.AreEqual(ccNumber, cc.CreditCardNumber);
        }

        [TestMethod]
        public async Task GetCreditCard_Success_When_Return_Null()
        {
            var creditCards = new List<CreditCardDto> {
                    new CreditCardDto()
                    {
                        CreditCardNumber = "1234-6534-5249-9876",
                        Name = "Naeem Charolia",
                        CVC = "843",
                        Expiry = DateTime.Parse("2022-02-01")
                    },
                    new CreditCardDto()
                    {
                        CreditCardNumber = "9876-6534-1234-5249",
                        Name = "Micheal Smith",
                        CVC = "534",
                        Expiry = DateTime.Parse("2023-05-01")
                    }
                };

            var ccNumber = "9876-6534-1234-1234";


            this.creditCardServiceMock.Setup(x => x.GetAsync(ccNumber))
                .ReturnsAsync(await Task.FromResult(creditCards.FirstOrDefault(c => c.CreditCardNumber.Equals(ccNumber))));

            var creditCardController = this.serviceProvider.GetService<CreditCardController>();

            var okResult = (OkObjectResult)await creditCardController.GetByCCNumberAsync(ccNumber);

            var cc = okResult.Value as CreditCardDto;

            Assert.IsNull(cc);
        }

        [TestMethod]
        public async Task SaveAsync_Success()
        {
            var cc = new CreditCardDto()
            {
                CreditCardNumber = "9876-6534-1234-5249",
                Name = "Micheal Smith",
                CVC = "534",
                Expiry = DateTime.Parse("2023-05-01")
            };

            this.creditCardServiceMock.Setup(x => x.CreateAsync(cc));

            var creditCardController = this.serviceProvider.GetService<CreditCardController>();

            var okResult = (OkResult)await creditCardController.SaveAsync(cc);

            Assert.IsInstanceOfType(okResult, typeof(OkResult));
        }
    }
}
