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
    public class ApplicantProfileRepository : IDataRepository<ApplicantProfilePoco>
    {
        private readonly string _connectionString;
        public ApplicantProfileRepository()
        {
            _connectionString = Connection.GetConnectionString();
        }
        public void Add(params ApplicantProfilePoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandType = System.Data.CommandType.Text;

                foreach (ApplicantProfilePoco item in items)
                {
                    cmd.CommandText = "INSERT INTO Applicant_Profiles" +
                        "(Id, Login, Current_Salary, Current_Rate, Currency, Country_Code, " +
                        "State_Province_Code, Street_Address, City_Town, Zip_Postal_Code) " +
                        "VALUES(" +
                        "@Id, " +
                        "@Login, " +
                        "@Current_Salary, " +
                        "@Current_Rate, " +
                        "@Currency, " +
                        "@Country_Code, " +
                        "@State_Province_Code, " +
                        "@Street_Address, " +
                        "@City_Town, " +
                        "@Zip_Postal_Code)";

                    cmd.Parameters.AddWithValue("Id", item.Id);
                    cmd.Parameters.AddWithValue("Login", item.Login);
                    cmd.Parameters.AddWithValue("Current_Salary", item.CurrentSalary);
                    cmd.Parameters.AddWithValue("Current_Rate", item.CurrentRate);
                    cmd.Parameters.AddWithValue("Currency", item.Currency);
                    cmd.Parameters.AddWithValue("Country_Code", item.Country);
                    cmd.Parameters.AddWithValue("State_Province_Code", item.Province);
                    cmd.Parameters.AddWithValue("Street_Address", item.Street);
                    cmd.Parameters.AddWithValue("City_Town", item.City);
                    cmd.Parameters.AddWithValue("Zip_Postal_Code", item.PostalCode);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantProfilePoco> GetAll(params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlDataReader reader = (new SqlCommand("SELECT * FROM Applicant_Profiles", conn).ExecuteReader());

                var list = new List<ApplicantProfilePoco>();

                while (reader.Read())
                {
                    list.Add(new ApplicantProfilePoco() { 
                        Id = (Guid)reader["Id"],
                        Login = (Guid)reader["Login"],
                        CurrentSalary = (decimal)reader["Current_Salary"],
                        CurrentRate = (decimal)reader["Current_Rate"],
                        Currency = (string)reader["Currency"],
                        Country = (string)reader["Country_Code"],
                        Province = (string)reader["State_Province_Code"],
                        Street = (string)reader["Street_Address"],
                        City = (string)reader["City_Town"],
                        PostalCode = (string)reader["Zip_Postal_Code"],
                    });
                }

                return list;
            }
        }

        public IList<ApplicantProfilePoco> GetList(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantProfilePoco GetSingle(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantProfilePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantProfilePoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandType = System.Data.CommandType.Text;

                foreach (ApplicantProfilePoco item in items)
                {
                    cmd.CommandText = "DELETE FROM Applicant_Profiles  WHERE Id=@Id";
                    cmd.Parameters.AddWithValue("Id", item.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(params ApplicantProfilePoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandType = System.Data.CommandType.Text;

                foreach (ApplicantProfilePoco item in items)
                {
                    cmd.CommandText = "UPDATE Applicant_Profiles " +
                        "SET " +
                        "Current_Salary=@Current_Salary, " +
                        "Current_Rate=@Current_Rate, " +
                        "Currency=@Currency, " +
                        "State_Province_Code=@State_Province_Code, " +
                        "Street_Address=@Street_Address, " +
                        "City_Town=@City_Town, " +
                        "Zip_Postal_Code=@Zip_Postal_Code " +
                        "WHERE Id=@Id";

                    cmd.Parameters.AddWithValue("Id", item.Id);
                    cmd.Parameters.AddWithValue("Current_Salary", item.CurrentSalary);
                    cmd.Parameters.AddWithValue("Current_Rate", item.CurrentRate);
                    cmd.Parameters.AddWithValue("Currency", item.Currency);
                    cmd.Parameters.AddWithValue("State_Province_Code", item.Province);
                    cmd.Parameters.AddWithValue("Street_Address", item.Street);
                    cmd.Parameters.AddWithValue("City_Town", item.City);
                    cmd.Parameters.AddWithValue("Zip_Postal_Code", item.PostalCode);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
