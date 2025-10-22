using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrDisciplinaryCaseProfile : Profile
    {
        public HrDisciplinaryCaseProfile()
        {
            CreateMap<HrDisciplinaryCaseDto, HrDisciplinaryCase>().ReverseMap();
            CreateMap<HrDisciplinaryCaseEditDto, HrDisciplinaryCase>().ReverseMap();
        }
    }
}
