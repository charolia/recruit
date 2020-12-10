using Recruit.WebApi.Repository.Models;
using Recruit.WebApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recruit.WebApi.Repository
{
    public class CreditCardRepository : ICreditCardRepository
    {
        // For this assignment we are not using any backend database storage.
        // To speed up the work we will store all the input into a static variable.
        private readonly List<CreditCardEntity> creditCardStorage = new List<CreditCardEntity>();

        public CreditCardRepository()
        {
            // This is here for inserting dummy data, if you enable, tests will fail
            // InsertDummyData();
        }

        private void InsertDummyData()
        {
            creditCardStorage.AddRange(
                new List<CreditCardEntity> {
                    new CreditCardEntity()
                    {
                        CreditCardNumber = "1234-6534-5249-9876",
                        Name = "Naeem Charolia",
                        CVC = 843,
                        Expiry = DateTime.Parse("2022-02-01")
                    },
                    new CreditCardEntity()
                    {
                        CreditCardNumber = "9876-6534-1234-5249",
                        Name = "Micheal Smith",
                        CVC = 534,
                        Expiry = DateTime.Parse("2023-05-01")
                    }
                });
        }

        public Task CreateAsync(CreditCardEntity creditCard)
        {
            if (creditCard == null)
            {
                throw new ArgumentNullException(nameof(creditCard));
            }

            // Intentionally missing CC already exist check as there were no requirement

            creditCardStorage.Add(creditCard);

            return Task.CompletedTask;
        }

        public async Task<CreditCardEntity> GetAsync(string creditCardNumber)
        {
            if (string.IsNullOrWhiteSpace(creditCardNumber))
            {
                throw new ArgumentNullException(nameof(creditCardNumber));
            }

            var result = creditCardStorage
                            .SingleOrDefault(x =>
                                string.Equals(x.CreditCardNumber, creditCardNumber, StringComparison.OrdinalIgnoreCase));

            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<CreditCardEntity>> GetAllAsync()
        {
            return await Task.FromResult(creditCardStorage);
        }     
    }
}
