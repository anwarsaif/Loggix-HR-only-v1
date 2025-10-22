using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrDecisionProfile : Profile
    {
        public HrDecisionProfile()
        {
            CreateMap<HrDecisionDto, HrDecision>().ReverseMap();
            CreateMap<HrDecisionEditDto, HrDecision>().ReverseMap();
        }
    } 
   
}
