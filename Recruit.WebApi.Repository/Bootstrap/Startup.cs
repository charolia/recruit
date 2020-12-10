using Microsoft.Extensions.DependencyInjection;
using Recruit.WebApi.Services.Interfaces;

namespace Recruit.WebApi.Repository.Bootstrap
{
    public class Startup
    {
        public static void Load(IServiceCollection services)
        {
            services.AddTransient<ICreditCardRepository, CreditCardRepository>();
        }
    }
}
