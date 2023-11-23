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
    public class SecurityLoginRepository : IDataRepository<SecurityLoginPoco>
    {
        private readonly string _connectionString;
        public SecurityLoginRepository()
        {
            _connectionString = Connection.GetConnectionString();
        }
        public void Add(params SecurityLoginPoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandType = System.Data.CommandType.Text;
                foreach (SecurityLoginPoco item in items)
                {
                    cmd.CommandText = "INSERT INTO Security_Logins " +
                        "(Id, Login, Password, Created_Date, Password_Update_Date, Agreement_Accepted_Date, Is_Locked, Is_Inactive, Email_Address, Phone_Number, Full_Name, Force_Change_Password, Prefferred_Language)" +
                        "VALUES(@Id, @Login, @Password, @Created_Date, @Password_Update_Date, @Agreement_Accepted_Date, @Is_Locked, @Is_Inactive, @Email_Address, @Phone_Number, @Full_Name, @Force_Change_Password, @Prefferred_Language)";

                    cmd.Parameters.AddWithValue("Id", item.Id);
                    cmd.Parameters.AddWithValue("Login", item.Login);
                    cmd.Parameters.AddWithValue("Password", item.Password);
                    cmd.Parameters.AddWithValue("Created_Date", item.Created);
                    cmd.Parameters.AddWithValue("Password_Update_Date", item.PasswordUpdate);
                    cmd.Parameters.AddWithValue("Agreement_Accepted_Date", item.AgreementAccepted);
                    cmd.Parameters.AddWithValue("Is_Locked", item.IsLocked);
                    cmd.Parameters.AddWithValue("Is_Inactive", item.IsInactive);
                    cmd.Parameters.AddWithValue("Email_Address", item.EmailAddress);
                    cmd.Parameters.AddWithValue("Phone_Number", item.PhoneNumber);
                    cmd.Parameters.AddWithValue("Full_Name", item.FullName);
                    cmd.Parameters.AddWithValue("Force_Change_Password", item.ForceChangePassword);
                    cmd.Parameters.AddWithValue("Prefferred_Language", item.PrefferredLanguage);

                    cmd.ExecuteNonQuery();

                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SecurityLoginPoco> GetAll(params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlDataReader reader = (new SqlCommand("SELECT * FROM Security_Logins", conn).ExecuteReader());

                var list = new List<SecurityLoginPoco>();

                while (reader.Read())
                {
                    list.Add(new SecurityLoginPoco()
                    {
                        Id = (Guid)reader["Id"],
                        Login = (string)reader["Login"],
                        Password = (string)reader["Password"],
                        Created = (DateTime)reader["Created_Date"],
                        PasswordUpdate = Convert.IsDBNull(reader["Password_Update_Date"]) ? null : (DateTime)reader["Password_Update_Date"],
                        AgreementAccepted = Convert.IsDBNull(reader["Agreement_Accepted_Date"]) ? null : (DateTime)reader["Agreement_Accepted_Date"],
                        IsLocked = (bool)reader["Is_Locked"],
                        IsInactive = (bool)reader["Is_Inactive"],
                        EmailAddress = (string)reader["Email_Address"],
                        PhoneNumber = Convert.IsDBNull(reader["Phone_Number"]) ? null : (string)reader["Phone_Number"],
                        FullName = Convert.IsDBNull(reader["Full_Name"]) ? null : (string)reader["Full_Name"],
                        ForceChangePassword = (bool)reader["Force_Change_Password"],
                        PrefferredLanguage = Convert.IsDBNull(reader["Prefferred_Language"]) ? null : (string)reader["Prefferred_Language"]
                    });
                }
                return list;
            }
        }

        public IList<SecurityLoginPoco> GetList(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginPoco GetSingle(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginPoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandType = System.Data.CommandType.Text;

                foreach (SecurityLoginPoco item in items)
                {
                    cmd.CommandText = "DELETE FROM Security_Logins WHERE Id=@Id";
                    cmd.Parameters.AddWithValue("Id", item.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(params SecurityLoginPoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();


                foreach (SecurityLoginPoco item in items)
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "UPDATE Security_Logins " +
                        "SET " +
                        "Login=@Login, " +
                        "Password=@Password, " +
                        "Created_Date=@Created_Date, " +
                        "Password_Update_Date=@Password_Update_Date, " +
                        "Agreement_Accepted_Date=@Agreement_Accepted_Date, " +
                        "Is_Locked=@Is_Locked, " +
                        "Is_Inactive=@Is_Inactive, " +
                        "Email_Address=@Email_Address, " +
                        "Phone_Number=@Phone_Number, " +
                        "Full_Name=@Full_Name, " +
                        "Force_Change_Password=@Force_Change_Password, " +
                        "Prefferred_Language=@Prefferred_Language " +
                        "WHERE Id=@Id";

                    cmd.Parameters.AddWithValue("Login", item.Login);
                    cmd.Parameters.AddWithValue("Password", item.Password);
                    cmd.Parameters.AddWithValue("Created_Date", item.Created);
                    cmd.Parameters.AddWithValue("Password_Update_Date", item.PasswordUpdate);
                    cmd.Parameters.AddWithValue("Agreement_Accepted_Date", item.AgreementAccepted);
                    cmd.Parameters.AddWithValue("Is_Locked", item.IsLocked);
                    cmd.Parameters.AddWithValue("Is_Inactive", item.IsInactive);
                    cmd.Parameters.AddWithValue("Email_Address", item.EmailAddress);
                    cmd.Parameters.AddWithValue("Phone_Number", item.PhoneNumber);
                    cmd.Parameters.AddWithValue("Full_Name", item.FullName);
                    cmd.Parameters.AddWithValue("Force_Change_Password", item.ForceChangePassword);
                    cmd.Parameters.AddWithValue("Prefferred_Language", item.PrefferredLanguage);
                    cmd.Parameters.AddWithValue("Id", item.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
