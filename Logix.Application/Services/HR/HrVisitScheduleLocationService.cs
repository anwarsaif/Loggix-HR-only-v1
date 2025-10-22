using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrVisitScheduleLocationService : GenericQueryService<HrVisitScheduleLocation, HrVisitScheduleLocationDto, HrVisitScheduleLocationVw>, IHrVisitScheduleLocationService
    {
        private readonly IHrRepositoryManager _hrRepositoryManager;

        public HrVisitScheduleLocationService(IQueryRepository<HrVisitScheduleLocation> queryRepository,
            IMapper mapper,
            IHrRepositoryManager hrRepositoryManager) : base(queryRepository, mapper)
        {
            _hrRepositoryManager = hrRepositoryManager;
        }

        public Task<IResult<HrVisitScheduleLocationDto>> Add(HrVisitScheduleLocationDto entity, CancellationToken cancellationToken = default)
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

        public Task<IResult<HrVisitScheduleLocationEditDto>> Update(HrVisitScheduleLocationEditDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetNewVisitCount(long groupId, string branchesId, CancellationToken cancellationToken = default)
        {
            try
            {
                int count = await _hrRepositoryManager.HrVisitScheduleLocationRepository.GetNewVisitCount(groupId, branchesId);
                return count;
            }
            catch
            {
                return 0;
            }
        }
    }
}
