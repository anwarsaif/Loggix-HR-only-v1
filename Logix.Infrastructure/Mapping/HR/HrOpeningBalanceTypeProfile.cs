using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrOpeningBalanceTypeProfile : Profile
    {
        public HrOpeningBalanceTypeProfile()
        {
            CreateMap<HrOpeningBalanceTypeDto, HrOpeningBalanceType>().ReverseMap();
        }
    }   
}
