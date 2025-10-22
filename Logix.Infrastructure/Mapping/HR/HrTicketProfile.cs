using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrTicketProfile : Profile
    {
        public HrTicketProfile()
        {
            CreateMap<HrTicketDto, HrTicket>().ReverseMap();
            CreateMap<HrTicketEditDto, HrTicket>().ReverseMap();
        }
    }
}
