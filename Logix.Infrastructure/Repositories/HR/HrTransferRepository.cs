
using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Polly;
using System.Data;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrTransferRepository : GenericRepository<HrTransfer, HrTransfersVw>, IHrTransferRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IConfiguration configuration;

        public HrTransferRepository(ApplicationDbContext context, IConfiguration _configuration) : base(context)
        {
            this.context = context;
            this.configuration = _configuration;

        }

        public async Task<string> HRGetchildeDepartmentFn(long DepID)
        {
            string result = string.Empty;
            using (SqlConnection objCnn = new SqlConnection(configuration.GetConnectionString("LogixLocal")))
            {
                await objCnn.OpenAsync();

                using (var command = objCnn.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = "SELECT dbo.HR_Get_childe_Department_Fn(@Dept_ID)";
                    command.Parameters.Add(new SqlParameter("@Dept_ID", DepID));

                    var obj = await command.ExecuteScalarAsync();

                    if (obj != null)
                    {
                        result = obj.ToString();
                    }
                }
            }
            return result;

        }
    }

    }
