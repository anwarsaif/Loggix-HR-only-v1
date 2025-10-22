using AutoMapper;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.Services.HR
{
    public class HrPayrollTransactionTypeService : GenericQueryService<HrPayrollTransactionType, HrPayrollTransactionTypeDto>, IHrPayrollTransactionTypeService
    {
        public HrPayrollTransactionTypeService(IQueryRepository<HrPayrollTransactionType> queryRepository, IMapper mapper) : base(queryRepository, mapper)
        {
        }

        public Task<IResult<HrPayrollTransactionTypeDto>> Add(HrPayrollTransactionTypeDto entity, CancellationToken cancellationToken = default)
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

        public Task<IResult<HrPayrollTransactionTypeEditDto>> Update(HrPayrollTransactionTypeEditDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
