using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;
using Logix.Domain.Main;
using System.Globalization;

namespace Logix.Application.Services.HR
{
    public class HrEmpGoalIndicatorService : GenericQueryService<HrEmpGoalIndicator, HrEmpGoalIndicatorDto, HrEmpGoalIndicatorsVw>, IHrEmpGoalIndicatorService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper mapper;
        private readonly ICurrentData session;


        public HrEmpGoalIndicatorService(IQueryRepository<HrEmpGoalIndicator> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this.mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
        }

        public Task<IResult<HrEmpGoalIndicatorDto>> Add(HrEmpGoalIndicatorDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrEmpGoalIndicatorRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrEmpGoalIndicatorDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;

                hrRepositoryManager.HrEmpGoalIndicatorRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                var getIndicatorEmployees = await hrRepositoryManager.HrEmpGoalIndicatorsEmployeeRepository.GetAll(x => x.IsDeleted == false && x.GoalIndicatorsId == Id);
                if (getIndicatorEmployees.Count() > 0)
                {
                    foreach (var Employeeitem in getIndicatorEmployees)
                    {
                        Employeeitem.IsDeleted = true;
                        Employeeitem.ModifiedBy = session.UserId;
                        Employeeitem.ModifiedOn = DateTime.Now;
                        hrRepositoryManager.HrEmpGoalIndicatorsEmployeeRepository.Update(Employeeitem);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                    }
                }
                var getIndicatorComptence = await hrRepositoryManager.HrEmpGoalIndicatorsCompetenceRepository.GetAll(x => x.IsDeleted == false && x.GoalIndicatorsId == Id);

                if (getIndicatorComptence.Count() > 0)
                {
                    foreach (var Comptenceitem in getIndicatorComptence)
                    {
                        Comptenceitem.IsDeleted = true;
                        Comptenceitem.ModifiedBy = session.UserId;
                        Comptenceitem.ModifiedOn = DateTime.Now;
                        hrRepositoryManager.HrEmpGoalIndicatorsCompetenceRepository.Update(Comptenceitem);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                    }
                }

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<HrEmpGoalIndicatorDto>.SuccessAsync(mapper.Map<HrEmpGoalIndicatorDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrEmpGoalIndicatorDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IResult<HrEmpGoalIndicatorEditDto>> Update(HrEmpGoalIndicatorEditDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IResult<string>> Add(HrEmpGoalIndicatorAddDto entity, CancellationToken cancellationToken = default)
        {
            try
            {
                int? MinKPIs = 0;
                int? MaxKPIs = 0;
                // الحد الادنى لمؤشرات الاداء والحد الأعلى
                var getKPi = await hrRepositoryManager.HrKpiTemplateRepository.GetOne(K => K.IsDeleted == false && K.Id == entity.KpiTemplatesId);
                if (getKPi == null) return await Result<string>.FailAsync($"نموذج التقييم غير موجود");

                MinKPIs = getKPi.MinKpis;
                MaxKPIs = getKPi.MaxKpis;
                if (entity.Competence.Count() > MaxKPIs || entity.Competence.Count() < MinKPIs) return await Result<string>.FailAsync($"الحد الادنى لمؤشرات الاداء {MinKPIs}....الحد الاعلاء لمؤشرات الاداء {MaxKPIs}");

                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                var GoalIndicators = new HrEmpGoalIndicator
                {
                    CreatedBy = session.UserId,
                    CreatedOn = DateTime.Now,
                    IsDeleted = false,
                    PeriodId = entity.PeriodId,
                    KpiTemplatesId = entity.KpiTemplatesId,
                };
                var AddedGoalIndicators = await hrRepositoryManager.HrEmpGoalIndicatorRepository.AddAndReturn(GoalIndicators);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var AllEmployees = await mainRepositoryManager.InvestEmployeeRepository.GetAll(x => x.IsDeleted == false && x.Isdel == false && x.StatusId != 2);

                var getAllTemplates = await hrRepositoryManager.HrKpiTemplateRepository.GetAll(x => x.IsDeleted == false);
                var GetAllGoalIndicatorsID = await hrRepositoryManager.HrEmpGoalIndicatorsEmployeeRepository.GetAll(x => x.IsDeleted == false);
                var hrEmpGoalIndicator = await hrRepositoryManager.HrEmpGoalIndicatorRepository.GetAll(x => x.IsDeleted && x.KpiTemplatesId == entity.KpiTemplatesId && x.PeriodId == entity.PeriodId);
                string CheckMsg = string.Empty;
                // نفحص هل تمت إضافة موظف بنفس الفترة والنموذج مسبقاً

                foreach (var EmpItem in entity.Employee)
                {
                    var checkifEmpExist = AllEmployees.Where(x => x.EmpId == EmpItem.EmpCode).FirstOrDefault();
                    if (checkifEmpExist == null) return await Result<string>.FailAsync($"الموظف رقم {EmpItem.EmpCode} غير موجود ");

                    var GoalIndicatorsID = GetAllGoalIndicatorsID.Where(x => x.EmpId == checkifEmpExist.Id).ToList();
                    var GoalsIds = GoalIndicatorsID.Select(x => x.GoalIndicatorsId).ToList();
                    var checkIfIndicatorEmployeeExist = hrEmpGoalIndicator.AsEnumerable().Where(entity => GoalsIds.Contains(entity.Id)).ToList();
                    if (checkIfIndicatorEmployeeExist.Count() > 0)
                    {
                        CheckMsg += CheckMsg + EmpItem.EmpCode + ",";
                        continue;
                    }

                }
                if (!string.IsNullOrEmpty(CheckMsg))
                    return await Result<string>.FailAsync($"الموظفين التاليين تمت اضافتهم بنفس الفترة والنموذج مسبقاً   {CheckMsg.Substring(0, CheckMsg.Length - 1)}");

                // نقوم باضافة الموظفين
                foreach (var EmpItem in entity.Employee)
                {
                    var checkifEmpExist = AllEmployees.Where(x => x.EmpId == EmpItem.EmpCode).FirstOrDefault();
                    if (checkifEmpExist == null) return await Result<string>.FailAsync($"الموظف رقم {EmpItem.EmpCode} غير موجود ");

                    var IndicatorsEmployee = new HrEmpGoalIndicatorsEmployee
                    {
                        CreatedBy = session.UserId,
                        CreatedOn = DateTime.Now,
                        IsDeleted = false,
                        EmpId = checkifEmpExist.Id,
                        GoalIndicatorsId = AddedGoalIndicators.Id,
                    };
                    await hrRepositoryManager.HrEmpGoalIndicatorsEmployeeRepository.Add(IndicatorsEmployee);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                }

                // نقوم باضافة مؤشرات الأداء

                foreach (var ComptenceItem in entity.Competence)
                {
                    var IndicatorsCompetence = new HrEmpGoalIndicatorsCompetence
                    {
                        CreatedBy = session.UserId,
                        CreatedOn = DateTime.Now,
                        IsDeleted = false,
                        CompetencesId = ComptenceItem.CompetencesId,
                        Note = ComptenceItem.Note ?? "",
                        Target = ComptenceItem.Target.ToString(),
                        Weight = ComptenceItem.Weight.ToString(),
                        GoalIndicatorsId = AddedGoalIndicators.Id,

                    };
                    await hrRepositoryManager.HrEmpGoalIndicatorsCompetenceRepository.Add(IndicatorsCompetence);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<string>.SuccessAsync(localization.GetResource1("AddSuccess"), "", 200);

            }
            catch (Exception exp)
            {

                return await Result<string>.FailAsync($"EXP in Add at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }


        public async Task<IResult<string>> Edit(HrEmpGoalIndicatorAddDto entity, CancellationToken cancellationToken = default)
        {
            try
            {
                int? MinKPIs = 0;
                int? MaxKPIs = 0;
                // الحد الادنى لمؤشرات الاداء والحد الأعلى
                var getKPi = await hrRepositoryManager.HrKpiTemplateRepository.GetOne(K => K.IsDeleted == false && K.Id == entity.KpiTemplatesId);
                if (getKPi == null) return await Result<string>.FailAsync($"نموذج التقييم غير موجود");

                MinKPIs = getKPi.MinKpis;
                MaxKPIs = getKPi.MaxKpis;
                if (entity.Competence.Where(x => x.IsDeleted == false).Count() > MaxKPIs || entity.Competence.Where(x => x.IsDeleted == false).Count() < MinKPIs) return await Result<string>.FailAsync($"الحد الادنى لمؤشرات الاداء {MinKPIs}....الحد الاعلاء لمؤشرات الاداء {MaxKPIs}");

                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                var EmpGoalIndicator = await hrRepositoryManager.HrEmpGoalIndicatorRepository.GetOne(x => x.IsDeleted == false && x.Id == entity.Id);
                if (EmpGoalIndicator == null) return await Result<string>.FailAsync($"{localization.GetMessagesResource("NoItemFoundToEdit")}");

                EmpGoalIndicator.PeriodId = entity.PeriodId;
                hrRepositoryManager.HrEmpGoalIndicatorRepository.Update(EmpGoalIndicator);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var AllEmployees = await mainRepositoryManager.InvestEmployeeRepository.GetAll(x => x.IsDeleted == false && x.Isdel == false && x.StatusId != 2);

                var getAllTemplates = await hrRepositoryManager.HrKpiTemplateRepository.GetAll(x => x.IsDeleted == false);
                var GetAllGoalIndicatorsID = await hrRepositoryManager.HrEmpGoalIndicatorsEmployeeRepository.GetAll(x => x.IsDeleted == false);
                var hrEmpGoalIndicator = await hrRepositoryManager.HrEmpGoalIndicatorRepository.GetAll(x => x.IsDeleted && x.KpiTemplatesId == entity.KpiTemplatesId && x.PeriodId == entity.PeriodId);
                string CheckMsg = string.Empty;
                // نفحص هل تمت إضافة موظف بنفس الفترة والنموذج مسبقاً

                foreach (var EmpItem in entity.Employee)
                {
                    var checkifEmpExist = AllEmployees.Where(x => x.EmpId == EmpItem.EmpCode).FirstOrDefault();
                    if (checkifEmpExist == null) return await Result<string>.FailAsync($"الموظف رقم {EmpItem.EmpCode} غير موجود ");

                    var GoalIndicatorsID = GetAllGoalIndicatorsID.Where(x => x.EmpId == checkifEmpExist.Id).ToList();
                    var GoalsIds = GoalIndicatorsID.Select(x => x.GoalIndicatorsId).ToList();
                    var checkIfIndicatorEmployeeExist = hrEmpGoalIndicator.AsEnumerable().Where(entity => GoalsIds.Contains(entity.Id)).ToList();
                    if (checkIfIndicatorEmployeeExist.Count() > 0 && !GoalsIds.Contains(entity.Id))
                    {
                        CheckMsg += CheckMsg + EmpItem.EmpCode + ",";
                        continue;
                    }

                }
                if (!string.IsNullOrEmpty(CheckMsg))
                    return await Result<string>.FailAsync($"الموظفين التاليين تمت اضافتهم بنفس الفترة والنموذج مسبقاً   {CheckMsg.Substring(0, CheckMsg.Length - 1)}");

                // نقوم باضافة الموظفين
                foreach (var EmpItem in entity.Employee)
                {
                    var checkifEmpExist = AllEmployees.Where(x => x.EmpId == EmpItem.EmpCode).FirstOrDefault();
                    if (checkifEmpExist == null) return await Result<string>.FailAsync($"الموظف رقم {EmpItem.EmpCode} غير موجود ");

                    if (EmpItem.IsDeleted == true && EmpItem.Id > 0)
                    {

                        var getHrEmpGoalIndicatorsEmployee = await hrRepositoryManager.HrEmpGoalIndicatorsEmployeeRepository.GetOne(x=>x.Id==EmpItem.Id);

                        if (getHrEmpGoalIndicatorsEmployee != null)
                        {
                            getHrEmpGoalIndicatorsEmployee.IsDeleted = true;
                            getHrEmpGoalIndicatorsEmployee.ModifiedBy = session.UserId;
                            getHrEmpGoalIndicatorsEmployee.ModifiedOn = DateTime.Now;
                             hrRepositoryManager.HrEmpGoalIndicatorsEmployeeRepository.Update(getHrEmpGoalIndicatorsEmployee);
                            await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                        }
                    }
                    else if(EmpItem.IsDeleted == false && EmpItem.Id== 0)
                    {
                        var IndicatorsEmployee = new HrEmpGoalIndicatorsEmployee
                        {
                            CreatedBy = session.UserId,
                            CreatedOn = DateTime.Now,
                            IsDeleted = false,
                            EmpId = checkifEmpExist.Id,
                            GoalIndicatorsId = EmpGoalIndicator.Id,
                        };
                        await hrRepositoryManager.HrEmpGoalIndicatorsEmployeeRepository.Add(IndicatorsEmployee);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


                    }


                }

                // نقوم باضافة مؤشرات الأداء

                foreach (var ComptenceItem in entity.Competence)
                {
                    if (ComptenceItem.IsDeleted == true && ComptenceItem.Id > 0)
                    {

                        var getHrEmpGoalIndicatorsCompetence = await hrRepositoryManager.HrEmpGoalIndicatorsCompetenceRepository.GetOne(x => x.Id == ComptenceItem.Id);

                        if (getHrEmpGoalIndicatorsCompetence != null)
                        {
                            getHrEmpGoalIndicatorsCompetence.IsDeleted = true;
                            getHrEmpGoalIndicatorsCompetence.ModifiedBy = session.UserId;
                            getHrEmpGoalIndicatorsCompetence.ModifiedOn = DateTime.Now;
                            hrRepositoryManager.HrEmpGoalIndicatorsCompetenceRepository.Update(getHrEmpGoalIndicatorsCompetence);
                            await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                        }
                    }
                    else if (ComptenceItem.IsDeleted == false && ComptenceItem.Id == 0)
                    {
                        var IndicatorsCompetence = new HrEmpGoalIndicatorsCompetence
                        {
                            CreatedBy = session.UserId,
                            CreatedOn = DateTime.Now,
                            IsDeleted = false,
                            CompetencesId = ComptenceItem.CompetencesId,
                            Note = ComptenceItem.Note ?? "",
                            Target = ComptenceItem.Target.ToString(),
                            Weight = ComptenceItem.Weight.ToString(),
                            GoalIndicatorsId = EmpGoalIndicator.Id,

                        };
                        await hrRepositoryManager.HrEmpGoalIndicatorsCompetenceRepository.Add(IndicatorsCompetence);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }
                }



                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<string>.SuccessAsync(localization.GetResource1("UpdateSuccess"), "", 200);

            }
            catch (Exception exp)
            {

                return await Result<string>.FailAsync($"EXP in Edit at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<HrKpiTemplateDto>> AddEmployee(long? EmpId, long? TemplateId, CancellationToken cancellationToken = default)
        {
            try
            {
                var getAllUsers = await mainRepositoryManager.SysUserRepository.GetAllVw(x => x.IsDeleted == false);

                var userGroupIds = getAllUsers.Where(x => x.EmpId == EmpId)
                                              .Select(x => x.GroupsId)
                                              .Distinct()
                                              .ToList();

                if (!userGroupIds.Any())
                {
                    return await Result<HrKpiTemplateDto>.FailAsync("  لا يوجد  نموذج  تقييم  للموظف   .");
                }

                var getAllTemplates = await hrRepositoryManager.HrKpiTemplateRepository.GetAll(x => x.IsDeleted == false);

                var matchingTemplates = getAllTemplates
                    .Where(t => t.GroupsId != null && userGroupIds.Any(g => t.GroupsId.Split(',').Contains(g)))
                    .ToList();

                if (!matchingTemplates.Any())
                {
                    return await Result<HrKpiTemplateDto>.FailAsync("  لا يوجد  نموذج  تقييم  للموظف   .");
                }

                var lastTemplate = matchingTemplates.OrderByDescending(t => t.CreatedOn).FirstOrDefault();

                if (lastTemplate == null)
                {
                    return await Result<HrKpiTemplateDto>.FailAsync("  لا يوجد  نموذج  تقييم  للموظف   .");
                }

                if (TemplateId == 0)
                {
                    TemplateId = lastTemplate.Id;
                }

                if (lastTemplate.Id != TemplateId)
                {
                    return await Result<HrKpiTemplateDto>.FailAsync("لا يمكن إضافة الموظف، حيث أنه يتبع  نموذج  تقييم اخر .");
                }

                // Step 7: Return the last template mapped to DTO
                return await Result<HrKpiTemplateDto>.SuccessAsync(mapper.Map<HrKpiTemplateDto>(lastTemplate), "", 200);
            }
            catch (Exception exp)
            {
                return await Result<HrKpiTemplateDto>.FailAsync($"EXP in Add at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

    }

}