using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrVisaProfile : Profile
    {
        public HrVisaProfile()
        {
            CreateMap<HrVisaDto, HrVisa>().ReverseMap();
            CreateMap<HrVisaEditDto, HrVisa>().ReverseMap();
        }
    }
}
