using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrAssignmenProfile : Profile
    {
        public HrAssignmenProfile()
        {
            CreateMap<HrAssignmenDto, HrAssignman>().ReverseMap();
            CreateMap<HrAssignmenEditDto, HrAssignman>().ReverseMap();
        }
    } 
}
