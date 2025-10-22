using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrDecisionsTypeProfile : Profile
    {
        public HrDecisionsTypeProfile()
        {
            CreateMap<HrDecisionsTypeDto, HrDecisionsType>().ReverseMap();
            CreateMap<HrDecisionsTypeEditDto, HrDecisionsType>().ReverseMap();
            CreateMap<HrDecisionsTypeGetByIdDto, HrDecisionsTypeVw>().ReverseMap();
        }
    }
}
