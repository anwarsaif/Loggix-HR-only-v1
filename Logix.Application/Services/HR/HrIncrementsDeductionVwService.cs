using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Helpers;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices;
using Logix.Application.Interfaces.IServices.ACC;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.ACC;
using Logix.Domain.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.Services.HR
{

    public class HrIncrementsDeductionVwService : GenericQueryService<HrIncrementsDeductionVw, HrIncrementsDeductionVw, HrIncrementsDeductionVw>, IHrIncrementsDeductionVwService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;

        private readonly IMapper _mapper;
        private readonly ICurrentData session;

        public HrIncrementsDeductionVwService(IQueryRepository<HrIncrementsDeductionVw> queryRepository, IHrRepositoryManager hrRepositoryManager, IMapper mapper, ICurrentData session) : base(queryRepository, mapper)
        {
            this.hrRepositoryManager = hrRepositoryManager;
            this._mapper = mapper;

            this.session = session;
        }
    }
}
