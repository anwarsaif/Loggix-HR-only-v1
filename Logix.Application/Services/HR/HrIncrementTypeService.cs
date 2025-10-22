using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;
using System.Linq.Expressions;

namespace Logix.Application.Services.HR
{
    public class HrIncrementTypeService : GenericQueryService<HrIncrementType, HrIncrementTypeDto, HrIncrementType>, IHrIncrementTypeService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;


        public HrIncrementTypeService(IQueryRepository<HrIncrementType> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
        }

        public Task<IResult<IEnumerable<HrIncrementTypeDto>>> GetAllVW(Expression<Func<HrIncrementTypeDto, bool>> expression, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }


        public Task<IResult<HrIncrementTypeDto>> GetOneVW(Expression<Func<HrIncrementTypeDto, bool>> expression, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }


    }
    }