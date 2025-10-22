using AutoMapper;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Domain.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.Services.HR
{
    public class HrContractsDeductionVwService : GenericQueryService<HrContractsDeductionVw, HrContractsDeductionVw, HrContractsDeductionVw>, IHrContractsDeductionVwService
    {
        public HrContractsDeductionVwService(IQueryRepository<HrContractsDeductionVw> queryRepository, IMapper mapper) : base(queryRepository, mapper)
        {
        }
    }
}
