using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrMandateRequestsMasterProfile : Profile
    {
        public HrMandateRequestsMasterProfile()
        {
            CreateMap<HrMandateRequestsMasterDto, HrMandateRequestsMaster>().ReverseMap();
            CreateMap<HrMandateRequestsMasterEditDto, HrMandateRequestsMaster>().ReverseMap();
        }
    }
}
