using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrLoanInstallmentPaymentProfile : Profile
    {
        public HrLoanInstallmentPaymentProfile()
        {
            CreateMap<HrLoanInstallmentPaymentDto, HrLoanInstallmentPayment>().ReverseMap();
            CreateMap<HrLoanInstallmentPaymentEditDto, HrLoanInstallmentPayment>().ReverseMap();
        }
    }
}
