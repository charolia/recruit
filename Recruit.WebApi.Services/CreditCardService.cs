using AutoMapper;
using Recruit.WebApi.Repository.Models;
using Recruit.WebApi.Services.Interfaces;
using Recruit.WebApi.Services.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recruit.WebApi.Services
{
    public class CreditCardService : ICreditCardService
    {
        private readonly ICreditCardRepository creditCardRepository;
        private readonly IMapper mapper;

        public CreditCardService(ICreditCardRepository creditCardRepository,
            IMapper mapper)
        {
            this.creditCardRepository = creditCardRepository ?? throw new ArgumentNullException(nameof(creditCardRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task CreateAsync(CreditCardDto creditCard)
        {
            if (creditCard == null)
            {
                throw new ArgumentNullException(nameof(creditCard));
            }

            await this.creditCardRepository
                .CreateAsync(this.mapper.Map<CreditCardEntity>(creditCard))
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<CreditCardDto>> GetAllAsync()
        {
            var result = await this.creditCardRepository
                        .GetAllAsync()
                        .ConfigureAwait(false);

            return this.mapper.Map<IEnumerable<CreditCardDto>>(result);
        }

        public async Task<CreditCardDto> GetAsync(string creditCardNumber)
        {
            var result = await this.creditCardRepository
                                   .GetAsync(creditCardNumber)
                                   .ConfigureAwait(false);

            return this.mapper.Map<CreditCardDto>(result);
        }
    }
}
