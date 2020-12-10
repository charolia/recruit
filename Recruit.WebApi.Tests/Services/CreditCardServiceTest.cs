using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Recruit.WebApi.Repository;
using Recruit.WebApi.Repository.Models;
using Recruit.WebApi.Services;
using Recruit.WebApi.Services.Interfaces;
using Recruit.WebApi.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Recruit.WebApi.Tests.Services
{
    [TestClass]
    public class CreditCardServiceTest
    {
        private IServiceCollection servicesCollection;
        private IServiceProvider serviceProvider;
        private Mock<ICreditCardRepository> creditCardRepositoryMock;

        [TestInitialize]
        public void TestInit()
        {
            this.servicesCollection = new ServiceCollection();

            var autoMapperConfig = new AutoMapper.MapperConfiguration(cfg =>
                                        cfg.AddProfile<Recruit.WebApi.Services.AutoMapperProfiles.AutoMapperProfile>());

            autoMapperConfig.AssertConfigurationIsValid();
            this.servicesCollection.AddSingleton<AutoMapper.IMapper>(autoMapperConfig.CreateMapper());

            this.creditCardRepositoryMock = new Mock<ICreditCardRepository>();

            this.servicesCollection.AddSingleton<ICreditCardRepository>(this.creditCardRepositoryMock.Object);
            this.servicesCollection.AddSingleton<ICreditCardService, CreditCardService>();

            this.serviceProvider = this.servicesCollection.BuildServiceProvider();
        }

        [TestMethod]
        public async Task CreateAsync_Success()
        {
            var dto = new CreditCardDto()
            {
                CreditCardNumber = "9876-6534-1234-5249",
                Name = "Micheal Smith",
                CVC = "534",
                Expiry = DateTime.Parse("2023-05-01")
            };

            this.creditCardRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<CreditCardEntity>()));

            var creditCardService = this.serviceProvider.GetService<ICreditCardService>();

            await creditCardService.CreateAsync(dto);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task CreateAsync_Fail_When_Arg_Null()
        {
            CreditCardDto dto = null;

            this.creditCardRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<CreditCardEntity>()));

            var creditCardService = this.serviceProvider.GetService<ICreditCardService>();

            await creditCardService.CreateAsync(dto);
        }

        [TestMethod]
        public async Task GetAllAsync_Success()
        {
            var entities = new List<CreditCardEntity>()
            {
                new CreditCardEntity()
                {
                    CreditCardNumber = "9876-6534-1234-5249",
                    Name = "Micheal Smith",
                    CVC = 534,
                    Expiry = DateTime.Parse("2023-05-01")
                }
            };

            var mapper = this.serviceProvider.GetService<IMapper>();
            var firstDto = mapper.Map<CreditCardDto>(entities.First());

            this.creditCardRepositoryMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(entities);

            var creditCardService = this.serviceProvider.GetService<ICreditCardService>();

            await creditCardService.CreateAsync(firstDto);

            var result = await creditCardService.GetAllAsync();

            Assert.IsTrue(result.Any());
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public async Task GetAsync_Success()
        {
            var dtos = new List<CreditCardDto>()
            {
                new CreditCardDto()
                {
                    CreditCardNumber = "9876-6534-1234-5249",
                    Name = "Micheal Smith",
                    CVC = "534",
                    Expiry = DateTime.Parse("2023-05-01")
                },
                new CreditCardDto()
                {
                    CreditCardNumber = "1235-6534-1234-5249",
                    Name = "Smith",
                    CVC = "504",
                    Expiry = DateTime.Parse("2022-05-01")
                }
            };

            var ccNumber = "1235-6534-1234-5249";
            var mapper = this.serviceProvider.GetService<IMapper>();
            var resultEntity = mapper.Map<CreditCardEntity>(dtos.FirstOrDefault(e => e.CreditCardNumber.Equals(ccNumber)));
            
            this.creditCardRepositoryMock.Setup(x => x.GetAsync(ccNumber))
                .ReturnsAsync(resultEntity);

            var creditCardService = this.serviceProvider.GetService<ICreditCardService>();

            await creditCardService.CreateAsync(dtos[0]);
            await creditCardService.CreateAsync(dtos[1]);

            var result = await creditCardService.GetAsync(ccNumber);

            Assert.IsNotNull(result);
            Assert.AreEqual(ccNumber, result.CreditCardNumber);
        }
    }
}
