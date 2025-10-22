using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrSalaryGroupRefranceProfile : Profile
    {
        public HrSalaryGroupRefranceProfile()
        {
            CreateMap<HrSalaryGroupRefranceDto, HrSalaryGroupRefrance>().ReverseMap();
            CreateMap<HrSalaryGroupRefranceEditDto, HrSalaryGroupRefrance>().ReverseMap();
        }
    }
}
