using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrExpensesPaymentProfile : Profile
    {
        public HrExpensesPaymentProfile()
        {
            CreateMap<HrExpensesPaymentDto, HrExpensesPayment>().ReverseMap();
            CreateMap<HrExpensesPaymentEditDto, HrExpensesPayment>().ReverseMap();
        }
    }
}
