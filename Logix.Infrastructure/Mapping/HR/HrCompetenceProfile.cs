using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrCompetenceProfile : Profile
    {
        public HrCompetenceProfile()
        {
            CreateMap<HrCompetenceDto, HrCompetence>().ReverseMap();
            CreateMap<HrCompetenceEditDto, HrCompetence>().ReverseMap();
        }
    }
}
