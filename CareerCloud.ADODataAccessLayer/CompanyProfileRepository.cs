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
    public class CompanyProfileRepository : IDataRepository<CompanyProfilePoco>
    {
        private readonly string _connectionString;
        public CompanyProfileRepository()
        {
            _connectionString = Connection.GetConnectionString();
        }
        public void Add(params CompanyProfilePoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandType = System.Data.CommandType.Text;
                foreach (CompanyProfilePoco item in items)
                {
                    cmd.CommandText = "INSERT INTO Company_Profiles " +
                        "(Id, Registration_Date, Company_Website, Contact_Phone, " +
                        "Contact_Name, Company_Logo)" +
                        "VALUES(@Id, @Registration_Date, @Company_Website," +
                        " @Contact_Phone, @Contact_Name, @Company_Logo)";

                    cmd.Parameters.AddWithValue("Id", item.Id);
                    cmd.Parameters.AddWithValue("Registration_Date", item.RegistrationDate);
                    cmd.Parameters.AddWithValue("Company_Website", item.CompanyWebsite);
                    cmd.Parameters.AddWithValue("Contact_Phone", item.ContactPhone);
                    cmd.Parameters.AddWithValue("Contact_Name", item.ContactName);
                    cmd.Parameters.AddWithValue("Company_Logo", item.CompanyLogo);

                    cmd.ExecuteNonQuery();

                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyProfilePoco> GetAll(params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlDataReader reader = (new SqlCommand("SELECT * FROM Company_Profiles", conn).ExecuteReader());

                var list = new List<CompanyProfilePoco>();

                while (reader.Read())
                {
                    list.Add(new CompanyProfilePoco()
                    {
                        Id = (Guid)reader["Id"],
                        CompanyWebsite = Convert.IsDBNull(reader["Company_Website"]) ? null : (string)reader["Company_Website"],
                        ContactName = Convert.IsDBNull(reader["Contact_Name"]) ? null : (string)reader["Contact_Name"],
                        ContactPhone = (string)reader["Contact_Phone"],
                        RegistrationDate = (DateTime)reader["Registration_Date"]
                    });
                }
                return list;
            }
        }

        public IList<CompanyProfilePoco> GetList(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyProfilePoco GetSingle(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyProfilePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyProfilePoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandType = System.Data.CommandType.Text;

                foreach (CompanyProfilePoco item in items)
                {
                    cmd.CommandText = "DELETE FROM Company_Profiles WHERE Id=@Id";
                    cmd.Parameters.AddWithValue("Id", item.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(params CompanyProfilePoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();


                foreach (CompanyProfilePoco item in items)
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "UPDATE Company_Profiles " +
                        "SET " +
                        "Registration_Date=@Registration_Date, " +
                        "Company_Website=@Company_Website, " +
                        "Contact_Phone=@Contact_Phone, " +
                        "Contact_Name=@Contact_Name, " +
                        "Company_Logo=@Company_Logo " +
                        "WHERE Id=@Id";

                    cmd.Parameters.AddWithValue("Registration_Date", item.RegistrationDate);
                    cmd.Parameters.AddWithValue("Company_Website", item.CompanyWebsite);
                    cmd.Parameters.AddWithValue("Contact_Phone", item.ContactPhone);
                    cmd.Parameters.AddWithValue("Contact_Name", item.ContactName);
                    cmd.Parameters.AddWithValue("Company_Logo", item.CompanyLogo);
                    cmd.Parameters.AddWithValue("Id", item.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
