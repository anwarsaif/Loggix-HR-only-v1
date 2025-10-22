using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
	public class HrAllowanceDeductionTempOrFixProfile : Profile
    {
        public HrAllowanceDeductionTempOrFixProfile()
        {
            CreateMap<HrAllowanceDeductionTempOrFixDto, HrAllowanceDeductionTempOrFix>().ReverseMap();
        }
    } 
}
