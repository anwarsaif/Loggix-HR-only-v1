using AutoMapper;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Services;
using Logix.Domain.HR;

public class HrPayrollDeductionAccountsVwService : GenericQueryService<HrPayrollDeductionAccountsVw, HrPayrollDeductionAccountsVw, HrPayrollDeductionAccountsVw>, IHrPayrollDeductionAccountsVwService
{

    private readonly IMapper mapper;


    public HrPayrollDeductionAccountsVwService(IQueryRepository<HrPayrollDeductionAccountsVw> queryRepository, IMapper mapper) : base(queryRepository, mapper)
        {
          this.mapper = mapper;

        }
    }
    