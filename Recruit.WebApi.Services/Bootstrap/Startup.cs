using Microsoft.Extensions.DependencyInjection;
using Recruit.WebApi.Repository;
using Recruit.WebApi.Services.Interfaces;

namespace Recruit.WebApi.Services.Bootstrap
{
    public class Startup
    {
        public static void Load(IServiceCollection services)
        {
            Recruit.WebApi.Repository.Bootstrap.Startup.Load(services);
            services.AddTransient<ICreditCardService, CreditCardService>();
        }
    }
}
