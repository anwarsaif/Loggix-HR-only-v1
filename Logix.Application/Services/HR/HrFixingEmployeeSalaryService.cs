using AutoMapper;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Vml.Office;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;
using Logix.Domain.Main;
using System.Globalization;

namespace Logix.Application.Services.HR
{
    public class HrFixingEmployeeSalaryService : GenericQueryService<HrFixingEmployeeSalary, HrFixingEmployeeSalaryDto, HrFixingEmployeeSalaryVw>, IHrFixingEmployeeSalaryService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;


        public HrFixingEmployeeSalaryService(IQueryRepository<HrFixingEmployeeSalary> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
        }

        public async Task<IResult<HrFixingEmployeeSalaryDto>> Add(HrFixingEmployeeSalaryDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrFixingEmployeeSalaryDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.empCode && x.IsDeleted == false && x.Isdel == false);
                if (checkEmpExist == null) return await Result<HrFixingEmployeeSalaryDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));

                entity.CreatedBy = session.UserId;
                entity.CreatedOn = DateTime.Now;
                entity.IsDeleted = false;
                var item = _mapper.Map<HrFixingEmployeeSalary>(entity);
                item.EmpId = (int?)checkEmpExist.Id;
                item.FacilityId = session.FacilityId;
                item.BranchId = session.BranchId;
                item.TheFixingSentTo = entity.SentTo.ToString();

                var newEntity = await hrRepositoryManager.HrFixingEmployeeSalaryRepository.AddAndReturn(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                // start of adding files
                if (entity.fileDtos.Count() >= 1)
                {
                    foreach (var singleitem in entity.fileDtos)
                    {
                        var newFile = new SysFile
                        {
                            CreatedBy = session.UserId,
                            CreatedOn = DateTime.Now,
                            FileUrl = singleitem.FileURL,
                            IsDeleted = false,
                            FileName = singleitem.FileName,
                            FileDate = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture),
                            PrimaryKey = newEntity.Id,
                            TableId = 130,
                            FileType = 0,
                            SourceFile = "",
                            FileExt = "",
                            FileDescription = "",
                            FacilityId = (int)session.FacilityId
                        };
                        await mainRepositoryManager.SysFileRepository.Add(newFile);
                        await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                    }

                }
                var entityMap = _mapper.Map<HrFixingEmployeeSalaryDto>(newEntity);
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<HrFixingEmployeeSalaryDto>.SuccessAsync(entityMap, localization.GetResource1("AddSuccess"));
            }
            catch (Exception)
            {

                return await Result<HrFixingEmployeeSalaryDto>.FailAsync(localization.GetResource1("AddError"));
            }
        }
        
        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var checkitem = await hrRepositoryManager.HrFixingEmployeeSalaryRepository.GetOneVw(x => x.IsDeleted == false && x.Id == Id);
                if (checkitem == null) return Result<HrFixingEmployeeSalaryDto>.Fail($"{localization.GetMessagesResource("NoDataWithId")} {Id}");
                if (checkitem.Status == 1) return Result<HrFixingEmployeeSalaryDto>.Fail(localization.GetMessagesResource("CannotDeleteActiveEmployeePlacement"));
                var item = await hrRepositoryManager.HrFixingEmployeeSalaryRepository.GetById(Id);
                if (item == null) return Result<HrFixingEmployeeSalaryDto>.Fail($"{localization.GetMessagesResource("NoDataWithId")} {Id}");
                item.IsDeleted = true;
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = (int)session.UserId;
                hrRepositoryManager.HrFixingEmployeeSalaryRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrFixingEmployeeSalaryDto>.SuccessAsync(_mapper.Map<HrFixingEmployeeSalaryDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrFixingEmployeeSalaryDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var checkitem = await hrRepositoryManager.HrFixingEmployeeSalaryRepository.GetOneVw(x => x.IsDeleted == false && x.Id == Id);
                if (checkitem == null) return Result<HrFixingEmployeeSalaryDto>.Fail($"{localization.GetMessagesResource("NoDataWithId")} {Id}");
                if (checkitem.Status == 1) return Result<HrFixingEmployeeSalaryDto>.Fail(localization.GetMessagesResource("CannotDeleteActiveFixingEmployeeSalaryStatus"));
                var item = await hrRepositoryManager.HrFixingEmployeeSalaryRepository.GetById(Id);
                if (item == null) return Result<HrFixingEmployeeSalaryDto>.Fail($"{localization.GetMessagesResource("NoDataWithId")} {Id}");
                item.IsDeleted = true;
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = (int)session.UserId;
                hrRepositoryManager.HrFixingEmployeeSalaryRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrFixingEmployeeSalaryDto>.SuccessAsync(_mapper.Map<HrFixingEmployeeSalaryDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrFixingEmployeeSalaryDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult<HrFixingEmployeeSalaryEditDto>> Update(HrFixingEmployeeSalaryEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrFixingEmployeeSalaryEditDto>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));
            if (string.IsNullOrEmpty(entity.empCode)) return await Result<HrFixingEmployeeSalaryEditDto>.FailAsync($"{localization.GetMessagesResource("EmployeeNumberIsRequired")}");

            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                // check if Emp Is Exist
                var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.empCode && x.IsDeleted == false && x.Isdel == false);
                if (checkEmpExist == null) return await Result<HrFixingEmployeeSalaryEditDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                var item = await hrRepositoryManager.HrFixingEmployeeSalaryRepository.GetById(entity.Id);

                if (item == null) return await Result<HrFixingEmployeeSalaryEditDto>.FailAsync(localization.GetResource1("UpdateError"));

                entity.ModifiedBy = session.UserId;
                entity.ModifiedOn = DateTime.Now;
                _mapper.Map(entity, item);
                item.EmpId = (int?)checkEmpExist.Id;
                item.TheFixingSentTo = entity.SentTo.ToString();

                hrRepositoryManager.HrFixingEmployeeSalaryRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                if (entity.fileDtos.Count() >= 1)
                {
                    foreach (var singleitem in entity.fileDtos)
                    {
                        if (singleitem.Id != 0)
                        {
                            var file = await mainRepositoryManager.SysFileRepository.GetById(singleitem.Id);
                            if (file != null)
                            {
                                file.FileUrl = singleitem.FileURL;
                                file.FileName = singleitem.FileName;
                                file.ModifiedBy = session.UserId;
                                file.ModifiedOn = DateTime.Now;
                                file.PrimaryKey = item.Id;
                                file.TableId = 130;
                                file.FileType = 0;
                                file.FileExt = "";
                                file.FileDescription = "";
                                //file.FileDate = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                                file.SourceFile = "";
                                file.ModifiedBy = session.UserId;
                                file.ModifiedOn = DateTime.Now;
                                file.FacilityId = (int)session.FacilityId;
                                file.IsDeleted = singleitem.IsDeleted;

                                mainRepositoryManager.SysFileRepository.Update(file);
                                await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                            }
                        }
                        else
                        {
                            var newFile = new SysFile
                            {
                                CreatedBy = session.UserId,
                                CreatedOn = DateTime.Now,
                                FileUrl = singleitem.FileURL,
                                IsDeleted = false,
                                FileName = singleitem.FileName,
                                FileDate = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture),
                                PrimaryKey = item.Id,
                                TableId = 130,
                                FileType = 0,
                                SourceFile = "",
                                FileExt = "",
                                FileDescription = "",
                                FacilityId = (int)session.FacilityId
                            };
                            await mainRepositoryManager.SysFileRepository.Add(newFile);
                            await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                        }
                    }
                }
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                return await Result<HrFixingEmployeeSalaryEditDto>.SuccessAsync(_mapper.Map<HrFixingEmployeeSalaryEditDto>(item), localization.GetResource1("UpdateSuccess"));
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrFixingEmployeeSalaryEditDto>.FailAsync($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }
    }
}