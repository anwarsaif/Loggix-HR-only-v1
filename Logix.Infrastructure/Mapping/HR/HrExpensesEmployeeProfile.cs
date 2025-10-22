using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrExpensesEmployeeProfile : Profile
    {
        public HrExpensesEmployeeProfile()
        {
            CreateMap<HrExpensesEmployeeDto, HrExpensesEmployee>().ReverseMap();
            CreateMap<HrExpensesEmployeeEditDto, HrExpensesEmployee>().ReverseMap();
        }
    }
}
