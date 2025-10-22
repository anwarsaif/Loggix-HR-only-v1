using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrVisaService : GenericQueryService<HrVisa, HrVisaDto, HrVisaVw>, IHrVisaService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;


        public HrVisaService(IQueryRepository<HrVisa> queryRepository,
            IMainRepositoryManager mainRepositoryManager,
            IMapper mapper,
            IHrRepositoryManager hrRepositoryManager,
            ILocalizationService localization) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
        }

        public async Task<IResult<HrVisaDto>> Add(HrVisaDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrVisaDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");
            if (string.IsNullOrEmpty(entity.EmpCode)) return await Result<HrVisaDto>.FailAsync($"Employee Id Is Required");
            try
            {
                // check if Emp Is Exist
                var CheckEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (CheckEmpExist == null) return await Result<HrVisaDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                if (CheckEmpExist.StatusId == 2) return await Result<HrVisaDto>.FailAsync(localization.GetHrResource("EmpNotActive"));

                entity.IsDeleted = false;
                var newItem = _mapper.Map<HrVisa>(entity);
                newItem.EmpId = CheckEmpExist.Id;

                var newEntity = await hrRepositoryManager.HrVisaRepository.AddAndReturn(newItem);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                if (entity.fileDtos != null && entity.fileDtos.Any())
                {
                    await mainRepositoryManager.SysFileRepository.SaveFiles(entity.fileDtos, newEntity.Id, 164, cancellationToken);
                    await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                return await Result<HrVisaDto>.SuccessAsync(_mapper.Map<HrVisaDto>(newEntity), localization.GetResource1("AddSuccess"));
            }
            catch (Exception)
            {
                return await Result<HrVisaDto>.FailAsync(localization.GetResource1("ErrorOccurredDuring"));
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrVisaRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrVisaDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.IsDeleted = true;
                hrRepositoryManager.HrVisaRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrVisaDto>.SuccessAsync(_mapper.Map<HrVisaDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrVisaDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrVisaRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrVisaDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.IsDeleted = true;
                hrRepositoryManager.HrVisaRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrVisaDto>.SuccessAsync(_mapper.Map<HrVisaDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrVisaDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<HrVisaEditDto>> Update(HrVisaEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrVisaEditDto>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));
            if (string.IsNullOrEmpty(entity.EmpCode)) return await Result<HrVisaEditDto>.FailAsync($"Employee Id Is Required");

            try
            {
                // check if Emp Is Exist
                var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (checkEmpExist == null) return await Result<HrVisaEditDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                //if (checkEmpExist.StatusId == 2) return await Result<HrVisaEditDto>.FailAsync(localization.GetHrResource("EmpNotActive"));
                var item = await hrRepositoryManager.HrVisaRepository.GetById(entity.Id);
                if (item == null) return await Result<HrVisaEditDto>.FailAsync("the Record Is Not Found");

                _mapper.Map(entity, item);
                item.EmpId = checkEmpExist.Id;

                hrRepositoryManager.HrVisaRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                if (entity.fileDtos != null && entity.fileDtos.Any())
                {
                    await mainRepositoryManager.SysFileRepository.SaveFiles(entity.fileDtos, item.Id, 164, cancellationToken);
                    await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                return await Result<HrVisaEditDto>.SuccessAsync(_mapper.Map<HrVisaEditDto>(item), localization.GetMessagesResource("success"));
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrVisaEditDto>.FailAsync($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
    }
}