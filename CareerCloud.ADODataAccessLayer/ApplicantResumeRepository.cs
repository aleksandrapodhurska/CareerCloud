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
    public class ApplicantResumeRepository : IDataRepository<ApplicantResumePoco>
    {
        private readonly string _connectionString;
        public ApplicantResumeRepository()
        {
            _connectionString = Connection.GetConnectionString();
        }
        public void Add(params ApplicantResumePoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandType = System.Data.CommandType.Text;

                foreach (ApplicantResumePoco item in items)
                {
                    cmd.CommandText = "INSERT INTO Applicant_Resumes(Id, Applicant, Resume, Last_Updated) " +
                        "VALUES(@Id, @Applicant, @Resume, @Last_Updated)";

                    cmd.Parameters.AddWithValue("Id", item.Id);
                    cmd.Parameters.AddWithValue("Applicant", item.Applicant);
                    cmd.Parameters.AddWithValue("Resume", item.Resume);
                    cmd.Parameters.AddWithValue("Last_Updated", item.LastUpdated);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantResumePoco> GetAll(params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlDataReader reader = (new SqlCommand("SELECT * FROM Applicant_Resumes", conn).ExecuteReader());

                var list = new List<ApplicantResumePoco>();

                while (reader.Read())
                {
                    list.Add(new ApplicantResumePoco()
                    {
                        Id = (Guid)reader["Id"],
                        Applicant = (Guid)reader["Applicant"],
                        Resume = (string)reader["Resume"],
                        LastUpdated = Convert.IsDBNull(reader["Last_Updated"]) ? null : (DateTime)reader["Last_Updated"]
                    });
                }

                return list;
            }
        }

        public IList<ApplicantResumePoco> GetList(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantResumePoco GetSingle(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantResumePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantResumePoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandType = System.Data.CommandType.Text;

                foreach (ApplicantResumePoco item in items)
                {
                    cmd.CommandText = "DELETE FROM Applicant_Resumes WHERE Id=@Id";
                    cmd.Parameters.AddWithValue("Id", item.Id);

                    cmd.ExecuteNonQuery();
                }
            }

        }

        public void Update(params ApplicantResumePoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();

                foreach (ApplicantResumePoco item in items)
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "UPDATE Applicant_Resumes " +
                        "SET " +
                        "Resume=@Resume, " +
                        "Last_Updated=@Last_Updated " +
                        "WHERE Id=@Id";

                    cmd.Parameters.AddWithValue("Resume", item.Resume);
                    cmd.Parameters.AddWithValue("Last_Updated", item.LastUpdated);
                    cmd.Parameters.AddWithValue("Id", item.Id);

                    cmd.ExecuteNonQuery();
                }

            }
        }
    }
}
