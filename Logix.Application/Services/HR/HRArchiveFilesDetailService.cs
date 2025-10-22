using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.Services.HR
{
    public class HrArchiveFilesDetailService : GenericQueryService<HrArchiveFilesDetail, HrArchiveFilesDetailDto, HrArchiveFilesDetailsVw>, IHrArchiveFilesDetailService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;

        private readonly IMainRepositoryManager _mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly ILocalizationService localization;


        public HrArchiveFilesDetailService(IQueryRepository<HrArchiveFilesDetail> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization)   : base(queryRepository, mapper)
        {
            this._mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
        }


        public async Task<IResult<HrArchiveFilesDetailDto>> Add(HrArchiveFilesDetailDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrArchiveFilesDetailDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

            try
            {
                var checkEmp = await _mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (checkEmp == null) return await Result<HrArchiveFilesDetailDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));

                entity.CreatedBy = session.UserId;
                entity.CreatedOn = DateTime.Now;
                var item = _mapper.Map<HrArchiveFilesDetail>(entity);
                item.EmpId = checkEmp.Id;
                item.IsDeleted = false;
                var newEntity = await hrRepositoryManager.HrArchiveFilesDetailRepository.AddAndReturn(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                var entityMap = _mapper.Map<HrArchiveFilesDetailDto>(newEntity);

                return await Result<HrArchiveFilesDetailDto>.SuccessAsync(entityMap, localization.GetMessagesResource("AddSuccess"));
            }
            catch (Exception exc)
            {
                return await Result<HrArchiveFilesDetailDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrArchiveFilesDetailRepository.GetById(Id);
                if (item == null) return Result<HrArchiveFilesDetailDto>.Fail($"--- there is no Data with this id: {Id}---");

                item.IsDeleted = true;
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = (int)session.UserId;
                hrRepositoryManager.HrArchiveFilesDetailRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrArchiveFilesDetailDto>.SuccessAsync(_mapper.Map<HrArchiveFilesDetailDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<HrArchiveFilesDetailDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrArchiveFilesDetailRepository.GetById(Id);
                if (item == null) return Result<HrArchiveFilesDetailDto>.Fail($"--- there is no Data with this id: {Id}---");

                item.IsDeleted = true;
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = (int)session.UserId;
                hrRepositoryManager.HrArchiveFilesDetailRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrArchiveFilesDetailDto>.SuccessAsync(_mapper.Map<HrArchiveFilesDetailDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<HrArchiveFilesDetailDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<HrArchiveFilesDetailEditDto>> Update(HrArchiveFilesDetailEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrArchiveFilesDetailEditDto>.FailAsync($"Error in {GetType()} : the passed entity IS NULL.");

            try
            {
                var checkEmp = await _mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (checkEmp == null) return await Result<HrArchiveFilesDetailEditDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));

                var item = await hrRepositoryManager.HrArchiveFilesDetailRepository.GetById(entity.Id);

                if (item == null) return await Result<HrArchiveFilesDetailEditDto>.FailAsync($"--- there is no Data with this id: {entity.Id}---");
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = (int)session.UserId;
                item.EmpId = checkEmp.Id;
                item.IsDeleted = false;

                hrRepositoryManager.HrArchiveFilesDetailRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrArchiveFilesDetailEditDto>.SuccessAsync(_mapper.Map<HrArchiveFilesDetailEditDto>(item), "Item updated successfully");
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrArchiveFilesDetailEditDto>.FailAsync($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
    }
}
