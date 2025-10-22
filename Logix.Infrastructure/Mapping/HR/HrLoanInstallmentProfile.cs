using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrLoanInstallmentProfile : Profile
    {
        public HrLoanInstallmentProfile()
        {
            CreateMap<HrLoanInstallmentDto, HrLoanInstallment>().ReverseMap();
            CreateMap<HrLoanInstallmentEditDto, HrLoanInstallment>().ReverseMap();
        }
    }
}
