using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrPayrollDService : GenericQueryService<HrPayrollD, HrPayrollDDto, HrPayrollDVw>, IHrPayrollDService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
		private readonly ILocalizationService localization;
		private readonly IMainRepositoryManager _mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;


        public HrPayrollDService(IQueryRepository<HrPayrollD> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this._mainRepositoryManager = mainRepositoryManager;
			this.localization = localization;
			this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
        }

        public Task<IResult<HrPayrollDDto>> Add(HrPayrollDDto entity, CancellationToken cancellationToken = default)
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

        public Task<IResult<HrPayrollDEditDto>> Update(HrPayrollDEditDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }



        public async Task<IResult<List<PayrollAccountingEntryDto>>> GetHrPayrollDTrans(long msId, long FacilityId)
        {
            try
            {
                var resul = await hrRepositoryManager.HrPayrollDRepository.GetHrPayrollDTrans(msId, session.FacilityId);
                return await Result<List<PayrollAccountingEntryDto>>.SuccessAsync(resul, "", 200);
            }
            catch (Exception ex)
            {

                return await Result<List<PayrollAccountingEntryDto>>.FailAsync(ex.Message);
            }
        }
        private async Task<IResult<List<PayrollAccountingEntryResultDto>>> GetPayrollReports(HrPayrollFilterDto filter, int type)
        {
            try
            {
                var resul = await hrRepositoryManager.HrPayrollDRepository.GetPayrollReports(filter, type);
                return await Result<List<PayrollAccountingEntryResultDto>>.SuccessAsync(resul, "", 200);
            }
            catch (Exception ex)
            {

                return await Result<List<PayrollAccountingEntryResultDto>>.FailAsync(ex.Message);
            }
        }
        public async Task<IResult<List<HrPayrollCompareResult?>>> PayrollCompare(HrPayrollCompareFilterDto filter, int CmdType)
        {
            try
            {
                var resul = await hrRepositoryManager.HrPayrollDRepository.PayrollCompare(filter, CmdType);
                return await Result<List<HrPayrollCompareResult?>>.SuccessAsync(resul, "", 200);
            }
            catch (Exception ex)
            {

                return await Result<List<HrPayrollCompareResult>>.FailAsync(ex.Message);
            }
        }
        public async Task<IResult<HrDashboard2ResultDto>> GetPayrollReportsForHrDashboard2(HrDashboardDto filter)
        {
            var branchesList = session.Branches.Split(',');
            try
            {
                HrDashboard2ResultDto results = new HrDashboard2ResultDto();
                // توزيع الرواتب حسب المواقع
                var getAllPayrollD = await hrRepositoryManager.HrPayrollDRepository.GetAllVw(p =>
                    p.IsDeleted == false &&
                    branchesList.Contains(p.BranchId.ToString()) &&
                    (filter.BranchId == 0 || p.BranchId == filter.BranchId) &&
                    (filter.LocationId == 0 || p.Location == filter.LocationId) &&
                    (filter.DeptId == 0 || p.DeptId == filter.DeptId)
                );

                var AllPayrollDData = getAllPayrollD.AsQueryable();
                var LocationData = getAllPayrollD.AsQueryable();
                if (!string.IsNullOrEmpty(filter.StartDate) && !string.IsNullOrEmpty(filter.EndDate))
                {
                    var startDate = DateHelper.StringToDate(filter.StartDate);
                    var endDate = DateHelper.StringToDate(filter.EndDate);
                    LocationData = LocationData.Where(p =>
                        DateHelper.StringToDate($"{p.FinancelYear}/{p.MsMonth}/01") >= startDate &&
                        DateHelper.StringToDate($"{p.FinancelYear}/{p.MsMonth}/01") <= endDate
                    );
                }

                var locationResults = LocationData
                    .GroupBy(p => new { p.LocationName, p.LocationName2 })
                    .Select(g => new HrDashboardDto
                    {
                        Cnt = g.Sum(x => x.Net),
                        Name = g.Key.LocationName,
                        Name2 = g.Key.LocationName2
                    })
                    .Take(10)
                    .ToList();
                results.locationResults = locationResults.ToList();

                // الرواتب خلال سنة

                var monthQueryResult = AllPayrollDData;

                if (!string.IsNullOrEmpty(filter.StartDate))
                {
                    var startYear = filter.StartDate.Substring(0, 4);
                    monthQueryResult = monthQueryResult.Where(p => p.FinancelYear.ToString() == startYear);
                }

                var monthResults = monthQueryResult
                    .GroupBy(p => new { p.MonthName, p.FinancelYear, p.MsMonth })
                    .OrderBy(g => g.Key.FinancelYear)
                    .ThenBy(g => g.Key.MsMonth)
                    .Select(g => new HrDashboardDto
                    {
                        Cnt = g.Sum(x => x.Net),
                        Name = g.Key.MonthName,
                        Name2 = g.Key.MonthName
                    })
                    .Take(10)
                    .ToList();
                results.monthResults = monthResults.ToList();

                // توزيع الرواتب حسب البدلات
                var allowanceQuery = AllPayrollDData;

                var allowanceResult = await hrRepositoryManager.HrPayrollAllowanceVwRepository.GetAll(a =>
                    a.IsDeleted == false
                );

                var allowanceQueryWithFilter = (from p in allowanceQuery
                                                join a in allowanceResult on p.MsdId equals a.MsdId
                                                where p.IsDeleted == false && a.IsDeleted == false
                                                select new { p, a }).AsQueryable();

                if (!string.IsNullOrEmpty(filter.StartDate))
                {
                    var startYear = filter.StartDate.Substring(0, 4);
                    allowanceQueryWithFilter = allowanceQueryWithFilter.Where(x => x.p.FinancelYear.ToString() == startYear);
                }

                var allowanceResults = allowanceQueryWithFilter
                    .GroupBy(x => new { x.a.Name, x.a.Name2 })
                    .Select(g => new HrDashboardDto
                    {
                        Cnt = g.Sum(x => x.a.Amount),
                        Name = g.Key.Name,
                        Name2 = g.Key.Name2
                    })
                    .Take(10)
                    .ToList();
                results.allowanceData = allowanceResults.ToList();


                // توزيع الرواتب حسب الحسميات
                var deductionQuery = AllPayrollDData;

                var deductionResult = await hrRepositoryManager.HrPayrollDeductionVwRepository.GetAll(a =>
                    a.IsDeleted == false
                );

                var deductionQueryWithFilter = (from p in deductionQuery
                                                join a in deductionResult on p.MsdId equals a.MsdId
                                                where p.IsDeleted == false && a.IsDeleted == false
                                                select new { p, a }).AsQueryable();

                if (!string.IsNullOrEmpty(filter.StartDate))
                {
                    var startYear = filter.StartDate.Substring(0, 4);
                    deductionQueryWithFilter = deductionQueryWithFilter.Where(x => x.p.FinancelYear.ToString() == startYear);
                }

                var deductionResults = deductionQueryWithFilter
                    .GroupBy(x => new { x.a.Name, x.a.Name2 })
                    .Select(g => new HrDashboardDto
                    {
                        Cnt = g.Sum(x => x.a.Amount),
                        Name = g.Key.Name,
                        Name2 = g.Key.Name2
                    })
                    .Take(10)
                    .ToList();
                results.DeductionData = deductionResults.ToList();
                return await Result<HrDashboard2ResultDto>.SuccessAsync(results, "", 200);

            }
            catch (Exception exp)
            {

                return await Result<HrDashboard2ResultDto>.FailAsync(exp.Message.ToString());
            }

        }

		private async Task<IResult<List<PayrollAccountingEntryResultDto>>> GetPayrollBranchReports(HrPayrollFilterDto filter, int type)
		{
			try
			{
				var resul = await hrRepositoryManager.HrPayrollDRepository.GetPayrollReports(filter, type);
				return await Result<List<PayrollAccountingEntryResultDto>>.SuccessAsync(resul, "", 200);
			}
			catch (Exception ex)
			{

				return await Result<List<PayrollAccountingEntryResultDto>>.FailAsync(ex.Message);
			}
		}
		public async Task<IResult<List<PayrollAccountingEntryResultDto>>> Search(HrPayrollFilterDto filter, CancellationToken cancellationToken = default)
		{
			try
			{
				if (filter.FinancelYear == 0 || filter.FinancelYear == null)
				{
					return await Result<List<PayrollAccountingEntryResultDto>>.FailAsync(" يجب اختيار السنة المالية");
				}
				var BranchesList = session.Branches.Split(',');
				if (!string.IsNullOrEmpty(filter.MsMonth))
				{
					if (int.TryParse(filter.MsMonth, out int month))
					{
						filter.MsMonth = month.ToString("D2");
					}
				}
				var items = await GetPayrollReports(filter, 4);
				if (items.Succeeded)
				{
					if (items.Data.Count() > 0)
					{
						return await Result<List<PayrollAccountingEntryResultDto>>.SuccessAsync(items.Data, "");
					}
					return await Result<List<PayrollAccountingEntryResultDto>>.SuccessAsync(items.Data, localization.GetResource1("NosearchResult"));
				}
				return await Result<List<PayrollAccountingEntryResultDto>>.FailAsync(items.Status.message);
			}
			catch (Exception ex)
			{
				return await Result<List<PayrollAccountingEntryResultDto>>.FailAsync(ex.Message);
			}
		}

		public async Task<IResult<List<HrPayrollDVw>>> SearchPayrollQuery(HrPayrollQueryFilterDto filter, CancellationToken cancellationToken = default)
		{
			try
			{
				var BranchesList = session.Branches.Split(',');
				var items = await hrRepositoryManager.HrPayrollDRepository.GetAllVw(x =>
				x.IsDeleted == false &&
				BranchesList.Contains(x.BranchId.ToString()) &&
				(filter.PayrollTypeId == null || filter.PayrollTypeId == 0 || filter.PayrollTypeId == x.PayrollTypeId) &&
				(filter.FinancelYear == null || filter.FinancelYear == 0 || filter.FinancelYear == x.FinancelYear) &&
				(string.IsNullOrEmpty(filter.EmpCode) || filter.EmpCode == x.EmpId.ToString()) &&
				(string.IsNullOrEmpty(filter.EmpName) || x.EmpName.ToLower().Contains(filter.EmpName.ToLower())) &&
				(filter.Location == null || filter.Location == 0 || filter.Location == x.Location) &&
				(filter.DeptId == null || filter.DeptId == 0 || filter.DeptId == x.DeptId) &&
				(filter.FacilityId == null || filter.FacilityId == 0 || filter.FacilityId == x.FacilityId) &&
				(filter.FinancelYear == 0 || filter.FinancelYear == null || filter.FinancelYear == x.FinancelYear) &&
				(filter.PaymentTypeId == 0 || filter.PaymentTypeId == null || filter.PaymentTypeId == x.PaymentTypeId) &&
				(filter.JobId == 0 || filter.JobId == null || filter.JobId == Convert.ToInt32(x.JobId)) &&
				(string.IsNullOrEmpty(filter.MsMonth) || x.MsMonth == filter.MsMonth)
				);
				//if (items != null)
				//{
				//	if (items.Count() > 0)
				//	{
				//		var res = items.AsEnumerable();
				//		if (!string.IsNullOrEmpty(filter.FromDate))
				//		{
				//			res = res.Where(x => DateHelper.StringToDate(filter.FromDate) <= DateHelper.StringToDate(x.MsDate));
				//		}

				//		if (!string.IsNullOrEmpty(filter.ToDate))
				//		{
				//			res = res.Where(x => DateHelper.StringToDate(filter.ToDate) >= DateHelper.StringToDate(x.MsDate));
				//		}
				//		return await Result<List<HrPayrollDVw>>.SuccessAsync(new List<HrPayrollDVw>());
				//	}
				//	return await Result<List<HrPayrollDVw>>.SuccessAsync(items.ToList(), localization.GetResource1("NosearchResult"));
				//}
				//return await Result<List<HrPayrollDVw>>.FailAsync("");

				if (items == null)
					return await Result<List<HrPayrollDVw>>.FailAsync(localization.GetResource1("NoDataAvailable"));

				if (!items.Any())
					return await Result<List<HrPayrollDVw>>.SuccessAsync(new List<HrPayrollDVw>(), localization.GetResource1("NoSearchResults"));

				var res = items.AsEnumerable();

				if (!string.IsNullOrEmpty(filter.FromDate))
					res = res.Where(x => DateHelper.StringToDate(filter.FromDate) <= DateHelper.StringToDate(x.MsDate));

				if (!string.IsNullOrEmpty(filter.ToDate))
					res = res.Where(x => DateHelper.StringToDate(filter.ToDate) >= DateHelper.StringToDate(x.MsDate));

				var filteredResults = res.ToList();

				return await Result<List<HrPayrollDVw>>.SuccessAsync(filteredResults);
			}
			catch (Exception ex)
			{
				return await Result<List<HrPayrollDVw>>.FailAsync(ex.Message);
			}
		}

		public async Task<IResult<List<PayrollAccountingEntryResultDto>>> PayrollByLocationSearch(HrPayrollFilterDto filter, CancellationToken cancellationToken = default)
		{
			try
			{
				if (filter.FinancelYear == 0 || filter.FinancelYear == null)
				{
					return await Result<List<PayrollAccountingEntryResultDto>>.FailAsync(" يجب اختيار السنة المالية");
				}
				var BranchesList = session.Branches.Split(',');
				if (!string.IsNullOrEmpty(filter.MsMonth))
				{
					if (int.TryParse(filter.MsMonth, out int month))
					{
						filter.MsMonth = month.ToString("D2");
					}
				}
				var items = await GetPayrollReports(filter, 1);
				if (items.Succeeded)
				{
					if (items.Data.Count() > 0)
					{
						return await Result<List<PayrollAccountingEntryResultDto>>.SuccessAsync(items.Data.ToList(), "");
					}
					return await Result<List<PayrollAccountingEntryResultDto>>.SuccessAsync(items.Data.ToList(), localization.GetResource1("NosearchResult"));
				}
				return await Result<List<PayrollAccountingEntryResultDto>>.FailAsync(items.Status.message);
			}
			catch (Exception ex)
			{
				return await Result<List<PayrollAccountingEntryResultDto>>.FailAsync(ex.Message);
			}
		}

		public async Task<IResult<List<PayrollAccountingEntryResultDto>>> PayrollByDeptSearch(HrPayrollFilterDto filter, CancellationToken cancellationToken = default)
		{
			try
			{
				if (filter.FinancelYear == 0 || filter.FinancelYear == null)
				{
					return await Result<List<PayrollAccountingEntryResultDto>>.FailAsync(" يجب اختيار السنة المالية");
				}
				if (!string.IsNullOrEmpty(filter.MsMonth))
				{
					if (int.TryParse(filter.MsMonth, out int month))
					{
						filter.MsMonth = month.ToString("D2");
					}
				}

				var BranchesList = session.Branches.Split(',');
				var items = await GetPayrollReports(filter, 2);
				if (items.Succeeded)
				{
					if (items.Data.Count() > 0)
					{
						return await Result<List<PayrollAccountingEntryResultDto>>.SuccessAsync(items.Data.ToList(), "");
					}
					return await Result<List<PayrollAccountingEntryResultDto>>.SuccessAsync(items.Data.ToList(), localization.GetResource1("NosearchResult"));
				}
				return await Result<List<PayrollAccountingEntryResultDto>>.FailAsync(items.Status.message);
			}
			catch (Exception ex)
			{
				return await Result<List<PayrollAccountingEntryResultDto>>.FailAsync(ex.Message);
			}
		}

		public async Task<IResult<List<HrPayrollCompareResult>>> SearchComperByBranch(HrPayrollCompareFilterDto filter, CancellationToken cancellationToken = default)
		{
			try
			{
				if (string.IsNullOrEmpty(filter.PreviousMonth) || filter.PreviousMonth == "0" || filter.PreviousMonth == "00")
				{
					return await Result<List<HrPayrollCompareResult>>.FailAsync($"  يجب اختيار الشهر السابق");

				}
				if (string.IsNullOrEmpty(filter.CurrentMonth) || filter.CurrentMonth == "0" || filter.CurrentMonth == "00")
				{
					return await Result<List<HrPayrollCompareResult>>.FailAsync($"  يجب اختيار الشهر الحالي");

				}
				if (filter.BranchId <= 0)
				{
					return await Result<List<HrPayrollCompareResult>>.FailAsync($"  يجب اختيار الفرع");

				}
				if (filter.FinancialYear <= 0)
				{
					return await Result<List<HrPayrollCompareResult>>.FailAsync($"  يجب اختيار السنة المالية");

				}

				var getData = await PayrollCompare(filter, 5);
				if (getData.Data.Count() > 0)
				{
					return await Result<List<HrPayrollCompareResult>>.SuccessAsync(getData.Data.ToList());
				}
				else
				{
					return await Result<List<HrPayrollCompareResult>>.SuccessAsync(getData.Data.ToList(), "لا توجد نتائج للمقارنة ");
				}

			}
			catch (Exception ex)
			{
				return await Result<List<HrPayrollCompareResult>>.FailAsync(ex.Message);
			}
		}
	}

}