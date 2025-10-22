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
    public class HrIncomeTaxSlideProfile : Profile
    {
        public HrIncomeTaxSlideProfile()
        {
            CreateMap<HrIncomeTaxSlideDto, HrIncomeTaxSlide>().ReverseMap();
            CreateMap<HrIncomeTaxSlideEditDto, HrIncomeTaxSlide>().ReverseMap();
        }
    }
}
