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
    public class CompanyLocationRepository : IDataRepository<CompanyLocationPoco>
    {
        private readonly string _connectionString;
        public CompanyLocationRepository()
        {
            _connectionString = Connection.GetConnectionString();
        }
        public void Add(params CompanyLocationPoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandType = System.Data.CommandType.Text;
                foreach (CompanyLocationPoco item in items)
                {
                    cmd.CommandText = "INSERT INTO Company_Locations " +
                        "(Id, Company, Country_Code, State_Province_Code, Street_Address, City_Town, " +
                        "Zip_Postal_Code)" +
                        "VALUES(@Id, @Company, @Country_Code, @State_Province_Code, @Street_Address," +
                        " @City_Town, @Zip_Postal_Code)";

                    cmd.Parameters.AddWithValue("Id", item.Id);
                    cmd.Parameters.AddWithValue("Company", item.Company);
                    cmd.Parameters.AddWithValue("Country_Code", item.CountryCode);
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

        public IList<CompanyLocationPoco> GetAll(params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlDataReader reader = (new SqlCommand("SELECT * FROM Company_Locations", conn).ExecuteReader());

                var list = new List<CompanyLocationPoco>();

                while (reader.Read())
                {
                    list.Add(new CompanyLocationPoco()
                    {
                        Id = (Guid)reader["Id"],
                        Company = (Guid)reader["Company"],
                        CountryCode = (string)reader["Country_Code"],
                        Province = Convert.IsDBNull(reader["State_Province_Code"]) ? null : (string)reader["State_Province_Code"],
                        Street = (string)reader["Street_Address"],
                        City = Convert.IsDBNull(reader["City_Town"]) ? null : (string)reader["City_Town"],
                        PostalCode = Convert.IsDBNull(reader["Zip_Postal_Code"]) ? null : (string)reader["Zip_Postal_Code"]
                    });
                }
                return list;
            }
        }

        public IList<CompanyLocationPoco> GetList(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyLocationPoco GetSingle(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyLocationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyLocationPoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandType = System.Data.CommandType.Text;

                foreach (CompanyLocationPoco item in items)
                {
                    cmd.CommandText = "DELETE FROM Company_Locations WHERE Id=@Id";
                    cmd.Parameters.AddWithValue("Id", item.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(params CompanyLocationPoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();


                foreach (CompanyLocationPoco item in items)
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "UPDATE Company_Locations " +
                        "SET " +
                        "Country_Code=@Country_Code, " +
                        "State_Province_Code=@State_Province_Code, " +
                        "Street_Address=@Street_Address, " +
                        "City_Town=@City_Town, " +
                        "Zip_Postal_Code=@Zip_Postal_Code " +
                        "WHERE Id=@Id";

                    cmd.Parameters.AddWithValue("Country_Code", item.CountryCode);
                    cmd.Parameters.AddWithValue("State_Province_Code", item.Province);
                    cmd.Parameters.AddWithValue("Street_Address", item.Street);
                    cmd.Parameters.AddWithValue("City_Town", item.City);
                    cmd.Parameters.AddWithValue("Zip_Postal_Code", item.PostalCode);
                    cmd.Parameters.AddWithValue("Id", item.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
