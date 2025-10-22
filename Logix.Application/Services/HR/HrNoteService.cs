using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.DTOs.Main;
using Logix.Application.DTOs.OPM;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrNoteService : GenericQueryService<HrNote, HrNoteDto, HrNoteVw>, IHrNoteService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly IMainRepositoryManager _mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly ILocalizationService localization;

        public HrNoteService(IQueryRepository<HrNote> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, ILocalizationService localization, IHrRepositoryManager hrRepositoryManager) : base(queryRepository, mapper)
        {
            this.hrRepositoryManager = hrRepositoryManager;
            this._mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.localization = localization;
        }

        public async Task<IResult<HrNoteDto>> Add(HrNoteDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrNoteDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");
            try
            {
                var CheckEmp = await _mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (CheckEmp == null) return await Result<HrNoteDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                entity.CreatedBy = session.UserId;
                entity.CreatedOn = DateTime.Now;
                entity.IsDeleted = false;
                var addItem = _mapper.Map<HrNote>(entity);
                addItem.EmpId = CheckEmp.Id;

                var newEntity = await hrRepositoryManager.HrNoteRepository.AddAndReturn(addItem);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = _mapper.Map<HrNoteDto>(newEntity);

                return await Result<HrNoteDto>.SuccessAsync(entityMap, localization.GetResource1("CreateSuccess"));
            }
            catch (Exception exc)
            {
                return await Result<HrNoteDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrNoteRepository.GetById(Id);
                if (item == null) return Result<HrNoteDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;

                hrRepositoryManager.HrNoteRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrNoteDto>.SuccessAsync(_mapper.Map<HrNoteDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrNoteDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrNoteRepository.GetById(Id);
                if (item == null) return Result<HrNoteDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;

                hrRepositoryManager.HrNoteRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrNoteDto>.SuccessAsync(_mapper.Map<HrNoteDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrNoteDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<HrNoteEditDto>> Update(HrNoteEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrNoteEditDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");
            try
            {
                // check if Emp Is Exist
                var checkEmp = await _mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (checkEmp == null) return await Result<HrNoteEditDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                var item = await hrRepositoryManager.HrNoteRepository.GetById(entity.NoteId);
                if (item == null) return await Result<HrNoteEditDto>.FailAsync($"--- there is no Data with this id: {entity.NoteId}---");
                entity.ModifiedBy = session.UserId;
                entity.ModifiedOn = DateTime.Now;
                _mapper.Map(entity, item);
                item.EmpId = checkEmp.Id;
                hrRepositoryManager.HrNoteRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrNoteEditDto>.SuccessAsync(_mapper.Map<HrNoteEditDto>(item), "Item updated successfully");

            }
            catch (Exception exc)
            {
                return await Result<HrNoteEditDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }
    }
}