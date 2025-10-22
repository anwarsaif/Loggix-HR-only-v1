using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrRequestDetaileProfile : Profile
    {
        public HrRequestDetaileProfile()
        {
            CreateMap<HrRequestDetailsDto, HrRequestDetaile>().ReverseMap();
            CreateMap<HrRequestDetailsEditDto, HrRequestDetaile>().ReverseMap();
        }
    }
}
