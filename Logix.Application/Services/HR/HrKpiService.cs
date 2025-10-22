using AutoMapper;
using iText.Commons.Bouncycastle.Asn1.X509;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Helpers;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;
using static QuestPDF.Helpers.Colors;

namespace Logix.Application.Services.HR
{
    public class HrKpiService : GenericQueryService<HrKpi, HrKpiDto, HrKpiVw>, IHrKpiService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly ILocalizationService localization;
        private readonly IWorkflowHelper workflowHelper;
        public HrKpiService(IQueryRepository<HrKpi> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, ILocalizationService localization, IHrRepositoryManager hrRepositoryManager, IWorkflowHelper workflowHelper) : base(queryRepository, mapper)
        {
            this.hrRepositoryManager = hrRepositoryManager;
            this.mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.localization = localization;
            this.workflowHelper = workflowHelper;
        }

        public async Task<IResult<HrKpiDto>> Add(HrKpiDto entity, CancellationToken cancellationToken = default)
        {

            if (entity == null) return await Result<HrKpiDto>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));
            if (string.IsNullOrEmpty(entity.EmpCode)) return await Result<HrKpiDto>.FailAsync(localization.GetResource1("EmployeeIsNumber"));

            try
            {
                entity.PerformanceId ??= 0;
                decimal? FinalRatingKPI = 0;
                decimal? FinalRatingCompetences = 0;
                decimal? TotalScore2 = 0;
                decimal? TotalScore1 = 0;
                decimal? DegreeKPI = 0;
                decimal? DegreeCompetence = 0;
                decimal CompetencesWeight = 0;
                decimal KpiWeight = 0;
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);


                // check if Emp Is Exist
                var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (checkEmpExist == null) return await Result<HrKpiDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                if (checkEmpExist.StatusId == 2) return await Result<HrKpiDto>.FailAsync(localization.GetResource1("EmpNotActive"));

                //    هل تم عمل تقييم للموظف من قبل لنفس فترة التقييم 

                if (entity.PerformanceId > 0)
                {
                    var checkIFKPIPerformanceExists = await hrRepositoryManager.HrKpiRepository.GetAllVw(x => x.IsDeleted == false && x.EmpId == checkEmpExist.Id && x.PerformanceId == entity.PerformanceId);

                    if (checkIFKPIPerformanceExists.Count() > 0) return await Result<HrKpiDto>.FailAsync("تم عمل تقييم للموظف من قبل لنفس فترة التقييم ");
                }

                if (entity.Compatence.Count == 0 && entity.RatingKPI.Count == 0) return await Result<HrKpiDto>.FailAsync("لن تتمكن من ارسال التقييم لعدم وجود كفاءات او مؤشرات اداء مدخلة على النموذج المحدد");

                /////////////////////////////////////////////////////
                var GetWeights = await hrRepositoryManager.HrKpiTemplateRepository.GetOne(x => x.IsDeleted == false && x.Id == entity.KpiTemId);
                if (GetWeights == null)
                {
                    return await Result<HrKpiDto>.FailAsync("  نموذج التقييم غير موجود");

                }
                CompetencesWeight = GetWeights.CompetencesWeight ?? 0;
                KpiWeight = GetWeights.KpiWeight ?? 0;
                if (entity.Compatence != null)
                {
                    if (entity.Compatence.Count() > 0)
                    {
                        FinalRatingCompetences = entity.Compatence.Sum(x => x.Score);
                        TotalScore1 = entity.Compatence.Sum(x => x.Weight);
                    }

                }

                if (entity.RatingKPI != null)
                {
                    if (entity.RatingKPI.Count > 0)
                    {
                        foreach (var RatingKPIItem in entity.RatingKPI)
                        {
                            if (RatingKPIItem.MethodId == 1)
                            {
                                if (RatingKPIItem.Target > 0)
                                {
                                    RatingKPIItem.DueDegree = Math.Round((decimal)(RatingKPIItem.ActualTarget / RatingKPIItem.Target * RatingKPIItem.Score), 2);
                                }
                                else
                                {
                                    RatingKPIItem.DueDegree = 0;
                                }
                            }
                            else if (RatingKPIItem.MethodId == 2)
                            {
                                decimal? Total = RatingKPIItem.Score - (RatingKPIItem.ActualTarget * RatingKPIItem.UnitRate);
                                if (Total > 0)
                                {
                                    RatingKPIItem.DueDegree = Math.Round((decimal)Total, 2);
                                }
                                else
                                {
                                    RatingKPIItem.DueDegree = 0;
                                }
                            }
                            else
                            {
                                RatingKPIItem.DueDegree = 0;
                            }
                            FinalRatingKPI += RatingKPIItem.DueDegree;

                        }
                        TotalScore2 = entity.RatingKPI.Sum(x => x.Score);

                    }
                }

                if (TotalScore1 > 0)
                {
                    DegreeCompetence = Math.Round((decimal)(FinalRatingCompetences / TotalScore1 * CompetencesWeight), 2);

                }
                if (TotalScore2 > 0)
                {
                    DegreeKPI = Math.Round((decimal)(FinalRatingKPI / TotalScore2 * KpiWeight), 2);

                }

                long? appId = 0;

                entity.AppTypeId ??= 0;


                //  ارسال الى سير العمل

                var GetApp_ID = await workflowHelper.Send(checkEmpExist.Id, 272, entity.AppTypeId);
                appId = GetApp_ID;
                var item = new HrKpi();
                item.EmpId = checkEmpExist.Id;
                item.CreatedBy = session.UserId;
                item.CreatedOn = DateTime.Now;
                item.IsDeleted = false;
                item.FinalRatingCompetences = FinalRatingCompetences;
                item.FinalRatingKpi = FinalRatingKPI;
                item.FinalRating = DegreeKPI + DegreeCompetence;
                item.EvaDate = entity.EvaDate;
                item.StatusId = 1;
                item.TypeId = entity.TypeId;
                item.AppId = appId;
                item.PerformanceId = entity.PerformanceId;
                item.Achievements = entity.Achievements;
                item.WeaknessesPoints = entity.WeaknessesPoints ?? "";
                item.StrengthsPoints = entity.StrengthsPoints ?? "";
                item.Note = entity.Note ?? "";
                item.Recommendations = entity.Recommendations ?? "";
                item.SuggestedTraining = entity.SuggestedTraining ?? "";
                item.StartDate = entity.StartDate;
                item.EndDate = entity.EndDate;
                item.KpiTemId = entity.KpiTemId;
                item.KpiTemId = entity.KpiTemId;
                item.ProbationResult = entity.ProbationResult;
                var addedItem = await hrRepositoryManager.HrKpiRepository.AddAndReturn(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                //    اضافة الكفاءات
                if (entity.Compatence != null)
                {
                    if (entity.Compatence.Count() > 0)
                    {
                        foreach (var CompetenceItem in entity.Compatence)
                        {
                            var newItem = new HrKpiDetaile()
                            {
                                Degree = CompetenceItem.Score,
                                KpiTemComId = CompetenceItem.Id.ToString(),
                                TemplateId = CompetenceItem.TemplateId,
                                Weight = CompetenceItem.Weight,
                                CompetencesId = CompetenceItem.CompetencesId,
                                KpiId = addedItem.Id,
                                ActualTarget = 0
                            };
                            await hrRepositoryManager.HrKpiDetaileRepository.Add(newItem);
                            await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                        }
                    }

                }




                //   اضافة مؤشرات الاداء
                if (entity.RatingKPI != null)
                {
                    if (entity.RatingKPI.Count() > 0)
                    {
                        foreach (var KpiItem in entity.RatingKPI)
                        {
                            var newItem = new HrKpiDetaile()
                            {
                                Degree = KpiItem.DueDegree,
                                KpiTemComId = KpiItem.Id.ToString(),
                                TemplateId = KpiItem.TemplateId,
                                Weight = KpiItem.Weight,
                                CompetencesId = KpiItem.CompetencesId,
                                KpiId = addedItem.Id,
                                ActualTarget = KpiItem.ActualTarget,
                                Target = KpiItem.Target,

                            };
                            await hrRepositoryManager.HrKpiDetaileRepository.Add(newItem);
                            await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                        }
                    }
                }

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<HrKpiDto>.SuccessAsync(_mapper.Map<HrKpiDto>(item), localization.GetMessagesResource("success"));
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Add at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrKpiDto>.FailAsync($"EXP in Add at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<List<object>>> GetEmployeeKpi(HRKpiQueryFilterDto filter, CancellationToken cancellationToken = default)

        {
            try
            {
                var result = await hrRepositoryManager.HrKpiRepository.GetKpiEmployeeDetailsAsync(filter);
                return await Result<List<object>>.SuccessAsync(result.ToList(), "", 200);
                //return (IResult<List<object>>)result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrKpiRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrKpiDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;

                hrRepositoryManager.HrKpiRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrKpiDto>.SuccessAsync(_mapper.Map<HrKpiDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrKpiDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrKpiRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrKpiDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;

                hrRepositoryManager.HrKpiRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrKpiDto>.SuccessAsync(_mapper.Map<HrKpiDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrKpiDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

		public async Task<IResult<List<HRRepKPIFilterDto>>> Search(HRRepKPIFilterDto filter, CancellationToken cancellationToken = default)
		{
			try
			{

				var BranchesList = session.Branches.Split(',');
				List<HRRepKPIFilterDto> resultList = new List<HRRepKPIFilterDto>();
				var items = await hrRepositoryManager.HrKpiRepository.GetAllVw(e => e.IsDeleted == false && BranchesList.Contains(e.BranchId.ToString()) &&
				(string.IsNullOrEmpty(filter.EmpCode) || filter.EmpCode == e.EmpCode) &&
				(string.IsNullOrEmpty(filter.Achievements) || e.Achievements.Contains(filter.Achievements)) &&
				(string.IsNullOrEmpty(filter.Recommendations) || e.Recommendations.Contains(filter.Recommendations)) &&
				(string.IsNullOrEmpty(filter.SuggestedTraining) || e.SuggestedTraining.Contains(filter.SuggestedTraining)) &&
				(string.IsNullOrEmpty(filter.StrengthsPoints) || e.StrengthsPoints.Contains(filter.StrengthsPoints)) &&
				(string.IsNullOrEmpty(filter.WeaknessesPoints) || e.WeaknessesPoints.Contains(filter.WeaknessesPoints)) &&
				(string.IsNullOrEmpty(filter.EmpName) || e.EmpName.ToLower().Contains(filter.EmpName) || e.EmpName2.ToLower().Contains(filter.EmpName)) &&
				(filter.LocationId == 0 || filter.LocationId == null || filter.LocationId == e.Location) &&
				(filter.Status == 0 || filter.Status == null || filter.Status == e.StatusId) &&
				(filter.Type == 0 || filter.Type == null || filter.Type == e.TypeId) &&
				(filter.DepartmentId == 0 || filter.DepartmentId == null || filter.DepartmentId == e.DeptId)
				);
				if (items != null)
				{
					if (items.Count() > 0)
					{

						var res = items.AsQueryable();

						if (filter.BranchId != null && filter.BranchId > 0)
						{
							res = res.Where(c => c.BranchId != null && c.BranchId == filter.BranchId);
						}

						if (filter.Month != null && filter.Month > 0)
						{
							res = res.Where(c => c.EvaDate != null && Convert.ToInt32(c.EvaDate.Substring(5, 2)) == filter.Month);
						}
						if (res.Any())
						{
							var getKPIDetailes = await hrRepositoryManager.HrKpiDetaileRepository.GetAllVw(x => x.IsDeleted == false);
							foreach (var item in res)
							{
								var getKpiForItem = getKPIDetailes.Where(x => x.KpiId == item.Id);
								var sumDegrees = getKpiForItem.Sum(d => d.Degree);
								var sumScores = getKpiForItem.Sum(d => d.Score);
								decimal? TotalDegree = 0;
								if (sumScores != 0)
								{
									TotalDegree = sumDegrees / sumScores * 100;
								}

								var newItem = new HRRepKPIFilterDto
								{
									EmpCode = item.EmpCode,
									EmpName = item.EmpName,
									DegreeTotal = TotalDegree,
									TemName = item.TemName,
									Achievements = item.Achievements,
									EvaDate = item.EvaDate,
									Recommendations = item.Recommendations,
									SuggestedTraining = item.SuggestedTraining,
									StrengthsPoints = item.StrengthsPoints,
									WeaknessesPoints = item.WeaknessesPoints,
								};
								resultList.Add(newItem);
							}
							if (resultList.Count > 0) return await Result<List<HRRepKPIFilterDto>>.SuccessAsync(resultList);
							return await Result<List<HRRepKPIFilterDto>>.SuccessAsync(resultList, localization.GetResource1("NosearchResult"));

						}
						return await Result<List<HRRepKPIFilterDto>>.SuccessAsync(resultList, localization.GetResource1("NosearchResult"));

					}
					return await Result<List<HRRepKPIFilterDto>>.SuccessAsync(resultList, localization.GetResource1("NosearchResult"));
				}
				return await Result<List<HRRepKPIFilterDto>>.FailAsync("errorrr");
			}
			catch (Exception ex)
			{
				return await Result<List<HRRepKPIFilterDto>>.FailAsync(ex.Message);
			}
		}

		public async Task<IResult<HrKpiEditDto>> Update(HrKpiEditDto entity, CancellationToken cancellationToken = default)
        {

            if (entity == null) return await Result<HrKpiEditDto>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));
            if (string.IsNullOrEmpty(entity.EmpCode)) return await Result<HrKpiEditDto>.FailAsync(localization.GetResource1("EmployeeIsNumber"));

            try
            {
                decimal? FinalRatingKPI = 0;
                decimal? FinalRatingCompetences = 0;
                decimal? TotalScore2 = 0;
                decimal? TotalScore1 = 0;
                decimal? DegreeKPI = 0;
                decimal? DegreeCompetence = 0;
                decimal CompetencesWeight = 0;
                decimal KpiWeight = 0;
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                var item = await hrRepositoryManager.HrKpiRepository.GetById(entity.Id);
                if (item == null) return await Result<HrKpiEditDto>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));

                // check if Emp Is Exist
                var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (checkEmpExist == null) return await Result<HrKpiEditDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                if (checkEmpExist.StatusId == 2) return await Result<HrKpiEditDto>.FailAsync(localization.GetResource1("EmpNotActive"));
                var GetWeights = await hrRepositoryManager.HrKpiRepository.GetOneVw(x => x.IsDeleted == false && x.Id == entity.Id);
                if (GetWeights != null)
                {
                    CompetencesWeight = GetWeights.CompetencesWeight ?? 0;
                    KpiWeight = GetWeights.KpiWeight ?? 0;
                }
                if (entity.Compatence != null)
                {
                    if (entity.Compatence.Count() > 0)
                    {
                        FinalRatingCompetences = entity.Compatence.Sum(x => x.Degree);
                        TotalScore1 = entity.Compatence.Sum(x => x.Weight);
                    }

                }

                if (entity.RatingKPI != null)
                {
                    if (entity.RatingKPI.Count > 0)
                    {
                        foreach (var RatingKPIItem in entity.RatingKPI)
                        {
                            if (RatingKPIItem.MethodId == 1)
                            {
                                if (RatingKPIItem.Target > 0)
                                {
                                    RatingKPIItem.DueDegree = Math.Round((decimal)(RatingKPIItem.ActualTarget / RatingKPIItem.Target * RatingKPIItem.Score), 2);
                                }
                                else
                                {
                                    RatingKPIItem.DueDegree = 0;
                                }
                            }
                            else if (RatingKPIItem.MethodId == 2)
                            {
                                decimal? Total = RatingKPIItem.Score - (RatingKPIItem.ActualTarget * RatingKPIItem.UnitRate);
                                if (Total > 0)
                                {
                                    RatingKPIItem.DueDegree = Math.Round((decimal)Total, 2);
                                }
                                else
                                {
                                    RatingKPIItem.DueDegree = 0;
                                }
                            }
                            else
                            {
                                RatingKPIItem.DueDegree = 0;
                            }
                            FinalRatingKPI += RatingKPIItem.DueDegree;

                        }
                        TotalScore2 = entity.RatingKPI.Sum(x => x.Score);

                    }
                }

                if (TotalScore1 > 0)
                {
                    DegreeCompetence = Math.Round((decimal)(FinalRatingCompetences / TotalScore1 * CompetencesWeight), 2);

                }
                if (TotalScore2 > 0)
                {
                    DegreeKPI = Math.Round((decimal)(FinalRatingKPI / TotalScore2 * KpiWeight), 2);

                }


                item.EmpId = checkEmpExist.Id;
                item.CreatedBy = session.UserId;
                item.CreatedOn = DateTime.Now;
                item.IsDeleted = false;
                item.FinalRatingCompetences = FinalRatingCompetences;
                item.FinalRatingKpi = FinalRatingKPI;
                item.FinalRating = DegreeKPI + DegreeCompetence;
                item.EvaDate = entity.EvaDate;
                item.StatusId = 1;
                item.TypeId = entity.TypeId;
                item.PerformanceId = entity.PerformanceId;
                item.Achievements = entity.Achievements;
                item.WeaknessesPoints = entity.WeaknessesPoints ?? "";
                item.StrengthsPoints = entity.StrengthsPoints ?? "";
                item.Note = entity.Note ?? "";
                item.Recommendations = entity.Recommendations ?? "";
                item.SuggestedTraining = entity.SuggestedTraining ?? "";
                item.StartDate = entity.StartDate;
                item.EndDate = entity.EndDate;
                item.KpiTemId = entity.KpiTemId;
                item.KpiTemId = entity.KpiTemId;
                item.ProbationResult = entity.ProbationResult;
                hrRepositoryManager.HrKpiRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                //    تعديل الكفاءات
                if (entity.Compatence != null)
                {
                    if (entity.Compatence.Count() > 0)
                    {
                        foreach (var CompetenceItem in entity.Compatence)
                        {
                            var GetCompetenceItem = await hrRepositoryManager.HrKpiDetaileRepository.GetById(CompetenceItem.Id);
                            if (GetCompetenceItem != null)
                            {
                                GetCompetenceItem.ActualTarget = 0;
                                GetCompetenceItem.Degree = CompetenceItem.Degree;
                                hrRepositoryManager.HrKpiDetaileRepository.Update(GetCompetenceItem);
                                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                            }
                        }
                    }

                }




                //   تعديل مؤشرات الاداء
                if (entity.RatingKPI != null)
                {
                    if (entity.RatingKPI.Count() > 0)
                    {
                        foreach (var KpiItem in entity.RatingKPI)
                        {
                            var GetKpiItem = await hrRepositoryManager.HrKpiDetaileRepository.GetById(KpiItem.Id);
                            if (GetKpiItem != null)
                            {
                                GetKpiItem.ActualTarget = KpiItem.ActualTarget;
                                GetKpiItem.Target = KpiItem.Target;
                                GetKpiItem.Degree = KpiItem.DueDegree;
                                hrRepositoryManager.HrKpiDetaileRepository.Update(GetKpiItem);
                                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                            }
                        }
                    }
                }

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<HrKpiEditDto>.SuccessAsync(_mapper.Map<HrKpiEditDto>(item), localization.GetMessagesResource("success"));
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrKpiEditDto>.FailAsync($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<bool>> UpdateKpiStatus(long Id, int StatusId, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrKpiRepository.GetById(Id);
                if (item == null) return await Result<bool>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));
                item.StatusId = StatusId;
                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                hrRepositoryManager.HrKpiRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                return await Result<bool>.SuccessAsync(true, localization.GetMessagesResource("success"));

            }
            catch (Exception exp)
            {

                Console.WriteLine($"EXP in UpdateKpiStatus at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<bool>.FailAsync($"EXP in UpdateKpiStatus at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
    }
}