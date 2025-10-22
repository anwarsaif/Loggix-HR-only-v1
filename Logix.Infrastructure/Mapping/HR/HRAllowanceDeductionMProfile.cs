using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrAllowanceDeductionMProfile : Profile
    {
        public HrAllowanceDeductionMProfile()
        {

            CreateMap<HrAllowanceDeductionMDto, HrAllowanceDeductionM>().ReverseMap();
            CreateMap<HrAllowanceDeductionMEditDto, HrAllowanceDeductionM>().ReverseMap();

        }
    } 
   
}
