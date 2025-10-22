using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrLoanProfile : Profile
    {
        public HrLoanProfile()
        {
            CreateMap<HrLoanDto, HrLoan>().ReverseMap();
            CreateMap<HrLoan4Dto, HrLoan>().ReverseMap();
            CreateMap<HrLoanEditDto, HrLoan>().ReverseMap();
            CreateMap<HrEditLoanDto, HrLoan>().ReverseMap();
        }
    }
}
