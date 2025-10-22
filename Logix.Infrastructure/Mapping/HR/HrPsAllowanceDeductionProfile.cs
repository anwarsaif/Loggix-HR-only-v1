using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrPsAllowanceDeductionProfile : Profile
    {
        public HrPsAllowanceDeductionProfile()
        {
            CreateMap<HrPsAllowanceDeductionDto, HrPsAllowanceDeduction>().ReverseMap();
            CreateMap<HrPsAllowanceDeductionEditDto, HrPsAllowanceDeduction>().ReverseMap();
        }
    }
}
