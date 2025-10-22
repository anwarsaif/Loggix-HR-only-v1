using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrMandateLocationMasterProfile : Profile
    {
        public HrMandateLocationMasterProfile()
        {
            CreateMap<HrMandateLocationMasterDto, HrMandateLocationMaster>().ReverseMap();
            CreateMap<HrMandateLocationMasterEditDto, HrMandateLocationMaster>().ReverseMap();
        }
    }
}
