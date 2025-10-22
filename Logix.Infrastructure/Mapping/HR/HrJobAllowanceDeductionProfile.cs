using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
	public class HrJobAllowanceDeductionProfile : Profile
    {
        public HrJobAllowanceDeductionProfile()
        {
            CreateMap<HrJobAllowanceDeductionDto, HrJobAllowanceDeduction>().ReverseMap();
        }
    }
}
