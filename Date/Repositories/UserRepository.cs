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
        private IUserRepository _userRepository;
        private string connectionString = "Data Source=.;Initial Catalog=Clients;Integrated Security=True;MultipleActiveResultSets=true";
        public UserRepository(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public void AddUser(User user)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Users (Name, BirthDate, marriage, CityId) VALUES (@Name, @BirthDate, @marriage, @CityId)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", user.Name);
                command.Parameters.AddWithValue("@BirthDate", user.BirthDate);
                command.Parameters.AddWithValue("@marriage", user.marriage);
                command.Parameters.AddWithValue("@CityId", user.CityId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public List<User> GetUsers()
        {
            List<User> users = new List<User>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, Name, BirthDate, marriage, CityId FROM Users";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        User user = new User
                        {
                            Id = (int)reader["Id"],
                            Name = reader["Name"].ToString(),
                            BirthDate = (DateTime)reader["BirthDate"],
                            marriage = (bool)reader["marriage"],
                            CityId = (int)reader["CityId"]
                        };
                        users.Add(user);
                    }
                }
            }

            return users;
        }
    }

}
