using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrRequestGoalsEmployeeDetailProfile : Profile
    {
        public HrRequestGoalsEmployeeDetailProfile()
        {
            CreateMap<HrRequestGoalsEmployeeDetailDto, HrRequestGoalsEmployeeDetail>().ReverseMap();
        }
    } 
}
