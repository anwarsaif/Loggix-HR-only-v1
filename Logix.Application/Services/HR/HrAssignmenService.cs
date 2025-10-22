using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.DTOs.Main;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Logix.Application.Services.HR
{
    public class HrAssignmenService : GenericQueryService<HrAssignman, HrAssignmenDto, HrAssignmenVw>, IHrAssignmenService
    {
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        public HrAssignmenService(IQueryRepository<HrAssignman> queryRepository, IMainRepositoryManager mainRepositoryManager, ILocalizationService localization, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
        }

        public async Task<IResult<HrAssignmenDto>> Add(HrAssignmenDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrAssignmenDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
            try
            {
                var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.empCode && x.IsDeleted == false && x.Isdel == false);
                if (checkEmpExist == null) return await Result<HrAssignmenDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));


                entity.IsDeleted = false;
                var item = _mapper.Map<HrAssignman>(entity);
                item.EmpId = checkEmpExist.Id;
                item.FacilityId = (int?)session.FacilityId;
                item.CreatedBy = session.UserId;
                item.CreatedOn = DateTime.Now;
                var newEntity = await hrRepositoryManager.HrAssignmenRepository.AddAndReturn(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = _mapper.Map<HrAssignmenDto>(newEntity);


                return await Result<HrAssignmenDto>.SuccessAsync(entityMap, localization.GetResource1("AddSuccess"));
            }
            catch (Exception)
            {

                return await Result<HrAssignmenDto>.FailAsync(localization.GetResource1("AddError"));
            }
        }

        public async Task<IResult<HrAssignmen2AddDto>> Assignment2Add(HrAssignmen2AddDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrAssignmen2AddDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
            try
            {
                if (!entity.descriptionsDtos.Any()) return await Result<HrAssignmen2AddDto>.FailAsync("يتم إضافة الموظفين اولا");
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                foreach (var item in entity.descriptionsDtos)
                {
                    var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == item.empCode && x.IsDeleted == false && x.Isdel == false);
                    if (checkEmpExist == null) return await Result<HrAssignmen2AddDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                    var newItem = new HrAssignman
                    {
                        EmpId = checkEmpExist.Id,
                        CreatedOn = DateTime.Now,
                        CreatedBy = session.UserId,
                        IsDeleted = false,
                        FacilityId = (int?)session.FacilityId,
                        AssignmentDate = entity.AssignmentDate,
                        FromDate = entity.FromDate,
                        Note = item.Note,
                        ToDate = entity.ToDate,
                        TypeId = entity.TypeId,
                    };
                    await hrRepositoryManager.HrAssignmenRepository.Add(newItem);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<HrAssignmen2AddDto>.SuccessAsync(localization.GetResource1("AddSuccess"));
            }
            catch (Exception)
            {

                return await Result<HrAssignmen2AddDto>.FailAsync(localization.GetResource1("AddError"));
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {

                var item = await hrRepositoryManager.HrAssignmenRepository.GetById(Id);
                if (item == null) return Result<HrAssignmenDto>.Fail($"--- there is no Data with this id: {Id}---");
                item.IsDeleted = true;
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = (int)session.UserId;
                hrRepositoryManager.HrAssignmenRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrAssignmenDto>.SuccessAsync(_mapper.Map<HrAssignmenDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<HrAssignmenDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {

                var item = await hrRepositoryManager.HrAssignmenRepository.GetById(Id);
                if (item == null) return Result<HrAssignmenDto>.Fail($"--- there is no Data with this id: {Id}---");
                item.IsDeleted = true;
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = (int)session.UserId;
                hrRepositoryManager.HrAssignmenRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrAssignmenDto>.SuccessAsync(_mapper.Map<HrAssignmenDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<HrAssignmenDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<HrAssignmenEditDto>> Update(HrAssignmenEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrAssignmenEditDto>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));
            if (string.IsNullOrEmpty(entity.empCode)) return await Result<HrAssignmenEditDto>.FailAsync($"Employee Id Is Required");

            try
            {
                // check if Emp Is Exist
                var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.empCode && x.IsDeleted == false && x.Isdel == false);
                if (checkEmpExist == null) return await Result<HrAssignmenEditDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                var item = await hrRepositoryManager.HrAssignmenRepository.GetById(entity.Id);

                if (item == null) return await Result<HrAssignmenEditDto>.FailAsync(localization.GetResource1("UpdateError"));


                _mapper.Map(entity, item);
                item.EmpId = checkEmpExist.Id;
                item.FacilityId = (int?)session.FacilityId;
                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = false;
                hrRepositoryManager.HrAssignmenRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrAssignmenEditDto>.SuccessAsync(_mapper.Map<HrAssignmenEditDto>(item), localization.GetResource1("UpdateSuccess"));
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrAssignmenEditDto>.FailAsync($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

    }

}
