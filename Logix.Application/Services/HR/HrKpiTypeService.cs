using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrKpiTypeService : GenericQueryService<HrKpiType, HrKpiTypeDto, HrKpiType>, IHrKpiTypeService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;

        public HrKpiTypeService(IQueryRepository<HrKpiType> queryRepository,
            IHrRepositoryManager hrRepositoryManager,
            IMainRepositoryManager mainRepositoryManager,
            IMapper mapper, ICurrentData session) : base(queryRepository, mapper)
        {
            this.hrRepositoryManager = hrRepositoryManager;
            this.mainRepositoryManager = mainRepositoryManager;
            _mapper = mapper;

            this.session = session;
        }

        public async Task<IResult<HrKpiTypeDto>> Add(HrKpiTypeDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrKpiTypeDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

            try
            {

                entity.Isdeleted = false;

                var item = _mapper.Map<HrKpiType>(entity);
                var newEntity = await hrRepositoryManager.HrKpiTypeRepository.AddAndReturn(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = _mapper.Map<HrKpiTypeDto>(newEntity);


                return await Result<HrKpiTypeDto>.SuccessAsync(entityMap, "item added successfully");
            }
            catch (Exception exc)
            {

                return await Result<HrKpiTypeDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }

        }



        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            var item = await hrRepositoryManager.HrKpiTypeRepository.GetById(Id);
            if (item == null) return Result<HrKpiTypeDto>.Fail($"--- there is no Data with this id: {Id}---");
            item.Isdeleted = true;

            hrRepositoryManager.HrKpiTypeRepository.Update(item);
            try
            {
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrKpiTypeDto>.SuccessAsync(_mapper.Map<HrKpiTypeDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<HrKpiTypeDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            var item = await hrRepositoryManager.HrKpiTypeRepository.GetById(Id);
            if (item == null) return Result<HrKpiTypeDto>.Fail($"--- there is no Data with this id: {Id}---");
            item.Isdeleted = true;
          
            hrRepositoryManager.HrKpiTypeRepository.Update(item);
            try
            {
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrKpiTypeDto>.SuccessAsync(_mapper.Map<HrKpiTypeDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<HrKpiTypeDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<HrKpiTypeEditDto>> Update(HrKpiTypeEditDto entity, CancellationToken cancellationToken = default)
        {

            if (entity == null) return await Result<HrKpiTypeEditDto>.FailAsync($"Error in {GetType()} : the passed entity IS NULL.");

            try
            {
                var item = await hrRepositoryManager.HrKpiTypeRepository.GetById(entity.Id);

                if (item == null) return await Result<HrKpiTypeEditDto>.FailAsync($"--- there is no Data with this id: {entity.Id}---");
                _mapper.Map(entity, item);

                hrRepositoryManager.HrKpiTypeRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrKpiTypeEditDto>.SuccessAsync(_mapper.Map<HrKpiTypeEditDto>(item), "Item updated successfully");
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrKpiTypeEditDto>.FailAsync($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
    }

}