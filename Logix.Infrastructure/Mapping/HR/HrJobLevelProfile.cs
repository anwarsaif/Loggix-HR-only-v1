using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrJobLevelProfile : Profile
    {
        public HrJobLevelProfile()
        {
            CreateMap<HrJobLevelDto, HrJobLevel>().ReverseMap();
            CreateMap<HrJobLevelEditDto, HrJobLevel>().ReverseMap();
        }
    }
}
