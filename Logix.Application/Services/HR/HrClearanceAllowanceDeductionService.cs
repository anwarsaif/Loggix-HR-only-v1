using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrClearanceAllowanceDeductionService : GenericQueryService<HrClearanceAllowanceDeduction, HrClearanceAllowanceDeductionDto, HrClearanceAllowanceVw>, IHrClearanceAllowanceDeductionService
    {
        public HrClearanceAllowanceDeductionService(IQueryRepository<HrClearanceAllowanceDeduction> queryRepository, IMapper mapper) : base(queryRepository, mapper)
        {
        }

        public Task<IResult<HrClearanceAllowanceDeductionDto>> Add(HrClearanceAllowanceDeductionDto entity, CancellationToken cancellationToken = default)
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

        public Task<IResult<HrClearanceAllowanceDeductionDto>> Update(HrClearanceAllowanceDeductionDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
