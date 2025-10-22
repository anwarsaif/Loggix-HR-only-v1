using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
	public class HrJobCategoryProfile : Profile
    {
		public HrJobCategoryProfile()
        {
            CreateMap<HrJobCategoryDto, HrJobCategory>().ReverseMap();
            CreateMap<HrJobCategoryEditDto, HrJobCategory>().ReverseMap();
        }
    }
}
