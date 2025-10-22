using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrArchivesFileProfile : Profile
    {
        public HrArchivesFileProfile()
        {
            CreateMap<HrArchivesFileDto, HrArchivesFile>().ReverseMap();
            CreateMap<HrArchivesFileEditDto, HrArchivesFile>().ReverseMap();
        }
    }
}
