using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrFlexibleWorkingMasterProfile : Profile
    {
        public HrFlexibleWorkingMasterProfile()
        {
            CreateMap<HrFlexibleWorkingMasterDto, HrFlexibleWorkingMaster>().ReverseMap();
            CreateMap<HrFlexibleWorkingMasterEditDto, HrFlexibleWorkingMaster>().ReverseMap();
        }
    }
}
