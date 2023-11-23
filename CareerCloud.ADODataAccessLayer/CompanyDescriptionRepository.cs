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
    public class CompanyDescriptionRepository : IDataRepository<CompanyDescriptionPoco>
    {
        private readonly string _connectionString;
        public CompanyDescriptionRepository()
        {
            _connectionString = Connection.GetConnectionString();
        }
        public void Add(params CompanyDescriptionPoco[] items)
        {
            using(var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandType = System.Data.CommandType.Text;

                foreach (CompanyDescriptionPoco item in items)
                {
                    cmd.CommandText = "INSERT INTO Company_Descriptions(Id, Company, LanguageId, Company_Name, Company_Description) " +
                        "VALUES(@Id, @Company, @LanguageId, @Company_Name, @Company_Description)";

                    cmd.Parameters.AddWithValue("Id", item.Id);
                    cmd.Parameters.AddWithValue("Company", item.Company);
                    cmd.Parameters.AddWithValue("LanguageId", item.LanguageId);
                    cmd.Parameters.AddWithValue("Company_Name", item.CompanyName);
                    cmd.Parameters.AddWithValue("Company_Description", item.CompanyDescription);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyDescriptionPoco> GetAll(params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlDataReader reader = (new SqlCommand("SELECT * FROM Company_Descriptions", conn).ExecuteReader());

                var list = new List<CompanyDescriptionPoco>();

                while (reader.Read())
                {
                    list.Add(new CompanyDescriptionPoco()
                    {
                        Id = (Guid)reader["Id"],
                        Company = (Guid)reader["Company"],
                        LanguageId = (string)reader["LanguageId"],
                        CompanyName = (string)reader["Company_Name"],
                        CompanyDescription = (string)reader["Company_Description"]
                    });
                }
                return list;
            }
        }

        public IList<CompanyDescriptionPoco> GetList(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyDescriptionPoco GetSingle(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyDescriptionPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyDescriptionPoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandType = System.Data.CommandType.Text;

                foreach (CompanyDescriptionPoco item in items)
                {
                    cmd.CommandText = "DELETE FROM Company_Descriptions WHERE Id=@Id";
                    cmd.Parameters.AddWithValue("Id", item.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(params CompanyDescriptionPoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandType = System.Data.CommandType.Text;

                foreach (CompanyDescriptionPoco item in items)
                {
                    cmd.CommandText = "UPDATE Company_Descriptions " +
                        "SET " +
                        "Company_Name=@Company_Name, " +
                        "Company_Description=@Company_Description " +
                        "WHERE Id=@Id";

                    cmd.Parameters.AddWithValue("Company_Name", item.CompanyName);
                    cmd.Parameters.AddWithValue("Company_Description", item.CompanyDescription);
                    cmd.Parameters.AddWithValue("Id", item.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
