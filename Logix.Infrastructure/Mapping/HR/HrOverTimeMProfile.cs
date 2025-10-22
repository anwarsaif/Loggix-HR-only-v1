using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrOverTimeMProfile : Profile
    {
        public HrOverTimeMProfile()
        {
            CreateMap<HrOverTimeMDto, HrOverTimeM>().ReverseMap();
            CreateMap<HrOverTimeMEditDto, HrOverTimeM>().ReverseMap();
            CreateMap<HrOverTimeMGetByIdDto, HrOverTimeMVw>().ReverseMap();
        }
    }
}
