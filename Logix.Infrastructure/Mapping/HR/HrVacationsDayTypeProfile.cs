using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrVacationsDayTypeProfile : Profile
    {
        public HrVacationsDayTypeProfile()
        {
            CreateMap<HrVacationsDayTypeDto, HrVacationsDayType>().ReverseMap();
            CreateMap<HrVacationsDayTypeEditDto, HrVacationsDayType>().ReverseMap();
        }
    }
}
