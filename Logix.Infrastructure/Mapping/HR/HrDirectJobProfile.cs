using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrDirectJobProfile : Profile
    {
        public HrDirectJobProfile()
        {
            CreateMap<HrDirectJobDto, HrDirectJob>().ReverseMap();
            CreateMap<HrDirectJobEditDto, HrDirectJob>().ReverseMap();
        }
    }
}
