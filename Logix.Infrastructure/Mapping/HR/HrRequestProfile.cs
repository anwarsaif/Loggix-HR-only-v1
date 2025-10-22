using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrRequestProfile : Profile
    {
        public HrRequestProfile()
        {
            CreateMap<HrRequestDto, HrRequest>().ReverseMap();
            CreateMap<HrRequestEditDto, HrRequest>().ReverseMap();
            CreateMap<HrRequestAddDto, HrRequest>().ReverseMap();
        }
    }
}
