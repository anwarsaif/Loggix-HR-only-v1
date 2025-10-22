using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrEmployeeCostProfile : Profile
    {
        public HrEmployeeCostProfile()
        {
            CreateMap<HrEmployeeCostDto, HrEmployeeCost>().ReverseMap();
            CreateMap<HrEmployeeCostEditDto, HrEmployeeCost>().ReverseMap();
        }
    }
}
