using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrCustodyProfile : Profile
    {
        public HrCustodyProfile()
        {
            CreateMap<HrCustodyDto, HrCustody>().ReverseMap();
            CreateMap<HrCustodyEditDto, HrCustody>().ReverseMap();
        }
    } 
   
}
