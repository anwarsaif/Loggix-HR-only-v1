using Logix.Application.DTOs.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrLoanPaymentService : IGenericQueryService<HrLoanPaymentDto, HrLoanPaymentVw>, IGenericWriteService<HrLoanPaymentDto, HrLoanPaymentEditDto>
    {
        Task<IResult<string>> DeleteHrLoanPayment(long Id, CancellationToken cancellationToken = default);
        Task<IResult<HrLoanPaymentDto>> Add(HrLoanPaymentAddDto entity, CancellationToken cancellationToken = default);

    }


}
