using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.DTOs.Main;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Wrapper;
using Logix.Domain.ACC;
using Logix.Domain.Main;
using Logix.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Globalization;
using WhatsappBusiness.CloudApi.Webhook;

namespace Logix.Infrastructure.Repositories
{
    public class SysFileRepository : GenericRepository<SysFile>, ISysFileRepository
    {
        private readonly ApplicationDbContext context;
        private readonly ICurrentData session;
        private readonly IMapper mapper;

        public SysFileRepository(ApplicationDbContext context,
            ICurrentData session, IMapper mapper) : base(context)
        {
            this.context = context;
            this.session = session;
            this.mapper = mapper;
        }

        public async Task<IResult<List<SysFile>>> SaveFiles(List<SaveFileDto> files, long PrimaryKey, int TableId, CancellationToken cancellationToken = default)
        {
            try
            {
                List<SysFile> savedFiles = new();
                foreach (var item in files)
                {
                    // اذا كان الملف موجود في قاعدة البيانات وتم حذفه
                    if (item.IsDeleted == true && item.Id > 0)
                    {
                        var file = await context.SysFiles.FirstOrDefaultAsync(x => x.Id == item.Id);
                        if (file == null) continue;
                        file.ModifiedBy = session.UserId;
                        file.ModifiedOn = DateTime.Now;
                        file.IsDeleted = true;
                         context.SysFiles.Update(file);
                    }
                    else
                    {
                        //  اذا كان الملف جديد
                        if (item.Id == 0)
                        {
                            var newFile = new SysFile
                            {
                                CreatedBy = session.UserId,
                                CreatedOn = DateTime.Now,
                                FileUrl = item.FileURL,
                                IsDeleted = false,
                                FileName = item.FileName,
                                FileDate = DateTime.UtcNow.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture),
                                PrimaryKey = PrimaryKey,
                                TableId = TableId,
                                FacilityId = (int)session.FacilityId,
                                FileType = 0
                            };
                            savedFiles.Add(context.SysFiles.Add(newFile).Entity);

                        }
                    }
                }
                return await Result<List<SysFile>>.SuccessAsync(savedFiles, "items added successfully");
            }
            catch (Exception ex)
            {
                return await Result<List<SysFile>>.FailAsync(ex.Message);
            }
        }
        public async Task<IResult<List<SysFile>>> DeleteFiles(long PrimaryKey, int TableId, CancellationToken cancellationToken = default)
        {
            try
            {
                List<SysFile> savedFiles = new();
                var files = context.SysFiles.Where(x => x.PrimaryKey == PrimaryKey && x.TableId == TableId && x.IsDeleted == false).ToList();
                foreach (var item in files)
                {
                    if (item.Id > 0)
                    {
                        item.ModifiedBy = session.UserId;
                        item.ModifiedOn = DateTime.Now;
                        item.IsDeleted = true;
                        context.SysFiles.Update(item);
                    }
                }
                return await Result<List<SysFile>>.SuccessAsync(savedFiles, "items Deleted successfully");
            }
            catch (Exception ex)
            {
                return await Result<List<SysFile>>.FailAsync(ex.Message);
            }
        }
    }
}
