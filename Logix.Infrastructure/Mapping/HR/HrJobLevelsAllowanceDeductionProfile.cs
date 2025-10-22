using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
	public class HrJobLevelsAllowanceDeductionProfile : Profile
    {
        public HrJobLevelsAllowanceDeductionProfile()
        {
            CreateMap<HrJobLevelsAllowanceDeductionDto, HrJobLevelsAllowanceDeduction>().ReverseMap();
            CreateMap<HrJobLevelsAllowanceDeductionEditDto, HrJobLevelsAllowanceDeduction>().ReverseMap();
        }
    } 
}
