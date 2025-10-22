using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrVacationsCatagoryProfile : Profile
    {
        public HrVacationsCatagoryProfile()
        {
            CreateMap<HrVacationsCatagoryDto, HrVacationsCatagory>().ReverseMap();
            CreateMap<HrVacationsCatagoryEditDto, HrVacationsCatagory>().ReverseMap();
        }
    }
}
