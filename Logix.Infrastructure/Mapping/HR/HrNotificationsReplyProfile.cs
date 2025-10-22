using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrNotificationsReplyProfile : Profile
    {
        public HrNotificationsReplyProfile()
        {
            CreateMap<HrNotificationsReplyDto, HrNotificationsReply>().ReverseMap();
            CreateMap<HrNotificationsReplyEditDto, HrNotificationsReply>().ReverseMap();
        }
    }
}
