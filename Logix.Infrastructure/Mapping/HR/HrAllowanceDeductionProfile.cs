using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrAllowanceDeductionProfile : Profile
    {
        public HrAllowanceDeductionProfile()
        {
            CreateMap<HrAllowanceDeductionDto, HrAllowanceDeduction>().ReverseMap();
            CreateMap<HrAllowanceDeductionEditDto, HrAllowanceDeduction>().ReverseMap();
            CreateMap<HrOtherAllowanceDeductionAddDto, HrAllowanceDeduction>().ReverseMap();
            CreateMap<HrOtherAllowanceDeductionEditDto, HrAllowanceDeduction>().ReverseMap();
        }
    }
}
