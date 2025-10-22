using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
	public class HrJobGroupsProfile : Profile
    {
        public HrJobGroupsProfile()
        {
            CreateMap<HrJobGroupsDto, HrJobGroups>().ReverseMap();
            CreateMap<HrJobGroupsEditDto, HrJobGroups>().ReverseMap();
        }
    }
}
