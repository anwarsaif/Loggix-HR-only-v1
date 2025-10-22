using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Helpers;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.Services.HR
{
	public class HrAllReportsService : IHrAllReportsService
	{
		private readonly IHrRepositoryManager hrRepositoryManager;
		private readonly IMainRepositoryManager mainRepositoryManager;
		private readonly IMapper _mapper;
		private readonly ILocalizationService localization;
		private readonly ICurrentData session;
		private readonly ISysConfigurationAppHelper configuration;

		public HrAllReportsService(
			IHrRepositoryManager hrRepositoryManager,
			IMainRepositoryManager mainRepositoryManager,
			IMapper mapper, ICurrentData session, ILocalizationService localization, ISysConfigurationAppHelper configuration)
		{
			this.hrRepositoryManager = hrRepositoryManager;
			this.mainRepositoryManager = mainRepositoryManager;
			_mapper = mapper;
			this.localization = localization;
			this.session = session;
			this.configuration = configuration;
		}

		public async Task<HrPayrollCompareFilterDto> GetHrCompareByBranch(Dictionary<string, string> dictionary)
		{
			return new HrPayrollCompareFilterDto()
			{
				CurrentMonth = dictionary["CurrentMonth"],
				PreviousMonth = dictionary["PreviousMonth"],
				FinancialYear = Convert.ToInt32(dictionary["FinancialYear"]),
				BranchId = Convert.ToInt32(dictionary["BranchId"]),
			};
		}
	}
}
