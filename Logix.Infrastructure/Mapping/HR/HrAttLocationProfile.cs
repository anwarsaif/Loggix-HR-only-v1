using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrAttLocationProfile : Profile
    {
        public HrAttLocationProfile()
        {
            CreateMap<HrAttLocationDto, HrAttLocation>().ReverseMap();
            CreateMap<HrAttLocationEditDto, HrAttLocation>().ReverseMap();
        }
    }
}
