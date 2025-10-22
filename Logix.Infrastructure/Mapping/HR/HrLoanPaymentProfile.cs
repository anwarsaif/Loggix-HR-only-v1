using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrLoanPaymentProfile : Profile
    {
        public HrLoanPaymentProfile()
        {
            CreateMap<HrLoanPaymentDto, HrLoanPayment>().ReverseMap();
            CreateMap<HrLoanPaymentEditDto, HrLoanPayment>().ReverseMap();
        }
    }
}
