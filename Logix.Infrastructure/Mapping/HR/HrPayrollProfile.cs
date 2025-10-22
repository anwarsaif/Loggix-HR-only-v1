using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrPayrollProfile : Profile
    {
        public HrPayrollProfile()
        {
            CreateMap<HrPayrollDto, HrPayroll>().ReverseMap();
            CreateMap<HrPayrollEditDto, HrPayroll>().ReverseMap();
        }
    }
}
