using System;

namespace Recruit.WebApi.Repository.Models
{
    public class CreditCardEntity
    {
        public string Name { get; set; }
        public string CreditCardNumber { get; set; }
        public int CVC { get; set; }
        public DateTime Expiry { get; set; }
    }
}
