using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrDisciplinaryActionTypeProfile : Profile
    {
        public HrDisciplinaryActionTypeProfile()
        {
            CreateMap<HrDisciplinaryActionTypeDto, HrDisciplinaryActionType>().ReverseMap();
            CreateMap<HrDisciplinaryActionTypeEditDto, HrDisciplinaryActionType>().ReverseMap();
        }
    }
}
