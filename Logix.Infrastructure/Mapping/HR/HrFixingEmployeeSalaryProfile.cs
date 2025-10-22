using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrFixingEmployeeSalaryProfile : Profile
    {
        public HrFixingEmployeeSalaryProfile()
        {
            CreateMap<HrFixingEmployeeSalaryDto, HrFixingEmployeeSalary>().ReverseMap();
            CreateMap<HrFixingEmployeeSalaryEditDto, HrFixingEmployeeSalary>().ReverseMap();
        }
    }
}
