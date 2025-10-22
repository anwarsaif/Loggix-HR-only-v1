using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrContracteProfile : Profile
    {
        public HrContracteProfile()
        {
            CreateMap<HrContracteDto, HrContracte>().ReverseMap();
            CreateMap<HrContracteAdd2Dto, HrContracte>().ReverseMap();
            CreateMap<HrContracteAdd3Dto, HrContracte>().ReverseMap();
            CreateMap<HrContracteEditDto, HrContracte>().ReverseMap();
            CreateMap<HrEmployeeVw, HrContractAddResponseDto>().ReverseMap();
        }
    } 
   
}
