using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrIncrementsAllowanceDeductionProfile : Profile
    {
        public HrIncrementsAllowanceDeductionProfile()
        {
            CreateMap<HrIncrementsAllowanceDeductionDto, HrIncrementsAllowanceDeduction>().ReverseMap();
            CreateMap<HrIncrementsAllowanceDeductionEditDto, HrIncrementsAllowanceDeduction>().ReverseMap();
        }
    }
}
