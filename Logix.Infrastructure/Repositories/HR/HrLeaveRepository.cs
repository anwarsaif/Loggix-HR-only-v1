using Castle.Windsor.Installer;
using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrLeaveRepository : GenericRepository<HrLeave, HrLeaveVw>, IHrLeaveRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration configuration;


        public HrLeaveRepository(ApplicationDbContext context, IConfiguration configuration) : base(context)
        {
            this._context = context;
            this.configuration = configuration;
        }
        public async Task<decimal> HR_End_Service_Due(string LeaveDate, long EmpID, int LeaveType = 0)
        {
            decimal result = 0;
            using (SqlConnection objCnn = new SqlConnection(configuration.GetConnectionString("LogixLocal")))
            {
                await objCnn.OpenAsync();

                using (var command = objCnn.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = "select dbo.HR_End_Service_Due(@Emp_ID,@Leave_Date,@Leave_Type)";
                    command.Parameters.Add(new SqlParameter("@Emp_ID", EmpID));
                    command.Parameters.Add(new SqlParameter("@Leave_Date", LeaveDate));
                    command.Parameters.Add(new SqlParameter("@Leave_Type", LeaveType));
                    var obj = await command.ExecuteScalarAsync();

                    if (obj != null)
                    {
                        result = Convert.ToDecimal(obj);
                    }
                }
            }

            return result;
        }


    }



}
