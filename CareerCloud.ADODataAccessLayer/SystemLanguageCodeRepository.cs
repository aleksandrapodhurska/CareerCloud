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
    public class SystemLanguageCodeRepository : IDataRepository<SystemLanguageCodePoco>
    {
        private readonly string _connectionString;
        public SystemLanguageCodeRepository()
        {
            _connectionString = Connection.GetConnectionString();
        }
        public void Add(params SystemLanguageCodePoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandType = System.Data.CommandType.Text;
                foreach (SystemLanguageCodePoco item in items)
                {
                    cmd.CommandText = "INSERT INTO System_Language_Codes(LanguageID, Name, Native_Name) " +
                        "Values(@LanguageID, @Name, @Native_Name)";

                    cmd.Parameters.AddWithValue("LanguageID", item.LanguageID);
                    cmd.Parameters.AddWithValue("Name", item.Name);
                    cmd.Parameters.AddWithValue("Native_Name", item.NativeName);

                    cmd.ExecuteNonQuery();

                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SystemLanguageCodePoco> GetAll(params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlDataReader reader = (new SqlCommand("SELECT * FROM System_Language_Codes", conn).ExecuteReader());

                var list = new List<SystemLanguageCodePoco>();
                while (reader.Read())
                {
                    list.Add(new SystemLanguageCodePoco()
                    {
                        LanguageID = (string)reader["LanguageID"],
                        Name = (string)reader["Name"],
                        NativeName = (string)reader["Native_Name"],
                    });
                }
                return list;
            }
        }

        public IList<SystemLanguageCodePoco> GetList(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SystemLanguageCodePoco GetSingle(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            IQueryable<SystemLanguageCodePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SystemLanguageCodePoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandType = System.Data.CommandType.Text;

                foreach (SystemLanguageCodePoco item in items)
                {
                    cmd.CommandText = "DELETE FROM System_Language_Codes " +
                        "WHERE LanguageID=@LanguageID";

                    cmd.Parameters.AddWithValue("LanguageID", item.LanguageID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(params SystemLanguageCodePoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();

                foreach (SystemLanguageCodePoco item in items)
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "UPDATE System_Language_Codes " +
                        "SET Name=@Name, " +
                        "Native_Name=@Native_Name " +
                        "WHERE LanguageID=@LanguageID";

                    cmd.Parameters.AddWithValue("Name", item.Name);
                    cmd.Parameters.AddWithValue("Native_Name", item.NativeName);
                    cmd.Parameters.AddWithValue("LanguageID", item.LanguageID);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
