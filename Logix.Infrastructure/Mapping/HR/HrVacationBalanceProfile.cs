using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrVacationBalanceProfile : Profile
    {
        public HrVacationBalanceProfile()
        {
            CreateMap<HrVacationBalanceDto, HrVacationBalance>().ReverseMap();
            CreateMap<HrVacationBalanceEditDto, HrVacationBalance>().ReverseMap();
        }
    }
}
