using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrAttShiftCloseDProfile : Profile
    {
        public HrAttShiftCloseDProfile()
        {
            CreateMap<HrAttShiftCloseDDto, HrAttShiftCloseD>().ReverseMap();
            CreateMap<HrAttShiftCloseDEditDto, HrAttShiftCloseD>().ReverseMap();
        }
    } 
}
