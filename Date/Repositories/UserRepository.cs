using Doamin.Entities;
using Doamin.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Data;
using PersianDate;
using System.Net.Http;
using Newtonsoft.Json;

namespace Date.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ICityRepository _cityRepository;

        private IUserRepository _userRepository;
        private string connectionString = "Data Source=.;Initial Catalog=ClientSample;Integrated Security=True;MultipleActiveResultSets=true";
        SqlConnection connection;
        LoggerConfig _loggerConfig;

        public UserRepository()
        {
            _loggerConfig = new LoggerConfig();

            connection = new SqlConnection(connectionString);
        }


        public void AddUser(User user)
        {
            _loggerConfig.LogError("AddUser Name:", user.Name);

            if (connection.State != System.Data.ConnectionState.Open)
                connection.Open();

            string query = "INSERT INTO Users (Name, BirthDate, marriage, CityId) VALUES (@Name, @BirthDate, @marriage, @CityId)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Name", user.Name);
            command.Parameters.AddWithValue("@BirthDate", user.BirthDate);
            command.Parameters.AddWithValue("@marriage", user.marriage);
            command.Parameters.AddWithValue("@CityId", user.CityId);

            command.ExecuteNonQuery();
            connection.Close();
        }


        public List<User> GetUsers()
        {
            _loggerConfig.LogWarning("GetUsers:", "");

            List<User> users = new List<User>();

            string query = @"
             SELECT Users.Id, Users.Name, Users.BirthDate, Users.marriage, Users.CityId, Cities.Name AS CityName
             FROM Users
             JOIN Cities ON Users.CityId = Cities.Id
             WHERE Users.IsDelete = 0";

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
                /*                var birthDate = Convert.ToDateTime(reader["BirthDate"]); 
                                var persianBirthDate = birthDate.ToPersianDateString();
                */
                users.Add(new User
                {
                    Id = (int)reader["Id"],
                    Name = reader["Name"].ToString(),
                    BirthDate = (DateTime)reader["BirthDate"],
                    marriage = (bool)reader["marriage"],
                    CityId = (int)reader["CityId"],
                    City = new City
                    {
                        Id = (int)reader["CityId"],
                        Name = reader["CityName"].ToString()
                    }
                });
            }
            reader.Close();
            return users;
        }

        public void DeleteUser(int userId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Users SET IsDelete = 1 WHERE Id = @UserId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserId", userId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public List<User> GetUsers(string searchText = null, string selectedCity = null)
        {
            _loggerConfig.LogWarning("Serach:", $"Name {searchText} city: {selectedCity}");

            List<User> users = new List<User>();

            string query = @" SELECT Users.Id, Users.Name, Users.BirthDate, Users.marriage, Users.CityId, Cities.Name AS CityName 
            FROM Users
            JOIN Cities ON Users.CityId = Cities.Id 
            WHERE Users.IsDelete = 0 " +
            (string.IsNullOrEmpty(searchText) ? "" : $"AND Users.Name LIKE N'%{searchText}%' ") +
            (string.IsNullOrEmpty(selectedCity) ? "" : $"AND Cities.Name LIKE N'%{selectedCity}%' ");

            if (connection.State != System.Data.ConnectionState.Open)
                connection.Open();

            SqlCommand command = new SqlCommand(query, connection);

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                users.Add(new User
                {
                    Id = (int)reader["Id"],
                    Name = reader["Name"].ToString(),
                    BirthDate = (DateTime)reader["BirthDate"],
                    marriage = (bool)reader["marriage"],
                    CityId = (int)reader["CityId"],
                    City = new City { Id = (int)reader["CityId"], Name = reader["CityName"].ToString() }
                });
            }
            reader.Close();

            return users;
        }

        public void AddUserToDatabase(string name, DateTime birthDate, bool marriage, int cityId)
        {
            _loggerConfig.LogWarning("add user:", name);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Users (Name, BirthDate, marriage, CityId) VALUES (@Name, @BirthDate, @marriage, @CityId)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@BirthDate", birthDate);
                command.Parameters.AddWithValue("@marriage", marriage);
                command.Parameters.AddWithValue("@CityId", cityId);

                connection.Open();
                command.ExecuteNonQuery();
            }


        }

        public void InsertUser(string name, DateTime birthDate, bool marriage, int cityId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("INSERT INTO Users (Name, BirthDate, marriage, CityId, IsDelete) VALUES (@Name, @BirthDate, @marriage, @CityId,0)", connection);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@BirthDate", birthDate);
                command.Parameters.AddWithValue("@marriage", marriage);
                command.Parameters.AddWithValue("@CityId", cityId);
                command.ExecuteNonQuery();
            }
        }
    }
}
