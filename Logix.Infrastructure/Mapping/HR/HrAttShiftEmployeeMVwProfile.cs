using AutoMapper;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrAttShiftEmployeeMVwProfile : Profile
    {
        public HrAttShiftEmployeeMVwProfile()
        {
            CreateMap<HrAttShiftEmployeeMFilterDto, HrAttShiftEmployeeMVw>().ReverseMap();
            CreateMap<HrAttShiftEmployeeMFilterDto, HrEmployeeVw>().ReverseMap();
        }
    }
}
