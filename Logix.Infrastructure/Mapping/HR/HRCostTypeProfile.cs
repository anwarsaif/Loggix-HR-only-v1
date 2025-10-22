using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrCostTypeProfile : Profile
    {
        public HrCostTypeProfile()
        {
            CreateMap<HrCostTypeDto, HrCostType>().ReverseMap();
            CreateMap<HrCostTypeEditDto, HrCostType>().ReverseMap();
        }
    }
}
