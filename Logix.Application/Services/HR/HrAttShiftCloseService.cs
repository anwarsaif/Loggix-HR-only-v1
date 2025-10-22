using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;
namespace Logix.Application.Services.HR
{
    public class HrAttShiftCloseService : GenericQueryService<HrAttShiftClose, HrAttShiftCloseDto, HrAttShiftClose>, IHrAttShiftCloseService
    {
        public HrAttShiftCloseService(IQueryRepository<HrAttShiftClose> queryRepository, IMapper mapper) : base(queryRepository, mapper)
        {
        }

        public Task<IResult<HrAttShiftCloseDto>> Add(HrAttShiftCloseDto entity, CancellationToken cancellationToken = default)
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

        public Task<IResult<HrAttShiftCloseEditDto>> Update(HrAttShiftCloseEditDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
