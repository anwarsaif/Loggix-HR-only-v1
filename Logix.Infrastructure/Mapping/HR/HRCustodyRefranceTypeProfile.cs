using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrCustodyRefranceTypeProfile : Profile
    {
        public HrCustodyRefranceTypeProfile()
        {
            CreateMap<HrCustodyRefranceTypeDto, HrCustodyRefranceType>().ReverseMap();
        }
    }
}
