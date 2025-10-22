using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrJobOfferService : IGenericQueryService<HrJobOfferDto, HrJobOfferVw>, IGenericWriteService<HrJobOfferDto, HrJobOfferEditDto>
    {

    }

}
