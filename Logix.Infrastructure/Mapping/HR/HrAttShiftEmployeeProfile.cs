using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrAttShiftEmployeeProfile : Profile
    {
        public HrAttShiftEmployeeProfile()
        {
            CreateMap<HrAttShiftEmployeeDto, HrAttShiftEmployee>().ReverseMap();
            CreateMap<HrAttShiftEmployeeEditDto, HrAttShiftEmployee>().ReverseMap();
        }
    }
}
