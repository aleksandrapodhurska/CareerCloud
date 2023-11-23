using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantWorkHistoryRepository : IDataRepository<ApplicantWorkHistoryPoco>
    {
        private readonly string _connectionString;
        public ApplicantWorkHistoryRepository()
        {
            _connectionString = Connection.GetConnectionString();
        }
        public void Add(params ApplicantWorkHistoryPoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandType = System.Data.CommandType.Text;

                foreach (ApplicantWorkHistoryPoco item in items)
                {
                    cmd.CommandText = "INSERT INTO Applicant_Work_History(Id, Applicant, Company_Name, Country_Code, Location, Job_Title, Job_Description, Start_Month, Start_Year, End_Month, End_Year) " +
                        "VALUES(@Id, @Applicant, @Company_Name, @Country_Code, @Location, @Job_Title, @Job_Description, @Start_Month, @Start_Year, @End_Month, @End_Year)";

                    cmd.Parameters.AddWithValue("Id", item.Id);
                    cmd.Parameters.AddWithValue("Applicant", item.Applicant);
                    cmd.Parameters.AddWithValue("Company_Name", item.CompanyName);
                    cmd.Parameters.AddWithValue("Country_Code", item.CountryCode);
                    cmd.Parameters.AddWithValue("Location", item.Location);
                    cmd.Parameters.AddWithValue("Job_Title", item.JobTitle);
                    cmd.Parameters.AddWithValue("Job_Description", item.JobDescription);
                    cmd.Parameters.AddWithValue("Start_Month", item.StartMonth);
                    cmd.Parameters.AddWithValue("Start_Year", item.StartYear);
                    cmd.Parameters.AddWithValue("End_Month", item.EndMonth);
                    cmd.Parameters.AddWithValue("End_Year", item.EndYear);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantWorkHistoryPoco> GetAll(params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlDataReader reader = (new SqlCommand("SELECT * FROM Applicant_Work_History", conn).ExecuteReader());

                var list = new List<ApplicantWorkHistoryPoco>();

                while (reader.Read())
                {
                    list.Add(new ApplicantWorkHistoryPoco()
                    {
                        Id = (Guid)reader["Id"],
                        Applicant = (Guid)reader["Applicant"],
                        CompanyName = (string)reader["Company_Name"],
                        CountryCode = (string)reader["Country_Code"],
                        Location = (string)reader["Location"],
                        JobTitle = (string)reader["Job_Title"],
                        JobDescription = (string)reader["Job_Description"],
                        StartMonth = (short)reader["Start_Month"],
                        StartYear = (int)reader["Start_Year"],
                        EndMonth = (short)reader["End_Month"],
                        EndYear = (int)reader["End_Year"]
                    });
                }
                return list;
            }
        }

        public IList<ApplicantWorkHistoryPoco> GetList(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantWorkHistoryPoco GetSingle(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantWorkHistoryPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantWorkHistoryPoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandType = System.Data.CommandType.Text;

                foreach (ApplicantWorkHistoryPoco item in items)
                {
                    cmd.CommandText = "DELETE FROM Applicant_Work_History WHERE Id=@Id";
                    cmd.Parameters.AddWithValue("Id", item.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(params ApplicantWorkHistoryPoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandType = System.Data.CommandType.Text;

                foreach (ApplicantWorkHistoryPoco item in items)
                {
                    cmd.CommandText = "UPDATE Applicant_Work_History " +
                        "SET " +
                        "Company_Name=@Company_Name, " +
                        "Location=@Location, " +
                        "Job_Title=@Job_Title, " +
                        "Job_Description=@Job_Description, " +
                        "Start_Month=@Start_Month, " +
                        "Start_Year=@Start_Year, " +
                        "End_Month=@End_Month, " +
                        "End_Year=@End_Year " +
                        "WHERE Id=@Id";

                    cmd.Parameters.AddWithValue("Company_Name", item.CompanyName);
                    cmd.Parameters.AddWithValue("Country_Code", item.CountryCode);                    
                    cmd.Parameters.AddWithValue("Location", item.Location);
                    cmd.Parameters.AddWithValue("Job_Title", item.JobTitle);
                    cmd.Parameters.AddWithValue("Job_Description", item.JobDescription);
                    cmd.Parameters.AddWithValue("Start_Month", item.StartMonth);
                    cmd.Parameters.AddWithValue("Start_Year", item.StartYear);
                    cmd.Parameters.AddWithValue("End_Month", item.EndMonth);
                    cmd.Parameters.AddWithValue("End_Year", item.EndYear);
                    cmd.Parameters.AddWithValue("Id", item.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
