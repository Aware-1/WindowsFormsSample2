using Doamin.Entities;
using Doamin.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Date.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ICityRepository _cityRepository;

        private IUserRepository _userRepository;
        private string connectionString = "Data Source=.;Initial Catalog=ClientSample;Integrated Security=True;MultipleActiveResultSets=true";
        SqlConnection connection;
        public UserRepository()
        {
            //_userRepository = userRepository;
            connection = new SqlConnection(connectionString);
        }


        public void AddUser(User user)
        {
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
            List<User> users = new List<User>();

            string query = @"
             SELECT Users.Id, Users.Name, Users.BirthDate, Users.marriage, Users.CityId, Cities.Name AS CityName
             FROM Users
             JOIN Cities ON Users.CityId = Cities.Id
             WHERE Users.IsDelete = 0";

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


        public List<User> GetUsers(string searchText = "", string selectedCity = "")
        {
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
    }
}
