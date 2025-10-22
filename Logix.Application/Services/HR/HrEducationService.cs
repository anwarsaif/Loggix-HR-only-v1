using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrEducationService : GenericQueryService<HrEducation, HrEducationDto, HrEducationVw>, IHrEducationService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;


        public HrEducationService(IQueryRepository<HrEducation> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
        }

        public async Task<IResult<HrEducationDto>> Add(HrEducationDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrEducationDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
            try
            {
                var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.Id == entity.EmpId && x.IsDeleted == false && x.Isdel == false);
                if (checkEmpExist == null) return await Result<HrEducationDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                var item = _mapper.Map<HrEducation>(entity);

                item.CreatedBy = session.UserId;
                item.CreatedOn = DateTime.Now;
                item.IsDeleted = false;
                item.EmpId = checkEmpExist.Id;
                var newEntity = await hrRepositoryManager.HrEducationRepository.AddAndReturn(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = _mapper.Map<HrEducationDto>(newEntity);


                return await Result<HrEducationDto>.SuccessAsync(entityMap, localization.GetResource1("AddSuccess"));
            }
            catch (Exception)
            {

                return await Result<HrEducationDto>.FailAsync(localization.GetResource1("AddError"));
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrEducationRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrEducationDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;

                hrRepositoryManager.HrEducationRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrEducationDto>.SuccessAsync(_mapper.Map<HrEducationDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrEducationDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IResult<HrEducationEditDto>> Update(HrEducationEditDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }


    }

}