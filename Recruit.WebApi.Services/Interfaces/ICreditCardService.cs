using Recruit.WebApi.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recruit.WebApi.Services.Interfaces
{
    public interface ICreditCardService
    {
        Task<IEnumerable<CreditCardDto>> GetAllAsync();
        Task<CreditCardDto> GetAsync(string creditCardNumber);
        Task CreateAsync(CreditCardDto creditCard);
    }
}
