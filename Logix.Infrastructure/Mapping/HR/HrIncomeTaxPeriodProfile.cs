using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrIncomeTaxPeriodProfile : Profile
    {
        public HrIncomeTaxPeriodProfile()
        {
            CreateMap<HrIncomeTaxPeriodDto, HrIncomeTaxPeriod>().ReverseMap();
            CreateMap<HrIncomeTaxPeriodEditDto, HrIncomeTaxPeriod>().ReverseMap();
        }
    }
}

