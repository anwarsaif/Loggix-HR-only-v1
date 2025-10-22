using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrGosiEmployeeProfile : Profile
    {
        public HrGosiEmployeeProfile()
        {
            CreateMap<HrGosiEmployeeDto, HrGosiEmployee>().ReverseMap();
            CreateMap<HrGosiEmployeeEditDto, HrGosiEmployee>().ReverseMap();
        }
    }
}
