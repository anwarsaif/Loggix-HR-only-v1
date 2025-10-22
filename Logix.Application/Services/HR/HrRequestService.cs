using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.DTOs.Main;
using Logix.Application.Helpers;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;
using Logix.Domain.Main;
using System.Globalization;

namespace Logix.Application.Services.HR
{
    // طلبات الموارد البشرية
    public class HrRequestService : GenericQueryService<HrRequest, HrRequestDto, HrRequestVw>, IHrRequestService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly ILocalizationService localization;
        private readonly IWorkflowHelper workflowHelper;

        public HrRequestService(IQueryRepository<HrRequest> queryRepository, IHrRepositoryManager hrRepositoryManager, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, ILocalizationService localization, IWorkflowHelper workflowHelper) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this.hrRepositoryManager = hrRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.localization = localization;
            this.workflowHelper = workflowHelper;
        }

        public Task<IResult<HrRequestDto>> Add(HrRequestDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
        public Task<IResult<HrRequestEditDto>> Update(HrRequestEditDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IResult<string>> Add(HrRequestAddDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<string>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");

            try
            {
                entity.CreatedBy = session.UserId;
                entity.CreatedOn = DateTime.Now;
                entity.IsDeleted = false;
                entity.FacilityId = session.FacilityId;
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                // start of check request Details Request Details

                foreach (var record in entity.RequestDto)
                {
                    var investEmployees = await mainRepositoryManager.InvestEmployeeRepository.GetOne(e => e.EmpId == record.empCode && e.IsDeleted == false);
                    if (investEmployees == null)
                    {
                        return await Result<string>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                    }
                    record.EmpId = investEmployees.Id;
                    record.RequestType = entity.RequestType;

                    if (entity.RequestType == 1 || entity.RequestType == 2 || entity.RequestType == 3)
                    {
                        if (record.Value == 0) return await Result<string>.FailAsync("يجب تحديد القيمة");
                        if (record.typeId == 0) return await Result<string>.FailAsync("يجب تحديد النوع");
                    }
                    if (entity.RequestType == 6)
                    {
                        if (record.typeId == 0) return await Result<string>.FailAsync("يجب تحديد النوع");
                    }

                    if (entity.RequestType == 1)
                    {
                        record.AllownceId = record.typeId;
                        record.DeductionId = 0;
                        record.OverTimeId = 0;
                        record.AbsenceTypeId = 0;
                    }
                    else if (entity.RequestType == 2)
                    {
                        record.AllownceId = 0;
                        record.DeductionId = record.typeId;
                        record.OverTimeId = 0;
                        record.AbsenceTypeId = 0;
                    }
                    else if (entity.RequestType == 3)
                    {
                        record.AllownceId = 0;
                        record.DeductionId = 0;
                        record.OverTimeId = record.typeId;
                        record.AbsenceTypeId = 0;
                    }
                    else if (entity.RequestType == 6)
                    {
                        record.AllownceId = 0;
                        record.DeductionId = 0;
                        record.OverTimeId = 0;
                        record.AbsenceTypeId = record.typeId;
                    }
                    else
                    {
                        record.AllownceId = 0;
                        record.DeductionId = 0;
                        record.OverTimeId = 0;
                        record.AbsenceTypeId = 0;
                    }
                }
                entity.AppTypeId ??= 0;


                //  ارسال الى سير العمل

                var GetApp_ID = await workflowHelper.Send(session.EmpId, 1312, entity.AppTypeId);
                // start of adding Request
                var newItem = _mapper.Map<HrRequest>(entity);
                newItem.StatusId = 1;
                newItem.EmpId = session.EmpId;
                newItem.AppId = GetApp_ID;
                var newEntity = await hrRepositoryManager.HrRequestRepository.AddAndReturn(newItem);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                // start of adding Request Details and Assign Request Id
                foreach (var record in entity.RequestDto)
                {
                    //var emp = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == record.EmpId.ToString());
                    var requestDetail = _mapper.Map<HrRequestDetaile>(record);
                    requestDetail.RequestId = newEntity.Id;
                    requestDetail.RequestType = newEntity.RequestType;
                    //if (emp != null)
                    //    requestDetail.EmpId = emp.Id;
                    //else
                    //    return await Result<string>.FailAsync(localization.GetResource1("EmployeeNotFound"));

                    await hrRepositoryManager.HrRequestDetaileRepository.Add(requestDetail);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }
                // start of adding files
                if (entity.fileDtos != null && entity.fileDtos.Count() >= 1)
                {
                    foreach (var item in entity.fileDtos)
                    {
                        var newFile = new SysFile
                        {
                            CreatedBy = session.UserId,
                            CreatedOn = DateTime.Now,
                            FileUrl = item.FileURL,
                            IsDeleted = false,
                            FileName = item.FileName,
                            FileDate = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture),
                            PrimaryKey = newEntity.Id,
                            TableId = 89,
                            FacilityId = (int)session.FacilityId

                        };
                        await mainRepositoryManager.SysFileRepository.Add(newFile);
                        await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                    }

                }


                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<string>.SuccessAsync(localization.GetResource1("AddSuccess"));
            }
            catch (Exception exc)
            {
                return await Result<string>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrRequestRepository.GetById(Id);
                if (item == null) return Result<HrRequestDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;

                hrRepositoryManager.HrRequestRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrRequestDto>.SuccessAsync(_mapper.Map<HrRequestDto>(item), localization.GetMessagesResource("success"));
            }
            catch (Exception exp)
            {
                return await Result<HrRequestDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IResult<string>> UpdateRequest(HrRequestEditDto entity, CancellationToken cancellationToken = default)
        {
            try
            {
                if (entity == null) return await Result<string>.FailAsync($"Error in {GetType()} : the passed entity IS NULL.");

                var item = await hrRepositoryManager.HrRequestRepository.GetById(entity.Id);

                if (item == null) return await Result<string>.FailAsync($"--- there is no Data with this id: {entity.Id}---");
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = (int)session.UserId;
                _mapper.Map(entity, item);
                item.FacilityId = session.FacilityId;
                //hrRepositoryManager.HrRequestRepository.Update(item);

                //await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                //entity.CreatedBy = session.UserId;
                //entity.CreatedOn = DateTime.Now;
                item.IsDeleted = false;
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                // start of check request Details Request Details

                foreach (var record in entity.RequestDto)
                {
                    var investEmployees = await mainRepositoryManager.InvestEmployeeRepository.GetOne(e => e.EmpId == record.empCode && e.IsDeleted == false);
                    if (investEmployees == null)
                    {
                        return await Result<string>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                    }
                    record.EmpId = investEmployees.Id;
                    record.RequestType = entity.RequestType;

                    if (entity.RequestType == 1 || entity.RequestType == 2 || entity.RequestType == 3)
                    {
                        if (record.Value == 0) return await Result<string>.FailAsync("يجب تحديد القيمة");
                        if (record.typeId == 0) return await Result<string>.FailAsync("يجب تحديد النوع");
                    }
                    if (entity.RequestType == 6)
                    {
                        if (record.typeId == 0) return await Result<string>.FailAsync("يجب تحديد النوع");
                    }

                    if (entity.RequestType == 1)
                    {
                        record.AllownceId = record.typeId;
                        record.DeductionId = 0;
                        record.OverTimeId = 0;
                        record.AbsenceTypeId = 0;
                    }
                    else if (entity.RequestType == 2)
                    {
                        record.AllownceId = 0;
                        record.DeductionId = record.typeId;
                        record.OverTimeId = 0;
                        record.AbsenceTypeId = 0;
                    }
                    else if (entity.RequestType == 3)
                    {
                        record.AllownceId = 0;
                        record.DeductionId = 0;
                        record.OverTimeId = record.typeId;
                        record.AbsenceTypeId = 0;
                    }
                    else if (entity.RequestType == 6)
                    {
                        record.AllownceId = 0;
                        record.DeductionId = 0;
                        record.OverTimeId = 0;
                        record.AbsenceTypeId = record.typeId;
                    }
                    else
                    {
                        record.AllownceId = 0;
                        record.DeductionId = 0;
                        record.OverTimeId = 0;
                        record.AbsenceTypeId = 0;
                    }
                }
                entity.AppTypeId ??= 0;


                //  ارسال الى سير العمل

                var GetApp_ID = await workflowHelper.Send(session.EmpId, 1312, entity.AppTypeId);
                // start of adding Request
                //var newItem = _mapper.Map<HrRequest>(entity);
                item.StatusId = 1;
                item.EmpId = session.EmpId;
                item.AppId = GetApp_ID;
                hrRepositoryManager.HrRequestRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                foreach (var record in entity.RequestDto)
                {
                    var requestDetail = _mapper.Map<HrRequestDetaile>(record);
                    requestDetail.RequestId = item.Id;
                    requestDetail.RequestType = item.RequestType;
                    requestDetail.IsDeleted = record.IsDeleted;
                    if (record.Id != 0)
                    {
                        requestDetail.ModifiedBy = session.UserId;
                        requestDetail.ModifiedOn = DateTime.Now;
                        hrRepositoryManager.HrRequestDetaileRepository.Update(requestDetail);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }
                    else
                    {
                        requestDetail.CreatedBy = session.UserId;
                        requestDetail.CreatedOn = DateTime.Now;
                        await hrRepositoryManager.HrRequestDetaileRepository.Add(requestDetail);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }
                }
                // start of adding files
                if (entity.fileDtos != null && entity.fileDtos.Count() >= 1)
                {
                    foreach (var item2 in entity.fileDtos)
                    {
                        if (item2.Id != 0)
                        {
                            var file = await mainRepositoryManager.SysFileRepository.GetById(item2.Id);
                            if (file != null)
                            {
                                file.FileUrl = item2.FileURL;
                                file.FileName = item2.FileName;
                                file.ModifiedBy = session.UserId;
                                file.ModifiedOn = DateTime.Now;
                                file.IsDeleted = item2.IsDeleted;
                                mainRepositoryManager.SysFileRepository.Update(file);
                                await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                            }
                        }
                        //else
                        //{
                        //    var newFile = new SysFile
                        //    {
                        //        CreatedBy = session.UserId,
                        //        CreatedOn = DateTime.Now,
                        //        FileUrl = item2.FileURL,
                        //        IsDeleted = false,
                        //        FileName = item2.FileName,
                        //        FileDate = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture),
                        //        PrimaryKey = item.Id,
                        //        TableId = 89,
                        //        FacilityId = session.FacilityId
                        //    };
                        //    await mainRepositoryManager.SysFileRepository.Add(newFile);
                        //    await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                        //}
                    }
                }
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                //return await Result<string>.SuccessAsync(localization.GetResource1("AddSuccess"));
                return await Result<string>.SuccessAsync(_mapper.Map<string>(item), localization.GetResource1("UpdateSuccess"));
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<string>.FailAsync($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
    }
}