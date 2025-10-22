using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrAttActionProfile : Profile
    {
        public HrAttActionProfile()
        {
            CreateMap<HrAttActionDto, HrAttAction>().ReverseMap();
        }
    }
}
