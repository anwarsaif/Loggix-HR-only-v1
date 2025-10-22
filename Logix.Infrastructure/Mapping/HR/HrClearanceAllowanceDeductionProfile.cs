using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrClearanceAllowanceDeductionProfile : Profile
    {
        public HrClearanceAllowanceDeductionProfile()
        {
            CreateMap<HrClearanceAllowanceDeductionDto, HrClearanceAllowanceDeduction>().ReverseMap();
        }
    }
}
