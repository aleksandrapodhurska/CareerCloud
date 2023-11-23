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
    public class CompanyJobDescriptionRepository : IDataRepository<CompanyJobDescriptionPoco>
    {
        private readonly string _connectionString;
        public CompanyJobDescriptionRepository()
        {
            _connectionString = Connection.GetConnectionString();
        }
        public void Add(params CompanyJobDescriptionPoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandType = System.Data.CommandType.Text;

                foreach (CompanyJobDescriptionPoco item in items)
                {
                    cmd.CommandText = "INSERT INTO Company_Jobs_Descriptions(Id, Job, Job_Name, Job_Descriptions) " +
                        "VALUES(@Id, @Job, @Job_Name, @Job_Descriptions)";

                    cmd.Parameters.AddWithValue("Id", item.Id);
                    cmd.Parameters.AddWithValue("Job", item.Job);
                    cmd.Parameters.AddWithValue("Job_Name", item.JobName);
                    cmd.Parameters.AddWithValue("Job_Descriptions", item.JobDescriptions);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyJobDescriptionPoco> GetAll(params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlDataReader reader = (new SqlCommand("SELECT * FROM Company_Jobs_Descriptions", conn).ExecuteReader());

                var list = new List<CompanyJobDescriptionPoco>();

                while (reader.Read())
                {
                    list.Add(new CompanyJobDescriptionPoco()
                    {
                        Id = (Guid)reader["Id"],
                        Job = (Guid)reader["Job"],
                        JobName = (string)reader["Job_Name"],
                        JobDescriptions = (string)reader["Job_Descriptions"]
                    });
                }
                return list;
            }
        }

        public IList<CompanyJobDescriptionPoco> GetList(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobDescriptionPoco GetSingle(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobDescriptionPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobDescriptionPoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandType = System.Data.CommandType.Text;

                foreach (CompanyJobDescriptionPoco item in items)
                {
                    cmd.CommandText = "DELETE FROM Company_Jobs_Descriptions WHERE Id=@Id";
                    cmd.Parameters.AddWithValue("Id", item.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(params CompanyJobDescriptionPoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandType = System.Data.CommandType.Text;

                foreach (CompanyJobDescriptionPoco item in items)
                {
                    cmd.CommandText = "UPDATE Company_Jobs_Descriptions " +
                        "SET " +
                        "Job_Name=@Job_Name, " +
                        "Job_Descriptions=@Job_Descriptions " +
                        "WHERE Id=@Id";

                    cmd.Parameters.AddWithValue("Job_Name", item.JobName);
                    cmd.Parameters.AddWithValue("Job_Descriptions", item.JobDescriptions);
                    cmd.Parameters.AddWithValue("Id", item.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
