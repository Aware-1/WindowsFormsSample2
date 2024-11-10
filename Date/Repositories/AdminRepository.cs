using Data;
using Doamin.Entities;
using Doamin.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Date.Repositories
{
    public class AdminRepository : IAdminRepository
    {

        private string connectionString = "Data Source=.;Initial Catalog=ClientSample;Integrated Security=True;MultipleActiveResultSets=true";
        SqlConnection connection;
        LoggerConfig _loggerConfig;
        private readonly IAdminRepository _adminRepository;

        public AdminRepository()
        {
            _loggerConfig = new LoggerConfig();
            connection = new SqlConnection(connectionString);
        }

        public bool ValidateUser(string userName, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @" 
                SELECT COUNT(1) FROM Admin
                WHERE UserName = @userName 
                AND Password = @Password AND Active = 0";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@userName", userName);
                command.Parameters.AddWithValue("@Password", password);
                connection.Open();
                int userCount = (int)command.ExecuteScalar();
                return userCount > 0;
            }
        }

        public List<Admin> GetUsers()
        {
            List<Admin> users = new List<Admin>();

            string query = @"
              SELECT Id
                    ,UserName
                    ,IsLimit
              FROM  Admin
              WHERE Active=0;";

            try
            {
                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();

            }
            catch (Exception ex)
            {
                _loggerConfig.LogError($"connection faliled: {ex.Message}");

            }

            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                users.Add(new Admin
                {
                    Id = (int)reader["Id"],
                    UserName = reader["UserName"].ToString(),
                    IsLimit = (bool)reader["IsLimit"]
                });
            }
            reader.Close();
            return users;
        }

        public Admin GetAdminDetails(int adminId)
        {
            Admin admin = null;
            string query = @" SELECT Id, UserName, IsLimit, Active FROM Admin WHERE Id = @AdminId";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AdminId", adminId);
                connection.Open(); SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    admin = new Admin
                    {
                        Id = (int)reader["Id"],
                        UserName = reader["UserName"].ToString(),
                        IsLimit = Convert.ToBoolean(reader["IsLimit"]),
                        Active = Convert.ToBoolean(reader["Active"])
                    };
                }
                reader.Close();
            }
            return admin;
        }
        public void UpdateAdmin(int adminId, string userName, bool isLimit, bool active)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query;
                if (adminId == 0)
                { query = @" INSERT INTO Admin (UserName, IsLimit, Active) 
                    VALUES (@UserName, @IsLimit, @Active)"; }
                else 
                { query = @" UPDATE Admin SET UserName = @UserName, IsLimit = @IsLimit, Active = @Active
                    WHERE Id = @AdminId"; }

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserName", userName);
                command.Parameters.AddWithValue("@IsLimit", isLimit ? 1 : 0);
                command.Parameters.AddWithValue("@Active", active ? 1 : 0);
                if (adminId != 0)
                {  command.Parameters.AddWithValue("@AdminId", adminId); }
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
