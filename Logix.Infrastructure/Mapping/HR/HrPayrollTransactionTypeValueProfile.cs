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
    public class HrPayrollTransactionTypeValueProfile : Profile
    {
        public HrPayrollTransactionTypeValueProfile()
        {
            CreateMap<HrPayrollTransactionTypeValueDto, HrPayrollTransactionTypeValue>().ReverseMap();
            CreateMap<HrPayrollTransactionTypeValueEditDto, HrPayrollTransactionTypeValue>().ReverseMap();
        }
    }
}
