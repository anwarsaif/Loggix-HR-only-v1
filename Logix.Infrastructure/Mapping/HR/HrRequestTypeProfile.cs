using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrRequestTypeProfile : Profile
    {
        public HrRequestTypeProfile()
        {
            CreateMap<HrRequestTypeDto, HrRequestType>().ReverseMap();
        }
    }
}
