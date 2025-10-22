using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrKpiDetaileProfile : Profile
    {
        public HrKpiDetaileProfile()
        {
            CreateMap<HrKpiDetaileDto, HrKpiDetaile>().ReverseMap();
            CreateMap<HrKpiDetaileEditDto, HrKpiDetaile>().ReverseMap();
        }
    }
}
