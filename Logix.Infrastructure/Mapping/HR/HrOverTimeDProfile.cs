using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrOverTimeDProfile : Profile
    {
        public HrOverTimeDProfile()
        {
            CreateMap<HrOverTimeDDto, HrOverTimeD>().ReverseMap();
            CreateMap<HrOverTimeDEditDto, HrOverTimeD>().ReverseMap();
        }
    }
}
