using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrCustodyItemProfile : Profile
    {
        public HrCustodyItemProfile()
        {
            CreateMap<HrCustodyItemDto, HrCustodyItem>().ReverseMap();
            CreateMap<HrCustodyItemEditDto, HrCustodyItem>().ReverseMap();
        }
    } 
   
}
