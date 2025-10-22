using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrDisciplinaryCaseActionProfile : Profile
    {
        public HrDisciplinaryCaseActionProfile()
        {
            CreateMap<HrDisciplinaryCaseActionDto, HrDisciplinaryCaseAction>().ReverseMap();
            CreateMap<HrDisciplinaryCaseActionEditDto, HrDisciplinaryCaseAction>().ReverseMap();
        }
    }
}
