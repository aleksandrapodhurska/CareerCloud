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
    public class ApplicantJobApplicationRepository : IDataRepository<ApplicantJobApplicationPoco>
    {
        private readonly string _connectionString;
        public ApplicantJobApplicationRepository()
        {
            _connectionString = Connection.GetConnectionString();
        }
        public void Add(params ApplicantJobApplicationPoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                foreach (ApplicantJobApplicationPoco item in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "INSERT INTO Applicant_Job_Applications " +
                        "(Id, Applicant, Job, Application_Date)" +
                        "VALUES(@Id, @Applicant, @Job, @Application_Date)";

                    cmd.Parameters.AddWithValue("Id", item.Id);
                    cmd.Parameters.AddWithValue("Applicant", item.Applicant);
                    cmd.Parameters.AddWithValue("Job", item.Job);
                    cmd.Parameters.AddWithValue("Application_Date", item.ApplicationDate);


                    conn.Open();
                    cmd.ExecuteNonQuery();

                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantJobApplicationPoco> GetAll(params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlDataReader reader = (new SqlCommand("SELECT * FROM Applicant_Job_Applications", conn).ExecuteReader());

                var list = new List<ApplicantJobApplicationPoco>();

                while (reader.Read())
                {
                    list.Add(new ApplicantJobApplicationPoco()
                    {
                        Id = (Guid)reader["Id"],
                        Applicant = (Guid)reader["Applicant"],
                        Job = (Guid)reader["Job"],
                        ApplicationDate = (DateTime)reader["Application_Date"]
                    });
                }
                return list;
            }
        }

        public IList<ApplicantJobApplicationPoco> GetList(Expression<Func<ApplicantJobApplicationPoco, bool>> where, params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantJobApplicationPoco GetSingle(Expression<Func<ApplicantJobApplicationPoco, bool>> where, params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantJobApplicationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantJobApplicationPoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandType = System.Data.CommandType.Text;

                foreach (ApplicantJobApplicationPoco item in items)
                {
                    cmd.CommandText = "DELETE FROM Applicant_Job_Applications " +
                                        "WHERE Id=@Id";
                    cmd.Parameters.AddWithValue("Id", item.Id);
                    cmd.ExecuteNonQuery();
                }

            }
        }

        public void Update(params ApplicantJobApplicationPoco[] items)
        {
            using(var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();

                foreach (ApplicantJobApplicationPoco item in items)
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "UPDATE Applicant_Job_Applications " +
                        "SET " +
                        "Application_Date=@Application_Date " +
                        "WHERE Id=@Id";

                    cmd.Parameters.AddWithValue("Application_Date", item.ApplicationDate);
                    cmd.Parameters.AddWithValue("Id", item.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
