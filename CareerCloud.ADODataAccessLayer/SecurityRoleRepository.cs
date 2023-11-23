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
    public class SecurityRoleRepository : IDataRepository<SecurityRolePoco>
    {
        private readonly string _connectionString;
        public SecurityRoleRepository()
        {
            _connectionString = Connection.GetConnectionString();
        }
        public void Add(params SecurityRolePoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandType = System.Data.CommandType.Text;
                foreach (SecurityRolePoco item in items)
                {
                    cmd.CommandText = "INSERT INTO Security_Roles " +
                        "(Id, Role, Is_Inactive)" +
                        "VALUES(@Id, @Role, @Is_Inactive)";

                    cmd.Parameters.AddWithValue("Id", item.Id);
                    cmd.Parameters.AddWithValue("Role", item.Role);
                    cmd.Parameters.AddWithValue("Is_Inactive", item.IsInactive);

                    cmd.ExecuteNonQuery();

                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SecurityRolePoco> GetAll(params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlDataReader reader = (new SqlCommand("SELECT * FROM Security_Roles", conn).ExecuteReader());

                var list = new List<SecurityRolePoco>();

                while (reader.Read())
                {
                    list.Add(new SecurityRolePoco()
                    {
                        Id = (Guid)reader["Id"],
                        Role = (string)reader["Role"],
                        IsInactive = (bool)reader["Is_Inactive"]
                    });
                }
                return list;
            }
        }

        public IList<SecurityRolePoco> GetList(Expression<Func<SecurityRolePoco, bool>> where, params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityRolePoco GetSingle(Expression<Func<SecurityRolePoco, bool>> where, params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityRolePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityRolePoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandType = System.Data.CommandType.Text;

                foreach (SecurityRolePoco item in items)
                {
                    cmd.CommandText = "DELETE FROM Security_Roles WHERE Id=@Id";
                    cmd.Parameters.AddWithValue("Id", item.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(params SecurityRolePoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();


                foreach (SecurityRolePoco item in items)
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "UPDATE Security_Roles " +
                        "SET " +
                        "Role=@Role, " +
                        "Is_Inactive=@Is_Inactive " +
                        "WHERE Id=@Id";

                    cmd.Parameters.AddWithValue("Role", item.Role);
                    cmd.Parameters.AddWithValue("Is_Inactive", item.IsInactive);
                    cmd.Parameters.AddWithValue("Id", item.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
