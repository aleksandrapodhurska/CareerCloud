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
    public class ApplicantSkillRepository : IDataRepository<ApplicantSkillPoco>
    {
        private readonly string _connectionString;
        public ApplicantSkillRepository()
        {
            _connectionString = Connection.GetConnectionString();
        }
        public void Add(params ApplicantSkillPoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandType = System.Data.CommandType.Text;

                foreach (ApplicantSkillPoco item in items)
                {
                    cmd.CommandText = "INSERT INTO Applicant_Skills(Id, Applicant, Skill, Skill_Level, Start_Month, Start_Year, End_Month, End_Year) " +
                        "VALUES(@Id, @Applicant, @Skill, @Skill_Level, @Start_Month, @Start_Year, @End_Month, @End_Year)";

                    cmd.Parameters.AddWithValue("Id", item.Id);
                    cmd.Parameters.AddWithValue("Applicant", item.Applicant);
                    cmd.Parameters.AddWithValue("Skill", item.Skill);
                    cmd.Parameters.AddWithValue("Skill_Level", item.SkillLevel);
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

        public IList<ApplicantSkillPoco> GetAll(params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlDataReader reader = (new SqlCommand("SELECT * FROM Applicant_Skills", conn).ExecuteReader());

                var list = new List<ApplicantSkillPoco>();

                while (reader.Read())
                {
                    list.Add(new ApplicantSkillPoco()
                    {
                        Id = (Guid)reader["Id"],
                        Applicant = (Guid)reader["Applicant"],
                        Skill = (string)reader["Skill"],
                        SkillLevel = (string)reader["Skill_Level"],
                        StartMonth = (byte)reader["Start_Month"],
                        StartYear = (int)reader["Start_Year"],
                        EndMonth = (byte)reader["End_Month"],
                        EndYear = (int)reader["End_Year"]
                    });
                }
                return list;
            }
        }

        public IList<ApplicantSkillPoco> GetList(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantSkillPoco GetSingle(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantSkillPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantSkillPoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandType = System.Data.CommandType.Text;

                foreach (ApplicantSkillPoco item in items)
                {
                    cmd.CommandText = "DELETE FROM Applicant_Skills WHERE Id=@Id";
                    cmd.Parameters.AddWithValue("Id", item.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(params ApplicantSkillPoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandType = System.Data.CommandType.Text;

                foreach (ApplicantSkillPoco item in items)
                {
                    cmd.CommandText = "UPDATE Applicant_Skills " +
                        "SET " +
                        "Skill=@Skill, " +
                        "Skill_Level=@Skill_Level, " +
                        "Start_Month=@Start_Month, " +
                        "Start_Year=@Start_Year, " +
                        "End_Month=@End_Month, " +
                        "End_Year=@End_Year " +
                        "WHERE Id=@Id";

                    cmd.Parameters.AddWithValue("Skill", item.Skill);
                    cmd.Parameters.AddWithValue("Skill_Level", item.SkillLevel);
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
