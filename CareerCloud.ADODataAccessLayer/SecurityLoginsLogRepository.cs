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
    public class SecurityLoginsLogRepository : IDataRepository<SecurityLoginsLogPoco>
    {
        private readonly string _connectionString;
        public SecurityLoginsLogRepository()
        {
            _connectionString = Connection.GetConnectionString();
        }
        public void Add(params SecurityLoginsLogPoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandType = System.Data.CommandType.Text;
                foreach (SecurityLoginsLogPoco item in items)
                {
                    cmd.CommandText = "INSERT INTO Security_Logins_Log " +
                        "(Id, Login, Source_IP, Logon_Date, Is_Succesful)" +
                        "VALUES(@Id, @Login, @Source_IP, @Logon_Date, @Is_Succesful)";

                    cmd.Parameters.AddWithValue("Id", item.Id);
                    cmd.Parameters.AddWithValue("Login", item.Login);
                    cmd.Parameters.AddWithValue("Source_IP", item.SourceIP);
                    cmd.Parameters.AddWithValue("Logon_Date", item.LogonDate);
                    cmd.Parameters.AddWithValue("Is_Succesful", item.IsSuccesful);

                    cmd.ExecuteNonQuery();

                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SecurityLoginsLogPoco> GetAll(params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlDataReader reader = (new SqlCommand("SELECT * FROM Security_Logins_Log", conn).ExecuteReader());

                var list = new List<SecurityLoginsLogPoco>();

                while (reader.Read())
                {
                    list.Add(new SecurityLoginsLogPoco()
                    {
                        Id = (Guid)reader["Id"],
                        Login = (Guid)reader["Login"],
                        SourceIP = (string)reader["Source_IP"],
                        LogonDate = (DateTime)reader["Logon_Date"],
                        IsSuccesful = (bool)reader["Is_Succesful"]
                    });
                }
                return list;
            }
        }

        public IList<SecurityLoginsLogPoco> GetList(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginsLogPoco GetSingle(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginsLogPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginsLogPoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandType = System.Data.CommandType.Text;

                foreach (SecurityLoginsLogPoco item in items)
                {
                    cmd.CommandText = "DELETE FROM Security_Logins_Log WHERE Id=@Id";
                    cmd.Parameters.AddWithValue("Id", item.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(params SecurityLoginsLogPoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();


                foreach (SecurityLoginsLogPoco item in items)
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "UPDATE Security_Logins_Log " +
                        "SET " +
                        "Login=@Login, " +
                        "Source_IP=@Source_IP, " +
                        "Logon_Date=@Logon_Date, " +
                        "Is_Succesful=@Is_Succesful " +
                        "WHERE Id=@Id";

                    cmd.Parameters.AddWithValue("Login", item.Login);
                    cmd.Parameters.AddWithValue("Source_IP", item.SourceIP);
                    cmd.Parameters.AddWithValue("Logon_Date", item.LogonDate);
                    cmd.Parameters.AddWithValue("Is_Succesful", item.IsSuccesful);
                    cmd.Parameters.AddWithValue("Id", item.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
