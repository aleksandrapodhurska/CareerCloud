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
    public class CompanyJobRepository : IDataRepository<CompanyJobPoco>
    {
        private readonly string _connectionString;
        public CompanyJobRepository()
        {
            _connectionString = Connection.GetConnectionString();
        }
        public void Add(params CompanyJobPoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandType = System.Data.CommandType.Text;
                foreach (CompanyJobPoco item in items)
                {
                    cmd.CommandText = "INSERT INTO Company_Jobs " +
                        "(Id, Company, Profile_Created, Is_Inactive, Is_Company_Hidden)" +
                        "VALUES(@Id, @Company, @Profile_Created, @Is_Inactive, @Is_Company_Hidden)";

                    cmd.Parameters.AddWithValue("Id", item.Id);
                    cmd.Parameters.AddWithValue("Company", item.Company);
                    cmd.Parameters.AddWithValue("Profile_Created", item.ProfileCreated);
                    cmd.Parameters.AddWithValue("Is_Inactive", item.IsInactive);
                    cmd.Parameters.AddWithValue("Is_Company_Hidden", item.IsCompanyHidden);

                    cmd.ExecuteNonQuery();

                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyJobPoco> GetAll(params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlDataReader reader = (new SqlCommand("SELECT * FROM Company_Jobs", conn).ExecuteReader());

                var list = new List<CompanyJobPoco>();

                while (reader.Read())
                {
                    list.Add(new CompanyJobPoco()
                    {
                        Id = (Guid)reader["Id"],
                        Company = (Guid)reader["Company"],
                        ProfileCreated = (DateTime)reader["Profile_Created"],
                        IsInactive = (bool)reader["Is_Inactive"],
                        IsCompanyHidden = (bool)reader["Is_Company_Hidden"]
                    });
                }
                return list;
            }
        }

        public IList<CompanyJobPoco> GetList(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobPoco GetSingle(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobPoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandType = System.Data.CommandType.Text;

                foreach (CompanyJobPoco item in items)
                {
                    cmd.CommandText = "DELETE FROM Company_Jobs WHERE Id=@Id";
                    cmd.Parameters.AddWithValue("Id", item.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(params CompanyJobPoco[] items)
        {
             using (var conn = new SqlConnection(_connectionString))
             {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();


                foreach (CompanyJobPoco item in items)
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "UPDATE Company_Jobs " +
                        "SET " +
                        "Profile_Created=@Profile_Created, " +
                        "Is_Inactive=@Is_Inactive, " +
                        "Is_Company_Hidden=@Is_Company_Hidden " +
                        "WHERE Id=@Id";

                    cmd.Parameters.AddWithValue("Profile_Created", item.ProfileCreated);
                    cmd.Parameters.AddWithValue("Is_Inactive", item.IsInactive);
                    cmd.Parameters.AddWithValue("Is_Company_Hidden", item.IsCompanyHidden);
                    cmd.Parameters.AddWithValue("Id", item.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
