using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrExpensesTypeProfile : Profile
    {
        public HrExpensesTypeProfile()
        {
            CreateMap<HrExpensesTypeDto, HrExpensesType>().ReverseMap();
            CreateMap<HrExpensesTypeEditDto, HrExpensesType>().ReverseMap();
        }
    }
}
