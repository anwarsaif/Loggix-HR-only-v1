using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrJobDescriptionProfile : Profile
    {
        public HrJobDescriptionProfile()
        {
            CreateMap<HrJobDescriptionDto, HrJobDescription>().ReverseMap();
            CreateMap<HrJobDescriptionEditDto, HrJobDescription>().ReverseMap();
        }
    }
}
