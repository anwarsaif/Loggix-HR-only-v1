using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrTrainingBagProfile : Profile
    {
        public HrTrainingBagProfile()
        {
            CreateMap<HrTrainingBagDto, HrTrainingBag>().ReverseMap();
            CreateMap<HrTrainingBagEditDto, HrTrainingBag>().ReverseMap();
        }
    }
}
