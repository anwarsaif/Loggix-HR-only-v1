using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrLicenseProfile : Profile
    {
        public HrLicenseProfile()
        {
            CreateMap<HrLicenseDto, HrLicense>().ReverseMap();
            CreateMap<HrLicenseEditDto, HrLicense>().ReverseMap();
        }
    }
}
