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
    public class HrPayrollTransactionTypeProfile : Profile
    {
        public HrPayrollTransactionTypeProfile()
        {
            CreateMap<HrPayrollTransactionTypeDto, HrPayrollTransactionType>().ReverseMap();
            CreateMap<HrPayrollTransactionTypeEditDto, HrPayrollTransactionType>().ReverseMap();
        }
    }
}
