using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrPayrollDProfile : Profile
    {
        public HrPayrollDProfile()
        {
            CreateMap<HrPayrollDDto, HrPayrollD>().ReverseMap();
            CreateMap<HrPayrollDEditDto, HrPayrollD>().ReverseMap();
        }
    }
}
