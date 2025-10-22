using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrGosiProfile : Profile
    {
        public HrGosiProfile()
        {
            CreateMap<HrGosiDto, HrGosi>().ReverseMap();
            CreateMap<HrGosiEditDto, HrGosi>().ReverseMap();
        }
    }
}
