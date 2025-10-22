using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrNoteProfile : Profile
    {
        public HrNoteProfile()
        {
            CreateMap<HrNoteDto, HrNote>().ReverseMap();
            CreateMap<HrNoteEditDto, HrNote>().ReverseMap();
        }
    }
}
