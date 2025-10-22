using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Application.DTOs.Main;
using Logix.Domain.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrEmployeeProfile: Profile
    {
        public HrEmployeeProfile()
        {
            CreateMap<HrEmployeeDto, HrEmployee>().ReverseMap();
            CreateMap<HrEmployeeEditDto, HrEmployee>().ReverseMap();
            CreateMap<EmpDataForEditDto, HrEmployeeVw>().ReverseMap();
        }
    }
}
