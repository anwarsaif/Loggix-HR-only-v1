using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrSalaryGroupProfile : Profile
    {
        public HrSalaryGroupProfile()
        {
            CreateMap<HrSalaryGroupDto, HrSalaryGroup>().ReverseMap();
            CreateMap<HrSalaryGroupEditDto, HrSalaryGroup>().ReverseMap();
        }
    }
}
