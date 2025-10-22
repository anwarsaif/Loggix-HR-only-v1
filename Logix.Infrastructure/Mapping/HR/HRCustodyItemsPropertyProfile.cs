using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrCustodyItemsPropertyProfile : Profile
    {
        public HrCustodyItemsPropertyProfile()
        {
            CreateMap<HrCustodyItemsPropertyDto, HrCustodyItemsProperty>().ReverseMap();
            CreateMap<HrCustodyItemsPropertyEditDto, HrCustodyItemsProperty>().ReverseMap();
        }
    }
   
}
