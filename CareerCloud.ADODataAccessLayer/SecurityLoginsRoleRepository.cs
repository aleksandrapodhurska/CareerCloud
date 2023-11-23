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
    public class SecurityLoginsRoleRepository : IDataRepository<SecurityLoginsRolePoco>
    {
        private readonly string _connectionString;
        public SecurityLoginsRoleRepository()
        {
            _connectionString = Connection.GetConnectionString();
        }
        public void Add(params SecurityLoginsRolePoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandType = System.Data.CommandType.Text;

                foreach (SecurityLoginsRolePoco item in items)
                {
                    cmd.CommandText = "INSERT INTO Security_Logins_Roles(Id, Login, Role) " +
                        "VALUES(@Id, @Login, @Role)";

                    cmd.Parameters.AddWithValue("Id", item.Id);
                    cmd.Parameters.AddWithValue("Login", item.Login);
                    cmd.Parameters.AddWithValue("Role", item.Role);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SecurityLoginsRolePoco> GetAll(params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlDataReader reader = (new SqlCommand("SELECT * FROM Security_Logins_Roles", conn).ExecuteReader());

                var list = new List<SecurityLoginsRolePoco>();

                while (reader.Read())
                {
                    list.Add(new SecurityLoginsRolePoco()
                    {
                        Id = (Guid)reader["Id"],
                        Login = (Guid)reader["Login"],
                        Role = (Guid)reader["Role"]
                    });
                }

                return list;
            }
        }

        public IList<SecurityLoginsRolePoco> GetList(Expression<Func<SecurityLoginsRolePoco, bool>> where, params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginsRolePoco GetSingle(Expression<Func<SecurityLoginsRolePoco, bool>> where, params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginsRolePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginsRolePoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandType = System.Data.CommandType.Text;

                foreach (SecurityLoginsRolePoco item in items)
                {
                    cmd.CommandText = "DELETE FROM Security_Logins_Roles WHERE Id=@Id";
                    cmd.Parameters.AddWithValue("Id", item.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(params SecurityLoginsRolePoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();

                foreach (SecurityLoginsRolePoco item in items)
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "UPDATE Security_Logins_Roles " +
                        "SET " +
                        "Role=@Role " +
                        "WHERE Id=@Id";

                    cmd.Parameters.AddWithValue("Role", item.Role);
                    cmd.Parameters.AddWithValue("Id", item.Id);

                    cmd.ExecuteNonQuery();
                }

            }
        }
    }
}
