using AutoMapper;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrAttDayService : GenericQueryService<HrAttDay, HrAttDay>, IHrAttDayService
    {
        public HrAttDayService(IQueryRepository<HrAttDay> queryRepository, IMapper mapper) : base(queryRepository, mapper)
        {

        }
    }
    }