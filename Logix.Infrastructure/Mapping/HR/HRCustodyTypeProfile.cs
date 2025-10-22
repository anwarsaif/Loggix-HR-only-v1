using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrCustodyTypeProfile : Profile
    {
        public HrCustodyTypeProfile()
        {
            CreateMap<HrCustodyTypeDto, HrCustodyType>().ReverseMap();
        }
    }    
}
