using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
	public class HrAttLocationEmployeeProfile : Profile
    {
        public HrAttLocationEmployeeProfile()
        {
            CreateMap<HrAttLocationEmployeeDto, HrAttLocationEmployee>().ReverseMap();
            CreateMap<HrAttLocationEmployeeEditeDto, HrAttLocationEmployee>().ReverseMap();
        }
    }
}
