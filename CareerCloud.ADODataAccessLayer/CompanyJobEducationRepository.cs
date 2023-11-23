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
    public class CompanyJobEducationRepository : IDataRepository<CompanyJobEducationPoco>
    {
        private readonly string _connectionString;
        public CompanyJobEducationRepository()
        {
            _connectionString = Connection.GetConnectionString();
        }
        public void Add(params CompanyJobEducationPoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandType = System.Data.CommandType.Text;
                foreach (CompanyJobEducationPoco item in items)
                {
                    cmd.CommandText = "INSERT INTO Company_Job_Educations " +
                        "(Id, Job, Major, Importance)" +
                        "VALUES(@Id, @Job, @Major, @Importance)";

                    cmd.Parameters.AddWithValue("Id", item.Id);
                    cmd.Parameters.AddWithValue("Job", item.Job);
                    cmd.Parameters.AddWithValue("Major", item.Major);
                    cmd.Parameters.AddWithValue("Importance", item.Importance);

                    cmd.ExecuteNonQuery();

                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyJobEducationPoco> GetAll(params Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlDataReader reader = (new SqlCommand("SELECT * FROM Company_Job_Educations", conn).ExecuteReader());

                var list = new List<CompanyJobEducationPoco>();

                while (reader.Read())
                {
                    list.Add(new CompanyJobEducationPoco()
                    {
                        Id = (Guid)reader["Id"],
                        Job = (Guid)reader["Job"],
                        Major = (string)reader["Major"],
                        Importance = (Int16)reader["Importance"]
                    });
                }
                return list;
            }
        }

        public IList<CompanyJobEducationPoco> GetList(Expression<Func<CompanyJobEducationPoco, bool>> where, params Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobEducationPoco GetSingle(Expression<Func<CompanyJobEducationPoco, bool>> where, params Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobEducationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobEducationPoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandType = System.Data.CommandType.Text;

                foreach (CompanyJobEducationPoco item in items)
                {
                    cmd.CommandText = "DELETE FROM Company_Job_Educations WHERE Id=@Id";
                    cmd.Parameters.AddWithValue("Id", item.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(params CompanyJobEducationPoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();


                foreach (CompanyJobEducationPoco item in items)
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "UPDATE Company_Job_Educations " +
                        "SET " +
                        "Major=@Major, " +
                        "Importance=@Importance " +
                        "WHERE Id=@Id";

                    cmd.Parameters.AddWithValue("Major", item.Major);
                    cmd.Parameters.AddWithValue("Importance", item.Importance);
                    cmd.Parameters.AddWithValue("Id", item.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
