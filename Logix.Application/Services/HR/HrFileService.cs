using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrFileService : GenericQueryService<HrFile, HrFileDto, HrFile>, IHrFileService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;


        public HrFileService(IQueryRepository<HrFile> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
        }

        public async Task<IResult<HrFileDto>> Add(HrFileDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrFileDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
            try
            {
                var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.Id == entity.EmpId && x.IsDeleted == false && x.Isdel == false);
                if (checkEmpExist == null) return await Result<HrFileDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                var item = _mapper.Map<HrFile>(entity);

                item.CreatedBy = session.UserId;
                item.CreatedOn = DateTime.Now;
                item.IsDeleted = false;
                item.EmpId = checkEmpExist.Id;
                var newEntity = await hrRepositoryManager.HrFileRepository.AddAndReturn(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                var entityMap = _mapper.Map<HrFileDto>(newEntity);
                return await Result<HrFileDto>.SuccessAsync(entityMap, localization.GetResource1("AddSuccess"));
            }
            catch (Exception)
            {

                return await Result<HrFileDto>.FailAsync(localization.GetResource1("AddError"));
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrFileRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrFileDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;

                hrRepositoryManager.HrFileRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrFileDto>.SuccessAsync(_mapper.Map<HrFileDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrFileDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrFileRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrFileDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;
                hrRepositoryManager.HrFileRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrFileDto>.SuccessAsync(_mapper.Map<HrFileDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrFileDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public Task<IResult<HrFileEditDto>> Update(HrFileEditDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

    }

}