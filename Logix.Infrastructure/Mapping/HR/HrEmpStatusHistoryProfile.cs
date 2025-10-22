using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrEmpStatusHistoryProfile : Profile
    {
        public HrEmpStatusHistoryProfile()
        {
            CreateMap<HrEmpStatusHistoryDto, HrEmpStatusHistory>().ReverseMap();
            CreateMap<HrEmpStatusHistoryEditDto, HrEmpStatusHistory>().ReverseMap();
        }
    } 
}
