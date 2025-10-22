using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrMandateRequestsDetaileProfile : Profile
    {
        public HrMandateRequestsDetaileProfile()
        {
            CreateMap<HrMandateRequestsDetaileDto, HrMandateRequestsDetaile>().ReverseMap();
            CreateMap<HrMandateRequestsDetaileEditDto, HrMandateRequestsDetaile>().ReverseMap();
        }
    }
}
