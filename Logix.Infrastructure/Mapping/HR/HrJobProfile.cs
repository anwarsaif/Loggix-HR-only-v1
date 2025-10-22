using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrJobProfile : Profile
    {
        public HrJobProfile()
        {
            CreateMap<HrJobDto, HrJob>().ReverseMap();
            CreateMap<HrJobEditDto, HrJob>().ReverseMap();
        }
    }
}
