using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrVacationsProfile : Profile
    {
        public HrVacationsProfile()
        {
            CreateMap<HrVacationsDto, HrVacation>().ReverseMap();
            CreateMap<HrVacationsEditDto, HrVacation>().ReverseMap();
        }
    }
}
