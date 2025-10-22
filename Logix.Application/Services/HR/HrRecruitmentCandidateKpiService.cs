using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.DTOs.WF;
using Logix.Application.Helpers;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;
using Logix.Domain.WF;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

namespace Logix.Application.Services.HR
{
    public class HrRecruitmentCandidateKpiService : GenericQueryService<HrRecruitmentCandidateKpi, HrRecruitmentCandidateKpiDto, HrRecruitmentCandidateKpiVw>, IHrRecruitmentCandidateKpiService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        private readonly IWFRepositoryManager wFRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly ISysConfigurationAppHelper sysConfigurationAppHelper;
        private readonly IMainRepositoryManager mainRepositoryManager;

        public HrRecruitmentCandidateKpiService(IQueryRepository<HrRecruitmentCandidateKpi> queryRepository, IWFRepositoryManager wFRepositoryManager, IMapper mapper, ICurrentData session, ISysConfigurationAppHelper sysConfigurationAppHelper, IMainRepositoryManager mainRepositoryManager, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this.wFRepositoryManager = wFRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.sysConfigurationAppHelper = sysConfigurationAppHelper;
            this.mainRepositoryManager = mainRepositoryManager;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
        }

        public Task<IResult<HrRecruitmentCandidateKpiDto>> Add(HrRecruitmentCandidateKpiDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IResult<string>> AddNewCandidateKpi(HrCandidateKPIDtoForOperations entity, CancellationToken cancellationToken = default)
        {
            string ReadBranchFromEmployee;
            int? branchId;
            string NumberingByYear;
            long? Application_Code;
            int? AppTypeId = 0;
            if (entity == null) return await Result<string>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
            try
            {
                if (entity.CandidateId <= 0) return await Result<string>.FailAsync("the Candidate Id is Required");
                var checkCandidateExist = await hrRepositoryManager.HrRecruitmentCandidateRepository.GetOne(c => c.Id == entity.CandidateId);
                if (checkCandidateExist == null)
                {
                    return await Result<string>.FailAsync("Candidate Not Found");
                }
                entity.StepId = 1;
                entity.StatusId = 2;
                entity.CreatedBy = session.UserId;
                entity.CreatedOn = DateTime.Now;
                ReadBranchFromEmployee = await sysConfigurationAppHelper.GetValue(155, 1);
                if (ReadBranchFromEmployee == "1")
                {
                    var checkEmployees = await hrRepositoryManager.HrEmployeeRepository.GetOne(e => e.Id == entity.ApplicantsId);
                    if (checkEmployees == null)
                    {
                        return await Result<string>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                    }
                    branchId = checkEmployees.BranchId;
                    entity.ApplicantsId = checkEmployees.Id;


                }
                NumberingByYear = await sysConfigurationAppHelper.GetValue(166, 1);
                var getWFApplications = await wFRepositoryManager.WfApplicationRepository.GetAll();
                if (NumberingByYear == "1")
                {
                    var getWFApplicationsAfterCondition = getWFApplications.Where(e => DateHelper.StringToDate(e.ApplicationDate).Year == DateHelper.StringToDate(entity.ApplicationDate).Year);
                    Application_Code = (getWFApplicationsAfterCondition.Max(y => y.ApplicationCode) ?? 0) + 1;
                }
                else
                {
                    //var getWFApplicationsAfterCondition = getWFApplications.Where(e => DateHelper.StringToDate1(e.ApplicationDate).Year == DateHelper.StringToDate1(e.ApplicationDate).Year);
                    Application_Code = (getWFApplications.Max(y => y.ApplicationCode) ?? 0) + 1;
                }
                if (entity.ApplicationsTypeId != null)
                {
                    AppTypeId = entity.ApplicationsTypeId;
                }

                if (entity.StepId == 1)
                {
                    var getFromSteps = await wFRepositoryManager.WfStepRepository.GetAll(x => x.Id, x => x.IsDeleted == false && x.StepTypeId == 1);
                    var getFromStepsTransactions = await wFRepositoryManager.WfStepsTransactionRepository.GetAll(x => x.IsDeleted == false && x.SortNo == 1 && x.AppTypeId == AppTypeId);
                    // var finalResult = getFromStepsTransactions.Where(x=> getFromSteps.ToString().Contains(x.FromStepId));
                    if (getFromSteps.Any())
                    {
                        var finalResult = getFromStepsTransactions.Where(x => x.FromStepId != null && getFromSteps.Any(s => s.ToString().Contains(x.FromStepId)));
                        entity.StepId = Convert.ToInt32(finalResult.Select(x => x.FromStepId).FirstOrDefault());
                    }
                }
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                if (string.IsNullOrEmpty(entity.ApplicationDate))
                {
                    entity.ApplicationDate = DateTime.Now.ToString();
                }
                var WfApplicationsItem = new WfApplication
                {
                    ApplicationDate = entity.ApplicationDate,
                    ApplicationCode = Application_Code,
                    ApplicationsTypeId = AppTypeId,
                    ApplicantsId = entity.ApplicantsId,
                    CreatedBy = session.UserId,
                    CreatedOn = DateTime.Now,
                    StatusId = entity.StatusId,
                    StepId = entity.StepId,
                    Note = ""
                };
                var newWfApplicationsItem = await wFRepositoryManager.WfApplicationRepository.AddAndReturn(WfApplicationsItem);

                await wFRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                // WfApplication  هنا تم الانتهاء من الاضافة الى جدول 
                ////////////////////////////////////////////////////////
                var GetStep_ID = await wFRepositoryManager.WfStepsTransactionRepository.GetOne(x => x.ToStepId, x => x.IsDeleted == false && x.SortNo == 1 && x.AppTypeId == AppTypeId);
                if (GetStep_ID == null)
                {
                    GetStep_ID = "0";
                }
                ////////////////////////////////////////////////////////

                // ApplicationsStatus  هنا تبدأ الاضافة الى جدول 
                var appStatus = new WfApplicationsStatusDto
                {
                    ApplicantsId = newWfApplicationsItem.ApplicantsId,
                    ApplicationsId = WfApplicationsItem.Id,
                    CreatedBy = session.UserId,
                    CreatedOn = DateTime.Now,
                    NewStatusId = 1,
                    Note = "",
                    StepId = Convert.ToInt32(GetStep_ID),
                };
                var StatusId = await wFRepositoryManager.WfApplicationsStatusRepository.ProcessApplicationStatusAsync((long)appStatus.ApplicationsId, 0, appStatus.NewStatusId, appStatus.StepId, appStatus.Note, appStatus.DesNo, appStatus.CouncilDate);

                //var ExecuteInsertProcedure = await wFRepositoryManager.WfApplicationsStatusRepository.InsertApplicationsStatus(appStatus);

                ///////////////////////////////////////////////////////////////////////////
                var newRecruitmentCandidateKPI = new HrRecruitmentCandidateKpi
                {
                    CandidateId = entity.CandidateId,
                    AppId = WfApplicationsItem.Id,
                    KpiTemId = entity.KpiTemId,
                    EvaDate = entity.EvaDate,
                    StatusId = 1,
                    CreatedBy = session.UserId,
                    CreatedOn = DateTime.Now,
                };
                var addNewwCandidateKpi = await hrRepositoryManager.HrRecruitmentCandidateKpiRepository.AddAndReturn(newRecruitmentCandidateKPI);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                if (entity.candidateKpiDDtos != null && entity.candidateKpiDDtos.Any())
                {
                    foreach (var item in entity.candidateKpiDDtos)
                    {
                        var newRecruitmentCandidateKPID = new HrRecruitmentCandidateKpiD
                        {
                            Degree = item.Degree,
                            KpiTemComId = item.KpiTemComId, // رقم بند التقييم 
                            KpiId = addNewwCandidateKpi.Id,
                            CreatedBy = session.UserId,
                            CreatedOn = DateTime.Now,
                            IsDeleted = false,


                        };
                        var addNewwCandidateKpid = await hrRepositoryManager.HrRecruitmentCandidateKpiDRepository.AddAndReturn(newRecruitmentCandidateKPID);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }
                }
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<string>.SuccessAsync(newWfApplicationsItem.ApplicationCode.ToString() ?? "", localization.GetResource1("AddSuccess"));


            }
            catch (Exception)
            {

                return await Result<string>.FailAsync(localization.GetResource1("AddError"));
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrRecruitmentCandidateKpiRepository.GetById(Id);
                if (item == null) return Result<HrRecruitmentCandidateKpiDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;

                hrRepositoryManager.HrRecruitmentCandidateKpiRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrRecruitmentCandidateKpiDto>.SuccessAsync(_mapper.Map<HrRecruitmentCandidateKpiDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrRecruitmentCandidateKpiDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrRecruitmentCandidateKpiRepository.GetById(Id);
                if (item == null) return Result<HrRecruitmentCandidateKpiDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;

                hrRepositoryManager.HrRecruitmentCandidateKpiRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrRecruitmentCandidateKpiDto>.SuccessAsync(_mapper.Map<HrRecruitmentCandidateKpiDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrRecruitmentCandidateKpiDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<IEnumerable<HrRecruitmentCandidateKpiFilterDto>>> SearchHrRecruitmentCandidateKp(HrRecruitmentCandidateKpiFilterDto filter, CancellationToken cancellationToken = default)
        {
            decimal? Eva_Degree = 0;
            decimal? Eva_Value = 0;
            List<HrRecruitmentCandidateKpiFilterDto> result = new List<HrRecruitmentCandidateKpiFilterDto>();
            try
            {
                var getAllKpiData = await hrRepositoryManager.HrRecruitmentCandidateKpiRepository.GetAllVW(x => x.IsDeleted == false);
                if (getAllKpiData == null) return await Result<IEnumerable<HrRecruitmentCandidateKpiFilterDto>>.SuccessAsync(localization.GetResource1("NosearchResult"));


                foreach (var item in getAllKpiData)
                {

                    var getAllKpiDetailsData = await hrRepositoryManager.HrRecruitmentCandidateKpiDRepository.GetAllVW(x => x.IsDeleted == false && x.KpiId == item.Id);
                    Eva_Degree = getAllKpiDetailsData.Sum(x => x.Degree);
                    Eva_Value = getAllKpiDetailsData.Sum(x => x.Degree * x.Weight);
                    var newRecord = new HrRecruitmentCandidateKpiFilterDto
                    {
                        Id = item.Id,
                        EvaDegree = Eva_Degree,
                        CandidateId = item.Id,
                        CandidateName = item.CandidateName,
                        EvaDate = item.EvaDate,
                        EvaValue = Eva_Value,
                        KpiTemId = item.KpiTemId,
                        TemName = item.TemName,
                        VacancyName = item.VacancyName
                    };
                    result.Add(newRecord);

                }

                if (result.Any())
                {

                    if (filter.CandidateId > 0)
                    {
                        result = result.Where(x => x.CandidateId != null && x.CandidateId == filter.CandidateId).ToList();
                    }
                    if (filter.KpiTemId > 0)
                    {
                        result = result.Where(x => x.KpiTemId != null && filter.KpiTemId == x.KpiTemId).ToList();
                    }
                    if (!string.IsNullOrEmpty(filter.CandidateName))
                    {
                        result = result.Where(x => x.CandidateName != null && x.CandidateName.Contains(filter.CandidateName)).ToList();
                    }
                    if (!string.IsNullOrEmpty(filter.EvaDate))
                    {
                        result = result.Where(x => x.EvaDate != null && DateHelper.StringToDate(x.EvaDate) >= DateHelper.StringToDate(filter.EvaDate)).ToList();
                    }
                    if (!string.IsNullOrEmpty(filter.EvaDateTo))
                    {
                        result = result.Where(x => x.EvaDate != null && DateHelper.StringToDate(x.EvaDate) <= DateHelper.StringToDate(filter.EvaDateTo)).ToList();
                    }


                }

                return await Result<IEnumerable<HrRecruitmentCandidateKpiFilterDto>>.SuccessAsync(result);
            }
            catch (Exception exp)
            {

                return await Result<IEnumerable<HrRecruitmentCandidateKpiFilterDto>>.FailAsync($"EXP in GetAllHRRecruitmentVacancy at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public Task<IResult<string>> SearchHrRecruitmentCandidateKp(HrCandidateKPIDtoForOperations filter, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IResult<HrRecruitmentCandidateKpiEditDto>> Update(HrRecruitmentCandidateKpiEditDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IResult<string>> UpdateCandidateKpi(HrCandidateKPIDtoForOperations entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<string>.FailAsync($"Error in Update of: {this.GetType()}, the passed entity is NULL !!!.");
            try
            {
                if (entity.CandidateId <= 0) return await Result<string>.FailAsync("the Candidate Id is Required");
                var checkCandidateExist = await hrRepositoryManager.HrRecruitmentCandidateRepository.GetOne(c => c.Id == entity.CandidateId);
                if (checkCandidateExist == null)
                {
                    return await Result<string>.FailAsync("Candidate Not Found");
                }
                var checkEmployees = await hrRepositoryManager.HrEmployeeRepository.GetOne(e => e.EmpId == entity.ApplicantsId.ToString());
                if (checkEmployees == null)
                {
                    return await Result<string>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                }
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                if (string.IsNullOrEmpty(entity.ApplicationDate))
                {
                    entity.ApplicationDate = DateTime.Now.ToString();
                }
                ///////////////////////////////////////////////////////////////////////////
                var getKpiItem = await hrRepositoryManager.HrRecruitmentCandidateKpiRepository.GetById(Convert.ToInt64( entity.KpiId));
                if (getKpiItem == null) return await Result<string>.FailAsync(localization.GetResource1("ErrorOccurredDuring"));

                getKpiItem.CandidateId = entity.CandidateId;
                getKpiItem.AppId = 0;
                getKpiItem.KpiTemId = entity.KpiTemId;
                getKpiItem.EvaDate = entity.EvaDate;
                getKpiItem.StatusId = 1;
                getKpiItem.ModifiedBy = session.UserId;
                getKpiItem.ModifiedOn = DateTime.Now;
                 hrRepositoryManager.HrRecruitmentCandidateKpiRepository.Update(getKpiItem);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                if (entity.candidateKpiDDtos != null && entity.candidateKpiDDtos.Any())
                {
                    foreach (var item in entity.candidateKpiDDtos)
                    {
                        var getItem = await hrRepositoryManager.HrRecruitmentCandidateKpiDRepository.GetById(item.Id);

                        if (getItem == null) return await Result<string>.FailAsync(localization.GetResource1("ErrorOccurredDuring"));

                        getItem.Degree = item.Degree;
                        getItem.KpiId = item.KpiId;
                        getItem.ModifiedBy = session.UserId;
                        getItem.ModifiedOn = DateTime.Now;
                        getItem.KpiTemComId = item.KpiTemComId;
                        hrRepositoryManager.HrRecruitmentCandidateKpiDRepository.Update(getItem);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }
                }
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<string>.SuccessAsync(localization.GetResource1("UpdateSuccess"));


            }
            catch (Exception)
            {

                return await Result<string>.FailAsync(localization.GetResource1("ErrorOccurredDuring"));
            }
        }
    }
}