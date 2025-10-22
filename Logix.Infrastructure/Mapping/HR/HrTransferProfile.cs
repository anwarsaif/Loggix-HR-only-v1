using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Infrastructure.Mapping.HR
{
    public class HrTransferProfile : Profile
    {
        public HrTransferProfile()
        {
            CreateMap<HrTransferDto, HrTransfer>().ReverseMap();
            CreateMap<HrTransferEditDto, HrTransfer>().ReverseMap();
            CreateMap<HrTransfersAllAddDto, HrTransfer>().ReverseMap();
            CreateMap<HrTransfersAdd2Dto, HrTransfer>().ReverseMap();
        }
    }
}
