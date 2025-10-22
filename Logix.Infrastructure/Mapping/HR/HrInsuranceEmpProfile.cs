using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrInsuranceEmpProfile : Profile
    {
        public HrInsuranceEmpProfile()
        {
            CreateMap<HrInsuranceEmpDto, HrInsuranceEmp>().ReverseMap();
            CreateMap<HrInsuranceEmpEditDto, HrInsuranceEmp>().ReverseMap();
        }
    } 
}
