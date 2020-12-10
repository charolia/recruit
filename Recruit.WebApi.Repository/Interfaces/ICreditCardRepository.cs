using Recruit.WebApi.Repository.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recruit.WebApi.Services.Interfaces
{
    public interface ICreditCardRepository
    {
        Task<IEnumerable<CreditCardEntity>> GetAllAsync();
        Task<CreditCardEntity> GetAsync(string creditCardNumber);
        Task CreateAsync(CreditCardEntity creditCard);
    }
}
