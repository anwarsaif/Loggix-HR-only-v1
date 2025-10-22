using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrIncrementProfile : Profile
    {
        public HrIncrementProfile()
        {
            CreateMap<HrIncrementDto, HrIncrement>().ReverseMap();
            CreateMap<HrIncrementEditDto, HrIncrement>().ReverseMap();
        }
    }
}
