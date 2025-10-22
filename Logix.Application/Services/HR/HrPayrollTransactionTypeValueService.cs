using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrPayrollTransactionTypeValueService : GenericQueryService<HrPayrollTransactionTypeValue, HrPayrollTransactionTypeValueDto, HrPayrollTransactionTypeValuesVw>, IHrPayrollTransactionTypeValueService
    {
        public HrPayrollTransactionTypeValueService(IQueryRepository<HrPayrollTransactionTypeValue> queryRepository, IMapper mapper) : base(queryRepository, mapper)
        {
        }

        public Task<IResult<HrPayrollTransactionTypeValueDto>> Add(HrPayrollTransactionTypeValueDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IResult<HrPayrollTransactionTypeValueEditDto>> Update(HrPayrollTransactionTypeValueEditDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
