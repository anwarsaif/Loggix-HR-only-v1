using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrProvisionsEmployeeProfile : Profile
    {
        public HrProvisionsEmployeeProfile()
        {
            CreateMap<HrProvisionsEmployeeDto, HrProvisionsEmployee>().ReverseMap();
            CreateMap<HrProvisionsEmployeeEditDto, HrProvisionsEmployee>().ReverseMap();
            CreateMap<HrProvisionEmployeeResultDto, HrProvisionsEmployeeVw>().ReverseMap();

        }
    }
}
