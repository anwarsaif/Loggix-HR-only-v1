using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
	public class HrJobGradeProfile : Profile
    {
        public HrJobGradeProfile()
        {
            CreateMap<HrJobGradeDto, HrJobGrade>().ReverseMap();
            CreateMap<HrJobGradeEditDto, HrJobGrade>().ReverseMap();
        }
    }
}
