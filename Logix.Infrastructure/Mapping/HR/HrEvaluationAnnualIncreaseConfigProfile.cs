using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrEvaluationAnnualIncreaseConfigProfile : Profile
    {
        public HrEvaluationAnnualIncreaseConfigProfile()
        {
            CreateMap<HrEvaluationAnnualIncreaseConfigDto, HrEvaluationAnnualIncreaseConfig>().ReverseMap();
            CreateMap<HrEvaluationAnnualIncreaseConfigEditDto, HrEvaluationAnnualIncreaseConfig>().ReverseMap();
        }
    }
}
