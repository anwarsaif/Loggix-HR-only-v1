using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrOpeningBalanceProfile : Profile
    {
        public HrOpeningBalanceProfile()
        {
            CreateMap<HrOpeningBalanceDto, HrOpeningBalance>().ReverseMap();
            CreateMap<HrOpeningBalanceEditDto, HrOpeningBalance>().ReverseMap();
        }
    } 
}
