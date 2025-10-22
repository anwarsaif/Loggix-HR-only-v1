using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrFileProfile : Profile
    {
        public HrFileProfile()
        {
            CreateMap<HrFileDto, HrFile>().ReverseMap();
            CreateMap<HrFileEditDto, HrFile>().ReverseMap();
        }
    }
}
