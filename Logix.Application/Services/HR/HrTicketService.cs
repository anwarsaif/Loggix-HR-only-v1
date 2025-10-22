using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Helpers;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrTicketService : GenericQueryService<HrTicket, HrTicketDto, HrTicketVw>, IHrTicketService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly ILocalizationService localization;
        private readonly ISysConfigurationAppHelper sysConfigurationAppHelper;


        public HrTicketService(IQueryRepository<HrTicket> queryRepository,
            IHrRepositoryManager hrRepositoryManager,
            IMainRepositoryManager mainRepositoryManager,
            IMapper mapper, ICurrentData session, ILocalizationService localization, ISysConfigurationAppHelper sysConfigurationAppHelper) : base(queryRepository, mapper)
        {
            this.hrRepositoryManager = hrRepositoryManager;
            this.mainRepositoryManager = mainRepositoryManager;
            _mapper = mapper;
            this.sysConfigurationAppHelper = sysConfigurationAppHelper;
            this.session = session;
            this.localization = localization;
        }

        public async Task<IResult<HrTicketDto>> Add(HrTicketDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrTicketDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");
            if (string.IsNullOrEmpty(entity.EmpCode)) return await Result<HrTicketDto>.FailAsync(localization.GetResource1("EmployeeIsNumber"));
            try
            {
                // check if Emp Is Exist
                var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (checkEmpExist == null) return await Result<HrTicketDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                if (checkEmpExist.StatusId == 2) return await Result<HrTicketDto>.FailAsync(localization.GetHrResource("EmpNotActive"));

                var MappedEntity = _mapper.Map<HrTicket>(entity);
                MappedEntity.EmpId = checkEmpExist.Id;
                MappedEntity.IsDeleted = false;
                MappedEntity.TotalAmount = (entity.TicketAmount * entity.TicketCount);

                var newEntity = await hrRepositoryManager.HrTicketRepository.AddAndReturn(MappedEntity);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                if (entity.fileDtos != null && entity.fileDtos.Any())
                {
                    await mainRepositoryManager.SysFileRepository.SaveFiles(entity.fileDtos, newEntity.Id, 165, cancellationToken);
                    await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                var entityMap = _mapper.Map<HrTicketDto>(newEntity);

                return await Result<HrTicketDto>.SuccessAsync(entityMap, localization.GetResource1("AddSuccess"));
            }
            catch (Exception)
            {
                return await Result<HrTicketDto>.FailAsync(localization.GetResource1("ErrorOccurredDuring"));
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrTicketRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrTicketDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.IsDeleted = true;
                hrRepositoryManager.HrTicketRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrTicketDto>.SuccessAsync(_mapper.Map<HrTicketDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrTicketDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrTicketRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrTicketDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.IsDeleted = true;
                hrRepositoryManager.HrTicketRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrTicketDto>.SuccessAsync(_mapper.Map<HrTicketDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrTicketDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<HrTicketEditDto>> Update(HrTicketEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrTicketEditDto>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));
            if (string.IsNullOrEmpty(entity.EmpCode)) return await Result<HrTicketEditDto>.FailAsync(localization.GetResource1("EmployeeIsNumber"));

            try
            {
                var item = await hrRepositoryManager.HrTicketRepository.GetById(entity.Id);

                if (item == null) return await Result<HrTicketEditDto>.FailAsync($"{localization.GetMessagesResource("NoItemFoundToEdit")}");

                // check if Emp Is Exist
                var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (checkEmpExist == null) return await Result<HrTicketEditDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                //if (checkEmpExist.StatusId == 2) return await Result<HrTicketEditDto>.FailAsync(localization.GetHrResource("EmpNotActive"));


                _mapper.Map(entity, item);

                item.EmpId = checkEmpExist.Id;
                item.TotalAmount = (entity.TicketAmount * entity.TicketCount);
                hrRepositoryManager.HrTicketRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                if (entity.fileDtos != null && entity.fileDtos.Any())
                {
                    await mainRepositoryManager.SysFileRepository.SaveFiles(entity.fileDtos, item.Id, 165, cancellationToken);
                    await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                return await Result<HrTicketEditDto>.SuccessAsync(_mapper.Map<HrTicketEditDto>(item), localization.GetMessagesResource("success"));
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrTicketEditDto>.FailAsync($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
    }
}