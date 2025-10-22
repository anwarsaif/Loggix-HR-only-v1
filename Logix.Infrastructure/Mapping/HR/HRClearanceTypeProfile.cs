using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrClearanceTypeProfile : Profile
    {
        public HrClearanceTypeProfile()
        {
            CreateMap<HrClearanceTypeDto, HrClearanceType>().ReverseMap();
        }
    }
}
