using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantEducationRepository : IDataRepository<ApplicantEducationPoco>
    {
        private readonly string _connectionString;
        public ApplicantEducationRepository()
        {
            _connectionString = Connection.GetConnectionString();
        }
        public void Add(params ApplicantEducationPoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandType = System.Data.CommandType.Text;
                foreach (ApplicantEducationPoco item in items)
                {
                    cmd.CommandText = "INSERT INTO Applicant_Educations " +
                        "(Id, Applicant, Major, Certificate_Diploma, Start_Date, " +
                        "Completion_Date, Completion_Percent)" +
                        "VALUES(@Id, @Applicant, @Major, @Certificate_Diploma," +
                        " @Start_Date, @Completion_Date, @Completion_Percent)";

                    cmd.Parameters.AddWithValue("Id", item.Id);
                    cmd.Parameters.AddWithValue("Applicant", item.Applicant);
                    cmd.Parameters.AddWithValue("Major", item.Major);
                    cmd.Parameters.AddWithValue("Certificate_Diploma", item.CertificateDiploma);
                    cmd.Parameters.AddWithValue("Start_Date", item.StartDate);
                    cmd.Parameters.AddWithValue("Completion_Date", item.CompletionDate);
                    cmd.Parameters.AddWithValue("Completion_Percent", item.CompletionPercent);

                    cmd.ExecuteNonQuery();

                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantEducationPoco> GetAll(params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlDataReader reader = (new SqlCommand("SELECT * FROM Applicant_Educations", conn).ExecuteReader());

                var list = new List<ApplicantEducationPoco>();

                while (reader.Read())
                {
                    list.Add(new ApplicantEducationPoco()
                    {
                        Id = (Guid)reader["Id"],
                        Applicant = (Guid)reader["Applicant"],
                        Major = (string)reader["Major"],
                        CertificateDiploma = (string)reader["Certificate_Diploma"],
                        StartDate = (DateTime)reader["Start_Date"],
                        CompletionDate = (DateTime)reader["Completion_Date"],
                        CompletionPercent = (Byte)reader["Completion_Percent"]
                    });
                }
                return list;
            }
        }

        public IList<ApplicantEducationPoco> GetList(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantEducationPoco GetSingle(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantEducationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantEducationPoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandType = System.Data.CommandType.Text;

                foreach (ApplicantEducationPoco item in items)
                {
                    cmd.CommandText = "DELETE FROM Applicant_Educations WHERE Id=@Id";
                    cmd.Parameters.AddWithValue("Id", item.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(params ApplicantEducationPoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();


                foreach (ApplicantEducationPoco item in items)
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "UPDATE Applicant_Educations " +
                        "SET " +
                        "Major=@Major, " +
                        "Certificate_Diploma=@Certificate_Diploma, " +
                        "Start_Date=@Start_Date, " +
                        "Completion_Date=@Completion_Date, " +
                        "Completion_Percent=@Completion_Percent " +
                        "WHERE Id=@Id";

                    cmd.Parameters.AddWithValue("Major", item.Major);
                    cmd.Parameters.AddWithValue("Certificate_Diploma", item.CertificateDiploma);
                    cmd.Parameters.AddWithValue("Start_Date", item.StartDate);
                    cmd.Parameters.AddWithValue("Completion_Date", item.CompletionDate);
                    cmd.Parameters.AddWithValue("Completion_Percent", item.CompletionPercent);
                    cmd.Parameters.AddWithValue("Id", item.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
