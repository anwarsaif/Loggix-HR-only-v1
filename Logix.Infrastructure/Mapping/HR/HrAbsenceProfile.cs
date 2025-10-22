using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrAbsenceProfile : Profile
    {
        public HrAbsenceProfile()
        {
            CreateMap<HrAbsenceDto, HrAbsence>().ReverseMap();
            CreateMap<HrAbsenceEditDto, HrAbsence>().ReverseMap();
        }
    }
}
