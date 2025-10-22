using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrExpensesScheduleProfile : Profile
    {
        public HrExpensesScheduleProfile()
        {
            CreateMap<HrExpensesScheduleDto, HrExpensesSchedule>().ReverseMap();
            CreateMap<HrExpensesScheduleEditDto, HrExpensesSchedule>().ReverseMap();
        }
    }
}
