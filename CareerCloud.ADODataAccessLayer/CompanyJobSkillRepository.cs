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
    public class CompanyJobSkillRepository : IDataRepository<CompanyJobSkillPoco>
    {
        private readonly string _connectionString;
        public CompanyJobSkillRepository()
        {
            _connectionString = Connection.GetConnectionString();
        }
        public void Add(params CompanyJobSkillPoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandType = System.Data.CommandType.Text;
                foreach (CompanyJobSkillPoco item in items)
                {
                    cmd.CommandText = "INSERT INTO Company_Job_Skills " +
                        "(Id, Job, Skill, Skill_Level, Importance)" +
                        "VALUES(@Id, @Job, @Skill, @Skill_Level," +
                        " @Importance)";

                    cmd.Parameters.AddWithValue("Id", item.Id);
                    cmd.Parameters.AddWithValue("Job", item.Job);
                    cmd.Parameters.AddWithValue("Skill", item.Skill);
                    cmd.Parameters.AddWithValue("Skill_Level", item.SkillLevel);
                    cmd.Parameters.AddWithValue("Importance", item.Importance);

                    cmd.ExecuteNonQuery();

                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyJobSkillPoco> GetAll(params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlDataReader reader = (new SqlCommand("SELECT * FROM Company_Job_Skills", conn).ExecuteReader());

                var list = new List<CompanyJobSkillPoco>();

                while (reader.Read())
                {
                    list.Add(new CompanyJobSkillPoco()
                    {
                        Id = (Guid)reader["Id"],
                        Job = (Guid)reader["Job"],
                        Skill = (string)reader["Skill"],
                        SkillLevel = (string)reader["Skill_Level"],
                        Importance = (int)reader["Importance"]
                    });
                }
                return list;
            }
        }

        public IList<CompanyJobSkillPoco> GetList(Expression<Func<CompanyJobSkillPoco, bool>> where, params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobSkillPoco GetSingle(Expression<Func<CompanyJobSkillPoco, bool>> where, params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobSkillPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobSkillPoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandType = System.Data.CommandType.Text;

                foreach (CompanyJobSkillPoco item in items)
                {
                    cmd.CommandText = "DELETE FROM Company_Job_Skills WHERE Id=@Id";
                    cmd.Parameters.AddWithValue("Id", item.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(params CompanyJobSkillPoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();


                foreach (CompanyJobSkillPoco item in items)
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "UPDATE Company_Job_Skills " +
                        "SET " +
                        "Skill=@Skill, " +
                        "Skill_Level=@Skill_Level, " +
                        "Importance=@Importance " +
                        "WHERE Id=@Id";

                    cmd.Parameters.AddWithValue("Skill", item.Skill);
                    cmd.Parameters.AddWithValue("Skill_Level", item.SkillLevel);
                    cmd.Parameters.AddWithValue("Importance", item.Importance);
                    cmd.Parameters.AddWithValue("Id", item.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
