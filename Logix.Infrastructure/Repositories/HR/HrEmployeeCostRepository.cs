using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Dynamic;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrEmployeeCostRepository : GenericRepository<HrEmployeeCost, HrEmployeeCostVw>, IHrEmployeeCostRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IConfiguration configuration;

        public HrEmployeeCostRepository(ApplicationDbContext context, IConfiguration configuration) : base(context)
        {
            this.configuration = configuration;
            this.context = context;
        }
        public async Task<IEnumerable<dynamic>> GetRPEmployeeContract(string EmpCode, string EmpName, string nationalityId, string DeptId, string Location, int LanguageType, string BranchID, string? BranchsID, string StatusID)
        {
            List<dynamic> result = new List<dynamic>();

            try
            {
                using (SqlConnection objCnn = new SqlConnection(configuration.GetConnectionString("LogixLocal")))
                {
                    await objCnn.OpenAsync();

                    using (SqlCommand command = new SqlCommand("HR_Employee_Contract_RP_SP", objCnn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@Emp_Code", EmpCode));
                        command.Parameters.Add(new SqlParameter("@Emp_name", EmpName));
                        command.Parameters.Add(new SqlParameter("@Location", Location));
                        command.Parameters.Add(new SqlParameter("@Dept_ID", DeptId));
                        command.Parameters.Add(new SqlParameter("@Nationality_ID", nationalityId));
                        command.Parameters.Add(new SqlParameter("@Branch_ID", BranchID));
                        command.Parameters.Add(new SqlParameter("@Branchs_ID", BranchsID));
                        command.Parameters.Add(new SqlParameter("@Status_ID", StatusID));
                        command.Parameters.Add(new SqlParameter("@Language_Type", LanguageType));
                        command.CommandTimeout = 300;
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            DataTable dataTable = new DataTable();
                            dataTable.Load(reader);

                            foreach (DataRow row in dataTable.Rows)
                            {
                                dynamic reportDto = new ExpandoObject();
                                var reportDictionary = (IDictionary<string, object>)reportDto;

                                foreach (DataColumn column in dataTable.Columns)
                                {
                                    reportDictionary[column.ColumnName] = row[column] != DBNull.Value ? row[column] : "";
                                }

                                result.Add(reportDto);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }

            return result;
        }

        public async Task<IEnumerable<dynamic>> GetRPEmployeeCost(string EmpCode, string EmpName, string nationalityId, string StartDate, string EndDate, string DeptId, string Location, int LanguageType)
        {
            List<dynamic> result = new List<dynamic>();

            try
            {
                using (SqlConnection objCnn = new SqlConnection(configuration.GetConnectionString("LogixLocal")))
                {
                    await objCnn.OpenAsync();


                    using (SqlCommand command = new SqlCommand("HR_Employee_Cost_RP_SP", objCnn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@Emp_Code", EmpCode));
                        command.Parameters.Add(new SqlParameter("@Emp_name", EmpName));
                        command.Parameters.Add(new SqlParameter("@Location", Location));
                        command.Parameters.Add(new SqlParameter("@Dept_ID", DeptId));
                        command.Parameters.Add(new SqlParameter("@Nationality_ID", nationalityId));
                        command.Parameters.Add(new SqlParameter("@Start_date", StartDate));
                        command.Parameters.Add(new SqlParameter("@End_date", EndDate));
                        command.Parameters.Add(new SqlParameter("@Language_Type", LanguageType));

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            DataTable dataTable = new DataTable();
                            dataTable.Load(reader);

                            foreach (DataRow row in dataTable.Rows)
                            {
                                dynamic reportDto = new ExpandoObject();
                                var reportDictionary = (IDictionary<string, object>)reportDto;

                                foreach (DataColumn column in dataTable.Columns)
                                {
                                    reportDictionary[column.ColumnName] = row[column] != DBNull.Value ? row[column] : "";
                                }

                                result.Add(reportDto);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }

            return result;
        }




    }
}
