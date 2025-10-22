using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrIncrementTypeProfile : Profile
    {
        public HrIncrementTypeProfile()
        {
            CreateMap<HrIncrementTypeDto, HrIncrementType>().ReverseMap();
        }
    }
}
