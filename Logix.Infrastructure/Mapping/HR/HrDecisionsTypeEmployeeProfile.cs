using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrDecisionsTypeEmployeeProfile : Profile
    {
        public HrDecisionsTypeEmployeeProfile()
        {
            CreateMap<HrDecisionsTypeEmployeeDto, HrDecisionsTypeEmployee>().ReverseMap();
            CreateMap<HrDecisionsTypeEmployeeEditDto, HrDecisionsTypeEmployee>().ReverseMap();
        }
    }
}
