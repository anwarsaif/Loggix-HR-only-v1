using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Helpers;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;
using Logix.Domain.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.Services.HR
{
    public class HrDecisionService : GenericQueryService<HrDecision, HrDecisionDto, HrDecisionsVw>, IHrDecisionService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly ILocalizationService localization;
        private readonly IWFRepositoryManager wFRepositoryManager;
        private readonly IEmailAppHelper emailAppHelper;
        private readonly IWorkflowHelper workflowHelper;

        public HrDecisionService(IQueryRepository<HrDecision> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, ILocalizationService localization, IHrRepositoryManager hrRepositoryManager, IWFRepositoryManager wFRepositoryManager, IEmailAppHelper emailAppHelper, IWorkflowHelper workflowHelper) : base(queryRepository, mapper)
        {
            this.hrRepositoryManager = hrRepositoryManager;
            this.mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.localization = localization;
            this.wFRepositoryManager = wFRepositoryManager;
            this.emailAppHelper = emailAppHelper;
            this.workflowHelper = workflowHelper;
        }

        public async Task<IResult<HrDecisionDto>> Add(HrDecisionDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrDecisionDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");
            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                var item = _mapper.Map<HrDecision>(entity);
                item.EmpId = 0;
                item.CreatedBy = session.UserId;
                item.CreatedOn = DateTime.Now;
                item.IsDeleted = false;
                entity.AppTypeID ??= 0;
                var empID = Convert.ToInt64(session.EmpId);
                //  ارسال الى سير العمل
                var GetApp_ID = await workflowHelper.Send(empID, 2107, entity.AppTypeID);
                item.AppId = GetApp_ID;
                var decisions = await hrRepositoryManager.HrDecisionRepository.GetAll();
                var decCode = decisions.Max(x => x.DecCode);
                item.DecCode = decCode + 1;
                var newEntity = await hrRepositoryManager.HrDecisionRepository.AddAndReturn(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = _mapper.Map<HrDecisionDto>(newEntity);

                foreach (var SingleEmp in entity.EmpInfo)
                {
                    var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == SingleEmp.EmpCode && x.IsDeleted == false && x.Isdel == false);
                    if (checkEmpExist == null) return await Result<HrDecisionDto>.FailAsync($"employee of Code :{SingleEmp.EmpCode}{localization.GetResource1("EmployeeNotFound")}");

                    var newItem = new HrDecisionsEmployee
                    {
                        CreatedBy = session.UserId,
                        CreatedOn = DateTime.Now,
                        IsDeleted = false,
                        EmpId = checkEmpExist.Id,
                        DecisionsId = newEntity.Id,


                    };
                    await hrRepositoryManager.HrDecisionsEmployeeRepository.Add(newItem);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                }

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);


                return await Result<HrDecisionDto>.SuccessAsync(entityMap, localization.GetResource1("AddSuccess"));
            }
            catch (Exception)
            {
                return await Result<HrDecisionDto>.FailAsync(localization.GetResource1("ErrorOccurredDuring"));
            }

        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrDecisionRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrDecisionDto>.Fail($"{localization.GetMessagesResource("NoItemFoundToDelete")}");

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;

                hrRepositoryManager.HrDecisionRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrDecisionDto>.SuccessAsync(_mapper.Map<HrDecisionDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrDecisionDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrDecisionRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrDecisionDto>.Fail($"{localization.GetMessagesResource("NoItemFoundToDelete")}");

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;

                hrRepositoryManager.HrDecisionRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrDecisionDto>.SuccessAsync(_mapper.Map<HrDecisionDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrDecisionDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }



        public async Task<IResult<HrDecisionEditDto>> Update(HrDecisionEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrDecisionEditDto>.FailAsync(localization.GetMessagesResource("AddNullEntity"));
            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                var item = await hrRepositoryManager.HrDecisionRepository.GetById(entity.Id);

                if (item == null) return await Result<HrDecisionEditDto>.FailAsync(localization.GetMessagesResource("NoItemFoundToEdit"));
                if (entity.EmpInfo.Where(x => x.IsDeleted == false).Count() <= 0)
                {
                    return await Result<HrDecisionEditDto>.FailAsync($"يجب ادخال موظف واحد على الأقل");
                }

                _mapper.Map(entity, item);
                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.EmpId = 0;
                hrRepositoryManager.HrDecisionRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                var getAllEmployees = await mainRepositoryManager.InvestEmployeeRepository.GetAll(x => x.IsDeleted == false && x.Isdel == false && x.StatusId != 2);
                var getAllDecisionsEmployees = await hrRepositoryManager.HrDecisionsEmployeeRepository.GetAll(x => x.IsDeleted == false);
                foreach (var singleItem in entity.EmpInfo)
                {
                    if (singleItem.IsDeleted == true && singleItem.Id > 0)
                    {
                        var CheckIfRecordExist = getAllDecisionsEmployees.Where(x => x.Id == singleItem.Id).FirstOrDefault();
                        if (CheckIfRecordExist == null) return await Result<HrDecisionEditDto>.FailAsync($"--- تأكد من وجود البيانات سابقا ---");
                        CheckIfRecordExist.IsDeleted = true;
                        CheckIfRecordExist.ModifiedBy = session.UserId;
                        CheckIfRecordExist.ModifiedOn = DateTime.Now;
                        hrRepositoryManager.HrDecisionsEmployeeRepository.Update(CheckIfRecordExist);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }

                    else if (singleItem.IsDeleted == false && singleItem.Id == 0)
                    {
                        //  بمعنى انه حقل جديد
                        var CheckIfEmpExist = getAllEmployees.Where(x => x.EmpId == singleItem.EmpCode).FirstOrDefault();
                        if (CheckIfEmpExist == null) return await Result<HrDecisionEditDto>.FailAsync($"--- لايوجد موظف بهذا الرقم: {singleItem.EmpCode}---");
                        var newEmployee = new HrDecisionsEmployee
                        {
                            CreatedBy = session.UserId,
                            CreatedOn = DateTime.Now,
                            EmpId = CheckIfEmpExist.Id,
                            IsDeleted = false,
                            DecisionsId = entity.Id
                        };
                        await hrRepositoryManager.HrDecisionsEmployeeRepository.Add(newEmployee);

                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


                    }
                }



                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<HrDecisionEditDto>.SuccessAsync(_mapper.Map<HrDecisionEditDto>(item), localization.GetMessagesResource("success"));
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrDecisionEditDto>.FailAsync($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<string>> SendEmail(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                List<string> Emails = new List<string>();
                List<string> Attachments = new List<string>();
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                var item = await hrRepositoryManager.HrDecisionRepository.GetOneVw(x => x.Id == Id);

                if (item == null) return await Result<string>.FailAsync($"لا يوجد قرار بهذا الرقم  :{Id} ");

                if (item.AppId <= 0) return await Result<string>.FailAsync($"لايوجد طلب لهذا القرار ليتم ارسال الاشعار الى الموظفين الموجودين في سير العمل ");

                var ApplicationsStatus = await wFRepositoryManager.WfApplicationsStatusRepository.GetAllVw(X => X.ApplicationsId == item.AppId);
                if (ApplicationsStatus == null) return await Result<string>.FailAsync($"لايوجد طلب لهذا القرار ليتم ارسال الاشعار الى الموظفين الموجودين في سير العمل ");

                foreach (var SingleEmail in ApplicationsStatus)
                {
                    if (!string.IsNullOrEmpty(SingleEmail.UserEmail))
                        Emails.Add(SingleEmail.UserEmail);
                }
                if (Emails.Count > 0)
                {
                    var DecTypeName = (session.Language == 1) ? item.DecTypeName : item.DecTypeName2;
                    if (!string.IsNullOrEmpty(item.FileUrl))
                    {
                        Attachments.Add(item.FileUrl);

                    }
                    await emailAppHelper.SendEmailWithAttachmentAsync(Emails, DecTypeName ?? "", item.Note ?? "", 1, Attachments);
                }
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                return await Result<string>.SuccessAsync("تمت عملية الإرسال بنجاح ");
            }
            catch (Exception)
            {

                return await Result<string>.SuccessAsync("لم تم عملية الإرسال ");
            }



        }

    }
}
