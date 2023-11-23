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
    public class SystemCountryCodeRepository : IDataRepository<SystemCountryCodePoco>
    {
        private readonly string _connectionString;
        public SystemCountryCodeRepository()
        {
            _connectionString = Connection.GetConnectionString();
        }
        public void Add(params SystemCountryCodePoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandType = System.Data.CommandType.Text;
                foreach (SystemCountryCodePoco item in items)
                {
                    cmd.CommandText = "INSERT INTO System_Country_Codes(Code, Name) " +
                        "Values(@Code, @Name)";

                    cmd.Parameters.AddWithValue("Code", item.Code);
                    cmd.Parameters.AddWithValue("Name", item.Name);

                    cmd.ExecuteNonQuery();

                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SystemCountryCodePoco> GetAll(params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            using(var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlDataReader reader = (new SqlCommand("SELECT * FROM System_Country_Codes", conn).ExecuteReader());

                var list = new List<SystemCountryCodePoco>();
                while (reader.Read())
                {
                    list.Add(new SystemCountryCodePoco()
                    {
                        Code = (string)reader["Code"],
                        Name = (string)reader["Name"],
                    });
                }
                return list;
            }
        }

        public IList<SystemCountryCodePoco> GetList(Expression<Func<SystemCountryCodePoco, bool>> where, params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SystemCountryCodePoco GetSingle(Expression<Func<SystemCountryCodePoco, bool>> where, params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            IQueryable<SystemCountryCodePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SystemCountryCodePoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandType = System.Data.CommandType.Text;

                foreach (SystemCountryCodePoco item in items)
                {
                    cmd.CommandText = "DELETE FROM System_Country_Codes " +
                        "WHERE Code=@Code";

                    cmd.Parameters.AddWithValue("Code", item.Code);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(params SystemCountryCodePoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();

                foreach (SystemCountryCodePoco item in items)
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "UPDATE System_Country_Codes " +
                        "SET Name=@Name " +
                        "WHERE Code=@Code";

                    cmd.Parameters.AddWithValue("Name", item.Name);
                    cmd.Parameters.AddWithValue("Code", item.Code);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
