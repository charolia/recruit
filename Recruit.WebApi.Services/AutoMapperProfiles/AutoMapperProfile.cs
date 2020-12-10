using AutoMapper;
using Recruit.WebApi.Repository.Models;
using Recruit.WebApi.Services.Models;

namespace Recruit.WebApi.Services.AutoMapperProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            this.CreateMap<CreditCardDto, CreditCardEntity>().ReverseMap();
        }
    }
}
