using AutoMapper;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Helpers;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices;
using Logix.Application.Interfaces.IServices.ACC;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.ACC;
using Logix.Domain.HR;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Globalization;
using System.Numerics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Logix.Application.Services.HR
{
    public class HrDirectJobService : GenericQueryService<HrDirectJob, HrDirectJobDto, HrDirectJobVw>, IHrDirectJobService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly ILocalizationService localization;
        private readonly IMainRepositoryManager mainRepositoryManager;


        public HrDirectJobService(IQueryRepository<HrDirectJob> queryRepository, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization, IMainRepositoryManager mainRepositoryManager) : base(queryRepository, mapper)
        {
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
            this.mainRepositoryManager = mainRepositoryManager;
        }


        public async Task<IResult<HrDirectJobDto>> Add(HrDirectJobDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrDirectJobDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");
            if (string.IsNullOrEmpty(entity.empCode)) return await Result<HrDirectJobDto>.FailAsync(localization.GetResource1("EmployeeIsNumber"));
            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                // check if Emp Is Exist
                var CheckEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.empCode && x.IsDeleted == false && x.Isdel == false);
                if (CheckEmpExist == null) return await Result<HrDirectJobDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                if (CheckEmpExist.StatusId == 2) return await Result<HrDirectJobDto>.FailAsync(localization.GetHrResource("EmpNotActive"));
                if (entity.TypeId <= 0) return await Result<HrDirectJobDto>.FailAsync(localization.GetCommonResource("type"));
                if (entity.TypeId == 2 && entity.VacationIds.Count() <=0)
                {
                    return await Result<HrDirectJobDto>.WarningAsync(localization.GetHrResource("CheckVacationfromlist"));
                }
                var vacationId = 0L;
                if(entity.VacationIds != null && entity.VacationIds.Count() > 0)
                    vacationId = entity.VacationIds.FirstOrDefault();

                entity.Date1 = Bahsas.HDateNow3(session);
                entity.CreatedBy = session.UserId;
                entity.CreatedOn = DateHelper.GetCurrentDateTime();
                entity.IsDeleted = false;
                var item = _mapper.Map<HrDirectJob>(entity);
                item.EmpId = CheckEmpExist.Id;
                item.VacationId = vacationId;
                item.DateDirect = entity.DateDirect;
                item.Note = entity.Note;
                item.TypeId = entity.TypeId;
                var newEntity = await hrRepositoryManager.HrDirectJobRepository.AddAndReturn(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                // start update hr_Vactions
                var getVacation = await hrRepositoryManager.HrVacationsRepository.GetOne(x => x.VacationId == vacationId);
                if (getVacation == null) return await Result<HrDirectJobDto>.FailAsync(localization.GetResource1("AddError"));
                getVacation.VacationRdate = entity.DateDirect;
                hrRepositoryManager.HrVacationsRepository.Update(getVacation);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                // here save files
                if (entity.fileDtos != null && entity.fileDtos.Count() > 0)
                {
                    var addFiles = await mainRepositoryManager.SysFileRepository.SaveFiles(entity.fileDtos, newEntity.Id, 152);
                    await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                var entityMap = _mapper.Map<HrDirectJobDto>(newEntity);

                return await Result<HrDirectJobDto>.SuccessAsync(entityMap, localization.GetResource1("AddSuccess"));
            }
            catch (Exception exc)
            {
                return await Result<HrDirectJobDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }


        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrDirectJobRepository.GetById(Id);
                if (item == null) return Result<HrDirectJobDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;

                hrRepositoryManager.HrDirectJobRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrDirectJobDto>.SuccessAsync(_mapper.Map<HrDirectJobDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrDirectJobDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrDirectJobRepository.GetById(Id);
                if (item == null) return Result<HrDirectJobDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;

                hrRepositoryManager.HrDirectJobRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrDirectJobDto>.SuccessAsync(_mapper.Map<HrDirectJobDto>(item), localization.GetMessagesResource("success"));
            }
            catch (Exception exp)
            {
                return await Result<HrDirectJobDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

		public async Task<IResult<HrDirectJobEditDto>> Update(HrDirectJobEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrDirectJobEditDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");
            if (string.IsNullOrEmpty(entity.empCode)) return await Result<HrDirectJobEditDto>.FailAsync(localization.GetResource1("EmployeeIsNumber"));
            try
            {
                await mainRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                // check if Emp Is Exist
                var checkEmp = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.empCode && x.IsDeleted == false && x.Isdel == false);
                if (checkEmp == null) return await Result<HrDirectJobEditDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                if (entity.TypeId <= 0) return await Result<HrDirectJobEditDto>.FailAsync(localization.GetCommonResource("type"));
                var item = await hrRepositoryManager.HrDirectJobRepository.GetById(entity.Id);
                if (item == null) return await Result<HrDirectJobEditDto>.FailAsync($"{localization.GetMessagesResource("NoItemFoundToEdit")}");

                item.Date1 = Bahsas.HDateNow3(session);
                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateHelper.GetCurrentDateTime();
                item.EmpId = checkEmp.Id;
                item.Note = entity.Note;
                item.DateDirect = entity.DateDirect;
                item.TypeId = entity.TypeId;
                hrRepositoryManager.HrDirectJobRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                // here save files
                if (entity.fileDtos != null && entity.fileDtos.Count() > 0)
                {
                    var addFiles = await mainRepositoryManager.SysFileRepository.SaveFiles(entity.fileDtos, item.Id, 152);
                    await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }
                await mainRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<HrDirectJobEditDto>.SuccessAsync(_mapper.Map<HrDirectJobEditDto>(item), localization.GetResource1("UpdateSuccess"));
            }
            catch (Exception exc)
            {
                return await Result<HrDirectJobEditDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }
		public async Task<IResult<List<HrDirectJobVw>>> Search(HrDirectJobFilterDto filter, CancellationToken cancellationToken = default)
		{
			try
			{
				var BranchesList = session.Branches.Split(',');
                filter.BranchId ??= 0;
                filter.LocationId ??= 0;
                filter.DeptId ??= 0;
                var items = await hrRepositoryManager.HrDirectJobRepository.GetAllVw(e => e.IsDeleted == false && 
                (string.IsNullOrEmpty(filter.EmpId) || e.EmpCode == filter.EmpId) &&
                (string.IsNullOrEmpty(filter.EmpName) || e.EmpName.Contains(filter.EmpName)) &&
                (filter.BranchId == 0 || e.BranchId == filter.BranchId) &&
                (filter.LocationId == 0 || e.Location == filter.LocationId) &&
                (filter.DeptId == 0 || e.DeptId == filter.DeptId) &&
                (BranchesList == null || BranchesList.Contains(e.BranchId.ToString()))
                );
				if (items != null)
				{
					if (items.Count() > 0)
					{
						var res = items.AsQueryable();
						if (!string.IsNullOrEmpty(filter.From))
						{
							res = res.Where(r => r.DateDirect != null && DateHelper.StringToDate(r.DateDirect) >= DateHelper.StringToDate(filter.From));
						}
						if (!string.IsNullOrEmpty(filter.To))
						{
							res = res.Where(r => r.DateDirect != null && DateHelper.StringToDate(r.DateDirect) <= DateHelper.StringToDate(filter.To));
						}
                        if (res.Count() > 0)
                            res = res.Where(x => !string.IsNullOrEmpty(x.DateDirect)).OrderByDescending(x => DateHelper.StringToDate(x.DateDirect));

                        return await Result<List<HrDirectJobVw>>.SuccessAsync(res.ToList(),"");

                    }

                    return await Result<List<HrDirectJobVw>>.SuccessAsync(localization.GetResource1("NosearchResult"));

				}

				return await Result<List<HrDirectJobVw>>.FailAsync("");
			}
			catch (Exception ex)
			{
				return await Result<List<HrDirectJobVw>>.FailAsync(ex.Message);
			}
		}
    }
}
