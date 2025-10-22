using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrPayrollCostcenterProfile : Profile
    {
        public HrPayrollCostcenterProfile()
        {
            CreateMap<HrPayrollCostcenterEditDto, HrPayrollCostcenter>().ReverseMap();
        }
    }
}
