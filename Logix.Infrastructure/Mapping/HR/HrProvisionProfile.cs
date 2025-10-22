using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrProvisionProfile : Profile
    {
        public HrProvisionProfile()
        {
            CreateMap<HrProvisionDto, HrProvision>().ReverseMap();
            CreateMap<HrProvisionEditDto, HrProvision>().ReverseMap();
            CreateMap<HrProvisionEditDto, HrProvisionsVw>().ReverseMap();
        }
    }
}
