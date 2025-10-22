using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrVacationsTypeProfile : Profile
    {
        public HrVacationsTypeProfile()
        {
            CreateMap<HrVacationsTypeDto, HrVacationsType>().ReverseMap();
            CreateMap<HrVacationsTypeEditDto, HrVacationsType>().ReverseMap();
        }
    }
}
