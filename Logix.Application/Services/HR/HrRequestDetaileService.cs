using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrRequestDetaileService : GenericQueryService<HrRequestDetaile, HrRequestDetailsDto, HrRequestDetailesVw>, IHrRequestDetailsService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly ILocalizationService localization;


        public HrRequestDetaileService(IQueryRepository<HrRequestDetaile> queryRepository, IHrRepositoryManager hrRepositoryManager, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this.hrRepositoryManager = hrRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.localization = localization;
        }

        public async Task<IResult<HrRequestDetailsDto>> Add(HrRequestDetailsDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrRequestDetailsDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");

            try
            {
                if (entity.RequestType == 1 || entity.RequestType == 2 || entity.RequestType == 3)
                {
                    if (entity.Value == 0) return await Result<HrRequestDetailsDto>.FailAsync("يجب تحديد القيمة");
                    if (entity.type == 0) return await Result<HrRequestDetailsDto>.FailAsync("يجب تحديد النوع");
                }
                if (entity.RequestType == 6)
                {
                    if (entity.type == 0) return await Result<HrRequestDetailsDto>.FailAsync("يجب تحديد النوع");
                }
                var mappedEntity = _mapper.Map<HrRequestDetaile>(entity);
                var investEmployees = await mainRepositoryManager.InvestEmployeeRepository.GetOne(e => e.EmpId == entity.empCode && e.IsDeleted == false);
                if (investEmployees == null)
                {
                    return await Result<HrRequestDetailsDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                }
                mappedEntity.EmpId = investEmployees.Id;

                if (entity.RequestType == 1)
                {
                    mappedEntity.AllownceId = entity.type;
                    mappedEntity.DeductionId = 0;
                    mappedEntity.OverTimeId = 0;
                    mappedEntity.AbsenceTypeId = 0;
                }
                else if (entity.RequestType == 2)
                {
                    mappedEntity.AllownceId = 0;
                    mappedEntity.DeductionId = entity.type;
                    mappedEntity.OverTimeId = 0;
                    mappedEntity.AbsenceTypeId = 0;
                }
                else if (entity.RequestType == 3)
                {
                    mappedEntity.AllownceId = 0;
                    mappedEntity.DeductionId = 0;
                    mappedEntity.OverTimeId = entity.type;
                    mappedEntity.AbsenceTypeId = 0;
                }
                else if (entity.RequestType == 6)
                {
                    mappedEntity.AllownceId = 0;
                    mappedEntity.DeductionId = 0;
                    mappedEntity.OverTimeId = 0;
                    mappedEntity.AbsenceTypeId = entity.type;
                }
                else
                {
                    mappedEntity.AllownceId = 0;
                    mappedEntity.DeductionId = 0;
                    mappedEntity.OverTimeId = 0;
                    mappedEntity.AbsenceTypeId = 0;
                }
                


                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);




                var newEntity = await hrRepositoryManager.HrRequestDetaileRepository.AddAndReturn(mappedEntity);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                //////////////////////////////////////////////////////////////////
                var entityMap = _mapper.Map<HrRequestDetailsDto>(newEntity);

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<HrRequestDetailsDto>.SuccessAsync(entityMap, "item added successfully");
            }
            catch (Exception exc)
            {
                return await Result<HrRequestDetailsDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrRequestDetaileRepository.GetById(Id);
                if (item == null) return Result<HrRequestDetailsDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;

                hrRepositoryManager.HrRequestDetaileRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrRequestDetailsDto>.SuccessAsync(_mapper.Map<HrRequestDetailsDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrRequestDetailsDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IResult<HrRequestDetailsEditDto>> Update(HrRequestDetailsEditDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}