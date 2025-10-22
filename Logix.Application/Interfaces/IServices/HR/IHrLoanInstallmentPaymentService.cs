using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrLoanInstallmentPaymentService : IGenericQueryService<HrLoanInstallmentPaymentDto, HrLoanInstallmentPaymentVw>, IGenericWriteService<HrLoanInstallmentPaymentDto, HrLoanInstallmentPaymentEditDto>
    {

    }


}
