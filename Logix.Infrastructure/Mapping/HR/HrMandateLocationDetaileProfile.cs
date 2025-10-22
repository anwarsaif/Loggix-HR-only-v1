using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrMandateLocationDetaileProfile : Profile
    {
        public HrMandateLocationDetaileProfile()
        {
            CreateMap<HrMandateLocationDetaileDto, HrMandateLocationDetaile>().ReverseMap();
            CreateMap<HrMandateLocationDetaileEditDto, HrMandateLocationDetaile>().ReverseMap();
        }
    }
}
