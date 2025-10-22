using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrArchiveFilesDetailProfile : Profile
    {
        public HrArchiveFilesDetailProfile()
        {
            CreateMap<HrArchiveFilesDetailDto, HrArchiveFilesDetail>().ReverseMap();
            CreateMap<HrArchiveFilesDetailEditDto, HrArchiveFilesDetail>().ReverseMap();
        }
    }
}
