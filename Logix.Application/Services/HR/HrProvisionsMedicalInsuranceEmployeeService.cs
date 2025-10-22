using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
	public class HrProvisionsMedicalInsuranceEmployeeService : GenericQueryService<HrProvisionsMedicalInsuranceEmployee, HrProvisionsMedicalInsuranceEmployeeDto, HrProvisionsMedicalInsuranceEmployeeVw>, IHrProvisionsMedicalInsuranceEmployeeService
	{
		private readonly IHrRepositoryManager hrRepositoryManager;
		private readonly ILocalizationService localization;
		private readonly IMainRepositoryManager mainRepositoryManager;
		private readonly IMapper _mapper;
		private readonly ICurrentData session;
		private readonly IAccRepositoryManager accRepositoryManager;


		public HrProvisionsMedicalInsuranceEmployeeService(IQueryRepository<HrProvisionsMedicalInsuranceEmployee> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization, IAccRepositoryManager accRepositoryManager) : base(queryRepository, mapper)
		{
			this.mainRepositoryManager = mainRepositoryManager;
			this._mapper = mapper;
			this.session = session;
			this.hrRepositoryManager = hrRepositoryManager;
			this.localization = localization;
			this.accRepositoryManager = accRepositoryManager;
		}

		public Task<IResult<HrProvisionsMedicalInsuranceEmployeeDto>> Add(HrProvisionsMedicalInsuranceEmployeeDto entity, CancellationToken cancellationToken = default)
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

		public Task<IResult<HrProvisionsMedicalInsuranceEmployeeEditDto>> Update(HrProvisionsMedicalInsuranceEmployeeEditDto entity, CancellationToken cancellationToken = default)
		{
			throw new NotImplementedException();
		}
	}

}