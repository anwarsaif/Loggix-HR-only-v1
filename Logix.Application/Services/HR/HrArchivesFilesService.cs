using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrArchivesFilesService : GenericQueryService<HrArchivesFile, HrArchivesFileDto, HrArchivesFilesVw>, IHrArchivesFilesService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;

        private readonly IMainRepositoryManager _mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly ILocalizationService localization;


        public HrArchivesFilesService(IQueryRepository<HrArchivesFile> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this._mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
        }

        public async Task<IResult<HrArchivesFileDto>> Add(HrArchivesFileDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrArchivesFileDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                var checkEmp = await _mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (checkEmp == null) return await Result<HrArchivesFileDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));

                entity.CreatedBy = session.UserId;
                entity.CreatedOn = DateTime.Now;
                entity.Qty = 1;
                entity.EmpTypeId = 1;
                var item = _mapper.Map<HrArchivesFile>(entity);
                item.EmpId = checkEmp.Id;
                item.IsDeleted = false;
                item.ArchiveDate = DateTime.Now.ToString();

                var newEntity = await hrRepositoryManager.HrArchivesFileRepository.AddAndReturn(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                var FileDetails = new HrArchiveFilesDetail
                {
                    ArchiveId = newEntity.ArchiveFileId,
                    EmpId = checkEmp.Id,
                    CreatedBy = session.UserId,
                    CreatedOn = DateTime.Now,
                    ShowEmp = entity.ShowEmp,
                    Url = entity.Url,
                    Note = entity.DocName,
                    FileTypeId = entity.FileTypeId,

                };
                var newDetailsEntity = await hrRepositoryManager.HrArchiveFilesDetailRepository.AddAndReturn(FileDetails);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                var entityMap = _mapper.Map<HrArchivesFileDto>(newEntity);


                return await Result<HrArchivesFileDto>.SuccessAsync(entityMap, localization.GetMessagesResource("AddSuccess"));
            }
            catch (Exception exc)
            {

                return await Result<HrArchivesFileDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }


        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            var item = await hrRepositoryManager.HrArchivesFileRepository.GetById(Id);
            if (item == null) return Result<HrArchivesFileDto>.Fail($"--- there is no Data with this id: {Id}---");
            item.IsDeleted = true;
            item.ModifiedOn = DateTime.Now;
            item.ModifiedBy = (int)session.UserId;
            hrRepositoryManager.HrArchivesFileRepository.Update(item);
            try
            {
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrArchivesFileDto>.SuccessAsync(_mapper.Map<HrArchivesFileDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<HrArchivesFileDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            var item = await hrRepositoryManager.HrArchivesFileRepository.GetById(Id);
            if (item == null) return Result<HrArchivesFileDto>.Fail($"--- there is no Data with this id: {Id}---");
            item.IsDeleted = true;
            item.ModifiedOn = DateTime.Now;
            item.ModifiedBy = (int)session.UserId;
            hrRepositoryManager.HrArchivesFileRepository.Update(item);
            try
            {
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrArchivesFileDto>.SuccessAsync(_mapper.Map<HrArchivesFileDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<HrArchivesFileDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<HrArchivesFileEditDto>> Update(HrArchivesFileEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrArchivesFileEditDto>.FailAsync($"Error in {GetType()} : the passed entity IS NULL.");

            try
            {
                var checkEmp = await _mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (checkEmp == null) return await Result<HrArchivesFileEditDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));

                var item = await hrRepositoryManager.HrArchivesFileRepository.GetOne(i => i.ArchiveFileId == entity.ArchiveFileId);

                if (item == null) return await Result<HrArchivesFileEditDto>.FailAsync($"--- there is no Data with this id: {entity.ArchiveFileId}---");
                var updateitem = _mapper.Map<HrArchivesFile>(entity);

                updateitem.ModifiedOn = DateTime.Now;
                updateitem.ModifiedBy = (int)session.UserId;
                updateitem.EmpId = checkEmp.Id;
                updateitem.IsDeleted = false;

                updateitem.Qty = 1;
                updateitem.EmpTypeId = 1;
                updateitem.FileTypeId = "0";
                hrRepositoryManager.HrArchivesFileRepository.Update(updateitem);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrArchivesFileEditDto>.SuccessAsync(_mapper.Map<HrArchivesFileEditDto>(item), "Item updated successfully");
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrArchivesFileEditDto>.FailAsync($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
    }

}
