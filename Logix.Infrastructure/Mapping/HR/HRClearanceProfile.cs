using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrClearanceProfile : Profile
    {
        public HrClearanceProfile()
        {
            CreateMap<HrClearanceDto, HrClearance>().ReverseMap();
            CreateMap<HrClearanceAddDto, HrClearance>().ReverseMap();
            CreateMap<HrClearanceEditDto, HrClearance>().ReverseMap();
        }
    }

}
