using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrCompetencesCatagoryProfile : Profile
    {
        public HrCompetencesCatagoryProfile()
        {
            CreateMap<HrCompetencesCatagoryDto, HrCompetencesCatagory>().ReverseMap();
            CreateMap<HrCompetencesCatagoryEditDto, HrCompetencesCatagory>().ReverseMap();
        }
    }
}
