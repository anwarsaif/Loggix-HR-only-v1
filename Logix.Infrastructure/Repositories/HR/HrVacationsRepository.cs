using System.Data;
using System.Linq.Expressions;
using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrVacationsRepository : GenericRepository<HrVacation, HrVacationsVw>, IHrVacationsRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IConfiguration configuration;

        public HrVacationsRepository(ApplicationDbContext context, IConfiguration _configuration) : base(context)
        {
            this.context = context;
            this.configuration = _configuration;
        }

        public async Task<decimal> Vacation_Balance2_FN(string Curr_Date, long ID_Emp, int VacationTypeId)
        {
            decimal result = 0;
            using (SqlConnection objCnn = new SqlConnection(configuration.GetConnectionString("LogixLocal")))
            {
                await objCnn.OpenAsync();

                using (var command = objCnn.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = "select dbo.HR_Vacation2_Balance_FN(@Curr_Date,@ID_Emp,@Vacation_Type_Id)";
                    command.Parameters.Add(new SqlParameter("@Curr_Date", Curr_Date));
                    command.Parameters.Add(new SqlParameter("@ID_Emp", ID_Emp));
                    command.Parameters.Add(new SqlParameter("@Vacation_Type_Id", VacationTypeId));
                    var obj = await command.ExecuteScalarAsync();

                    if (obj != null)
                    {
                        result = Convert.ToDecimal(obj);
                    }
                }
            }

            return result;
        }
        public async Task<decimal> Vacation_Balance_FN(string Curr_Date, long ID_Emp)
        {
            decimal result = 0;

            using (SqlConnection objCnn = new SqlConnection(configuration.GetConnectionString("LogixLocal")))
            {
                await objCnn.OpenAsync();

                using (var command = objCnn.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = "SELECT dbo.HR_Vacation_Balance_FN(@Curr_Date,@ID_Emp)";
                    command.Parameters.Add(new SqlParameter("@Curr_Date", Curr_Date));
                    command.Parameters.Add(new SqlParameter("@ID_Emp", ID_Emp));
                    var obj = await command.ExecuteScalarAsync();

                    if (obj != null)
                    {
                        result = Convert.ToDecimal(obj);
                    }
                }
            }

            return result;
        }
        public async Task<IEnumerable<HrVacationsVw>> GetAllFromView(Expression<Func<HrVacationsVw, bool>> expression)
        {
            try
            {
                return await context.Set<HrVacationsVw>().Where(expression).AsNoTracking().ToListAsync();
            }
            catch (Exception)
            {
                return new List<HrVacationsVw>();
            }
        }


        public async Task<long> Check_Have_Vacation_Type_Id(int? Vacation_Type_Id)
        {
            try
            {

                var count = await context.HrVacations
                .Where(a => a.VacationTypeId == Vacation_Type_Id && a.IsDeleted == false)
                .CountAsync();

                return count;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<decimal> GetCountDays(string StartDate, string EndDate, int VacationTypeId)
        {
            try
            {
                decimal result = 0;
                using (SqlConnection objCnn = new SqlConnection(configuration.GetConnectionString("LogixLocal")))
                {
                    await objCnn.OpenAsync();

                    using (var command = objCnn.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "HR_VacationRequest_SP";
                        command.Parameters.Add(new SqlParameter("@CMDTYPE", 6));
                        command.Parameters.Add(new SqlParameter("@Vacation_Type_Id", VacationTypeId));
                        command.Parameters.Add(new SqlParameter("@SDate", StartDate));
                        command.Parameters.Add(new SqlParameter("@EDate", EndDate));
                        var obj = await command.ExecuteScalarAsync();

                        if (obj != null)
                        {
                            result = Convert.ToDecimal(obj);
                        }
                    }
                }

                return result;
            }
            catch
            {
                return 0;
            }
        }
    }
}
