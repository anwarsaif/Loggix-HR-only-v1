using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrSalaryGroupAccountProfile : Profile
    {
        public HrSalaryGroupAccountProfile()
        {
            CreateMap<HrSalaryGroupAccountDto, HrSalaryGroupAccount>().ReverseMap();
            CreateMap<HrSalaryGroupAccountEditDto, HrSalaryGroupAccount>().ReverseMap();
        }
    }
}
