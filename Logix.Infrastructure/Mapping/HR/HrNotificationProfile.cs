using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrNotificationProfile : Profile
    {
        public HrNotificationProfile() 
        {
            CreateMap<HrNotificationDto, HrNotification>().ReverseMap();
            CreateMap<HrNotificationEditDto, HrNotification>().ReverseMap();
        }
    }
}
