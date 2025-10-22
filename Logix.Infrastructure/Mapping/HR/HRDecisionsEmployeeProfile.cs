using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrDecisionsEmployeeProfile : Profile
    {
        public HrDecisionsEmployeeProfile()
        {
            CreateMap<HrDecisionsEmployeeDto, HrDecisionsEmployee>().ReverseMap();
            CreateMap<HrDecisionsEmployeeEditDto, HrDecisionsEmployee>().ReverseMap();
        }
    } 
   
}
