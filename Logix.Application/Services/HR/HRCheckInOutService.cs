using AutoMapper;
using iText.Commons.Bouncycastle.Asn1.X509;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static SkiaSharp.HarfBuzz.SKShaper;

namespace Logix.Application.Services.HR
{
    public class HrCheckInOutService : GenericQueryService<HrCheckInOut, HrCheckInOutDto, HrCheckInOutVw>, IHrCheckInOutService
    {
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        private readonly IMapper mapper;
        private readonly ICurrentData session;


        public HrCheckInOutService(IQueryRepository<HrCheckInOut> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this.mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
        }


        public Task<IResult<HrCheckInOutDto>> Add(HrCheckInOutDto entity, CancellationToken cancellationToken = default)
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

        public Task<IResult<HrCheckInOutDto>> Update(HrCheckInOutDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IResult<string>> ChangeType(long Id, CancellationToken cancellationToken = default)
        {
            if (Id <= 0) return await Result<string>.FailAsync($"Error in {GetType()} : the passed entity IS NULL.");
            try
            {

                var item = await hrRepositoryManager.HrCheckInOutRepository.GetById(Id);

                if (item == null) return await Result<string>.FailAsync($"--- there is no Data with this id: {Id}---");

                if (item.Checktype == 1)
                {
                    item.Checktype = 2;
                }
                else
                {
                    item.Checktype = 1;
                }

                hrRepositoryManager.HrCheckInOutRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<string>.SuccessAsync("Item updated successfully", 200);
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<string>.FailAsync($"EXP in UpdateStatus at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<string>> UpdateCheckInOut(HrUpdateCheckINout entity, CancellationToken cancellationToken = default)
        {
            try
            {
                var getAllCheckInOut = await hrRepositoryManager.HrCheckInOutRepository.GetAll(x => x.SendActualAttendance == false);
                if (getAllCheckInOut == null || !getAllCheckInOut.Any())
                {
                    return await Result<string>.SuccessAsync(data: null, message: localization.GetResource1("NotAbleShowResults"));
                }

                var res = getAllCheckInOut.AsQueryable();

                if (!string.IsNullOrEmpty(entity.EmpCode))
                {
                    var getEmp = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.Isdel == false && x.IsDeleted == false && x.EmpId == entity.EmpCode);
                    if (getEmp == null)
                        return await Result<string>.SuccessAsync(localization.GetResource1("EmployeeNotFound"));

                    res = res.Where(x => x.EmpId == getEmp.Id);
                }

                if (!string.IsNullOrEmpty(entity.FromDate) && !string.IsNullOrEmpty(entity.ToDate))
                {
                    var from = DateHelper.StringToDate(entity.FromDate);
                    var to = DateHelper.StringToDate(entity.ToDate);
                    res = res.Where(r => r.Checktime.HasValue && r.Checktime.Value.Date >= from && r.Checktime.Value.Date <= to);
                }

                var grouped = res
                    .Where(x => x.Checktype.HasValue)
                    .GroupBy(x => new { x.EmpId, Date = x.Checktime.Value.Date })
                    .ToList();

                foreach (var group in grouped)
                {
                    var empId = group.Key.EmpId;
                    var date = group.Key.Date;

                    var checkIn = group.Where(g => g.Checktype == 1).OrderBy(g => g.Checktime).FirstOrDefault();
                    var checkOut = group.Where(g => g.Checktype == 2).OrderByDescending(g => g.Checktime).FirstOrDefault();

                    if (checkIn != null && checkOut != null)
                    {
                        var totalMinutes = (int)(checkOut.Checktime.Value - checkIn.Checktime.Value).TotalMinutes;
                        var totalTimeFormatted = $"{totalMinutes / 60}:{totalMinutes % 60:D2}";

                        await hrRepositoryManager.HrActualAttendanceRepository.Add(new HrActualAttendance
                        {
                            EmpId = empId,
                            Checktimein = checkIn.Checktime.Value,
                            Checktimeout = checkOut.Checktime.Value,
                            Date = date.Date.ToString(),
                            //TotalTime = totalTimeFormatted,
                            //crea = DateTime.Now
                        });
                    }
                }

                // Update Send_ActualAttendance flag
                foreach (var item in res)
                {
                    item.SendActualAttendance = true;
                    hrRepositoryManager.HrCheckInOutRepository.Update(item);
                }

                return await Result<string>.SuccessAsync(localization.GetResource1("ActionSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<string>.FailAsync($"EXP in UpdateCheckInOut at ({this.GetType()}) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

		public async Task<IResult<List<HrAttendanceUnknownFilterDto>>> Search(HrAttendanceUnknownFilterDto filter, CancellationToken cancellationToken = default)
		{
			try
			{
				var BranchesList = session.Branches.Split(',');
				List<HrAttendanceUnknownFilterDto> resultList = new List<HrAttendanceUnknownFilterDto>();
				var items = await hrRepositoryManager.HrCheckInOutRepository.GetAllVw(e => e.IsSend == false && BranchesList.Contains(e.BranchId.ToString()) && e.Checktime != null && e.Checktype != null);
				if (items != null)
				{
					if (items.Count() > 0)
					{

						var res = items.AsQueryable();
						if (!string.IsNullOrEmpty(filter.EmpCode))
						{
							res = res.Where(c => c.EmpCode != null && c.EmpCode == filter.EmpCode);
						}
						if (!string.IsNullOrEmpty(filter.EmpName))
						{
							res = res.Where(c => (c.EmpName != null && c.EmpName.ToLower().Contains(filter.EmpName.ToLower())));
						}
						if (!string.IsNullOrEmpty(filter.FromDate) && !string.IsNullOrEmpty(filter.ToDate))
						{
							res = res.Where(r =>
							(r.Checktime >= DateHelper.StringToDate(filter.FromDate)) &&
						   (r.Checktime <= DateHelper.StringToDate(filter.ToDate))
						   );
						}
						if (!string.IsNullOrEmpty(filter.TimeFrom) && !string.IsNullOrEmpty(filter.TimeTo))
						{
							filter.TimeFrom += ":00";
							filter.TimeTo += ":00";
							res = res.Where(r => r.Checktime.HasValue &&
							r.Checktime.Value.TimeOfDay >= TimeSpan.Parse(filter.TimeFrom) &&
							r.Checktime.Value.TimeOfDay <= TimeSpan.Parse(filter.TimeTo));
						}
						if (filter.BranchId != null && filter.BranchId > 0)
						{
							res = res.Where(c => c.BranchId != null && c.BranchId.Equals(filter.BranchId));
						}
						if (filter.Location != null && filter.Location > 0)
						{
							res = res.Where(c => c.Location != null && c.Location.Equals(filter.Location));
						}

						foreach (var item in res)
						{
							var newRecord = new HrAttendanceUnknownFilterDto
							{

								Id = item.Id,
								EmpCode = item.EmpCode,
								EmpName = session.Language == 1 ? item.EmpName : item.EmpName2,
								DayName = session.Language == 1 ? item.DayName : item.DayName2,
								Checktime = item.Checktime,
								TimeText = item.Checktime.Value.TimeOfDay.ToString() ?? "00:00",
								CheckTypeName = (item.Checktype == 1) ? localization.GetHrResource("EntryTransaction") : localization.GetHrResource("ExitTransaction"),
							};
							resultList.Add(newRecord);
						}
						if (resultList.Any())
							return await Result<List<HrAttendanceUnknownFilterDto>>.SuccessAsync(resultList, "");
						return await Result<List<HrAttendanceUnknownFilterDto>>.SuccessAsync(resultList, localization.GetResource1("NosearchResult"));
					}
					return await Result<List<HrAttendanceUnknownFilterDto>>.SuccessAsync(resultList, localization.GetResource1("NosearchResult"));
				}
				return await Result<List<HrAttendanceUnknownFilterDto>>.FailAsync("errrorrrs");
			}
			catch (Exception ex)
			{
				return await Result<List<HrAttendanceUnknownFilterDto>>.FailAsync(ex.Message);
			}
		}

		//public async Task<IResult<string>> UpdateCheckInOut(HrUpdateCheckINout entity, CancellationToken cancellationToken = default)
		//{
		//    try
		//    {
		//        var getAllCheckInOut = await hrRepositoryManager.HrCheckInOutRepository.GetAll(x => x.SendActualAttendance == false);
		//        if (getAllCheckInOut == null)
		//        {
		//            return await Result<string>.SuccessAsync(localization.GetResource1("NotAbleShowResults"));
		//        }
		//        var res = getAllCheckInOut.AsQueryable();

		//        if (!string.IsNullOrEmpty(entity.EmpCode))
		//        {
		//            var getEmp = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.Isdel == false && x.IsDeleted == false && x.EmpId == entity.EmpCode);

		//            if (getEmp == null)
		//            {
		//                return await Result<string>.SuccessAsync(localization.GetResource1("EmployeeNotFound"));

		//            }
		//            res = res.Where(x => x.EmpId == getEmp.Id);
		//        }

		//        if (!string.IsNullOrEmpty(entity.FromDate) && !string.IsNullOrEmpty(entity.ToDate))
		//        {
		//            res = res.Where(r => r.Checktime != null &&
		//                    r.Checktime >= DateHelper.StringToDate(entity.FromDate) &&
		//                    r.Checktime <= DateHelper.StringToDate(entity.ToDate));
		//        }
		//        // اذا لم توجد بيانات
		//        if (res.Count() <= 0)
		//        {
		//            return await Result<string>.SuccessAsync(localization.GetResource1("NotAbleShowResults"));
		//        }

		//        for (int i = 0; i < res.Count() - 1; i++)
		//        {
		//            foreach (var item in entity.AttendanceData)
		//            {
		//                var date1 = item.CHECKTIMEIN2;
		//            }
		//        }
		//        return await Result<string>.SuccessAsync(localization.GetResource1("ActionSuccess"));
		//    }
		//    catch (Exception exp)
		//    {

		//        return await Result<string>.FailAsync($"EXP in UpdateCheckInOut at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
		//    }
		//}

	}
}
