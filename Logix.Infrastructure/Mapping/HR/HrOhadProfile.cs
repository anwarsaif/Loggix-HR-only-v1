using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrOhadProfile : Profile
    {
        public HrOhadProfile()
        {
            CreateMap<HrOhadDto, HrOhad>().ReverseMap();
            CreateMap<HrOhadEditDto, HrOhad>().ReverseMap();
        }
    } 
}
