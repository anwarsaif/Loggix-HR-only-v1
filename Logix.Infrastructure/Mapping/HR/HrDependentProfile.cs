using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrDependentProfile : Profile
    {
        public HrDependentProfile()
        {
            CreateMap<HrDependentDto, HrDependent>().ReverseMap();
            CreateMap<HrDependentEditDto, HrDependent>().ReverseMap();
        }
    }
}
