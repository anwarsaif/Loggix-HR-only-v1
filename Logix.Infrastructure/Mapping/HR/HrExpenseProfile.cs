using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrExpenseProfile : Profile
    {
        public HrExpenseProfile()
        {
            CreateMap<HrExpenseDto, HrExpense>().ReverseMap();
            CreateMap<HrExpenseEditDto, HrExpense>().ReverseMap();
        }
    }
}
