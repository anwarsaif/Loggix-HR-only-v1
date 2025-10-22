using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrDecisionsTypeService : GenericQueryService<HrDecisionsType, HrDecisionsTypeDto, HrDecisionsTypeVw>, IHrDecisionsTypeService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;


        public HrDecisionsTypeService(IQueryRepository<HrDecisionsType> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
        }

        public async Task<IResult<HrDecisionsTypeDto>> Add(HrDecisionsTypeDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrDecisionsTypeDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");
            try
            {
                entity.CreatedBy = session.UserId;
                entity.CreatedOn = DateTime.Now;
                entity.IsDeleted = false;
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                var newEntity = await hrRepositoryManager.HrDecisionsTypeRepository.AddAndReturn(_mapper.Map<HrDecisionsType>(entity));

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = _mapper.Map<HrDecisionsTypeDto>(newEntity);
                if (entity.HrDecisionsTypeEmployee != null)
                {

                    foreach (var item in entity.HrDecisionsTypeEmployee)
                    {
                        var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == item.EmpCode && x.IsDeleted == false && x.Isdel == false);
                        if (checkEmpExist == null) return await Result<HrDecisionsTypeDto>.FailAsync($"employee of Id :{item.EmpCode}{localization.GetResource1("EmployeeNotFound")}");

                        var newItem = new HrDecisionsTypeEmployee
                        {
                            CreatedBy = session.UserId,
                            CreatedOn = DateTime.Now,
                            IsDeleted = false,
                            EmpId = checkEmpExist.Id,
                            DecisionsTypeId = newEntity.Id

                        };
                        await hrRepositoryManager.HrDecisionsTypeEmployeeRepository.Add(newItem);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                    }

                }
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);


                return await Result<HrDecisionsTypeDto>.SuccessAsync(entityMap, localization.GetResource1("AddSuccess"));
            }
            catch (Exception)
            {
                return await Result<HrDecisionsTypeDto>.FailAsync(localization.GetResource1("ErrorOccurredDuring"));
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrDecisionsTypeRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrDecisionsTypeDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;

                hrRepositoryManager.HrDecisionsTypeRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrDecisionsTypeDto>.SuccessAsync(_mapper.Map<HrDecisionsTypeDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrDecisionsTypeDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrDecisionsTypeRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrDecisionsTypeDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;

                hrRepositoryManager.HrDecisionsTypeRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrDecisionsTypeDto>.SuccessAsync(_mapper.Map<HrDecisionsTypeDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrDecisionsTypeDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<HrDecisionsTypeEditDto>> Update(HrDecisionsTypeEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrDecisionsTypeEditDto>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));
            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                var item = await hrRepositoryManager.HrDecisionsTypeRepository.GetById(entity.Id);

                if (item == null) return await Result<HrDecisionsTypeEditDto>.FailAsync("the Record Is Not Found");

                entity.ModifiedBy = session.UserId;
                entity.ModifiedOn = DateTime.Now;
                _mapper.Map(entity, item);
                hrRepositoryManager.HrDecisionsTypeRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var getAllEmployees = await mainRepositoryManager.InvestEmployeeRepository.GetAll(x => x.IsDeleted == false && x.Isdel == false && x.StatusId != 2);
                var getAllDecisionsEmployees = await hrRepositoryManager.HrDecisionsTypeEmployeeRepository.GetAll(x => x.IsDeleted == false);
                foreach (var singleItem in entity.HrDecisionsTypeEmployee)
                {
                    if (singleItem.IsDeleted == true && singleItem.Id > 0)
                    {
                        var CheckIfRecordExist = getAllDecisionsEmployees.Where(x => x.Id == singleItem.Id).FirstOrDefault();
                        if (CheckIfRecordExist == null) return await Result<HrDecisionsTypeEditDto>.FailAsync($"--- تأكد من وجود البيانات سابقا ---");
                        CheckIfRecordExist.IsDeleted = true;
                        CheckIfRecordExist.ModifiedBy = session.UserId;
                        CheckIfRecordExist.ModifiedOn = DateTime.Now;
                        hrRepositoryManager.HrDecisionsTypeEmployeeRepository.Update(CheckIfRecordExist);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }

                    else if (singleItem.IsDeleted == false && singleItem.Id == 0)
                    {
                        //  بمعنى انه حقل جديد
                        var CheckIfEmpExist = getAllEmployees.Where(x => x.EmpId == singleItem.EmpCode).FirstOrDefault();
                        if (CheckIfEmpExist == null) return await Result<HrDecisionsTypeEditDto>.FailAsync($"--- لايوجد موظف بهذا الرقم: {singleItem.EmpCode}---");
                        var newEmployee = new HrDecisionsTypeEmployee
                        {
                            CreatedBy = session.UserId,
                            CreatedOn = DateTime.Now,
                            EmpId = CheckIfEmpExist.Id,
                            IsDeleted = false,
                            DecisionsTypeId = entity.Id
                        };
                        await hrRepositoryManager.HrDecisionsTypeEmployeeRepository.Add(newEmployee);

                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


                    }
                }
                
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<HrDecisionsTypeEditDto>.SuccessAsync(_mapper.Map<HrDecisionsTypeEditDto>(item), localization.GetMessagesResource("success"));
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrDecisionsTypeEditDto>.FailAsync($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
    }
    }