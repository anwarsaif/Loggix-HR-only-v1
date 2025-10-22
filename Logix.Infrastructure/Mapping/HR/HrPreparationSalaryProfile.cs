using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrPreparationSalaryProfile : Profile
    {
        public HrPreparationSalaryProfile()
        {
            CreateMap<HrPreparationSalaryDto, HrPreparationSalary>().ReverseMap();
            CreateMap<HrPreparationSalaryEditDto, HrPreparationSalary>().ReverseMap();
            CreateMap<HrPreparationSalariesDataDto, HrPreparationSalariesVw>().ReverseMap();
            CreateMap<HrPreparationSalaryEditDto, HrPreparationCommissionUpdateDto>().ReverseMap();
        }
    }
}
