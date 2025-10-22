using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrPayrollAllowanceDeductionProfile : Profile
    {
        public HrPayrollAllowanceDeductionProfile()
        {
            CreateMap<HrPayrollAllowanceDeductionDto, HrPayrollAllowanceDeduction>().ReverseMap();
            CreateMap<HrPayrollAllowanceDeductionEditDto, HrPayrollAllowanceDeduction>().ReverseMap();
        }
    }
}
