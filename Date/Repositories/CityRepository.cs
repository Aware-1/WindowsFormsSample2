﻿
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
    public class CityRepository : ICityRepository
    {
        private ICityRepository _cityRepository;
        private string connectionString = "Data Source=.;Initial Catalog=ClientSample;Integrated Security=True;MultipleActiveResultSets=true";
        SqlConnection connection;
        public CityRepository()
        {
            connection = new SqlConnection(connectionString);
        }

        public int GetCityIdByName(string cityName)
        {
            int cityId = -1;

            if (connection.State != System.Data.ConnectionState.Open)
                connection.Open();

            string query = "SELECT Id FROM Cities WHERE Name = @CityName";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CityName", cityName);

            if(connection.State != System.Data.ConnectionState.Open)
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                cityId = (int)reader["Id"];
            }
            reader.Close();


            return cityId;
        }

        public string GetCityNameById(int cityId)
        {
            string cityName = string.Empty;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Name FROM Cities WHERE Id = @CityId"; SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CityId", cityId);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                { cityName = reader["Name"].ToString(); }
                reader.Close();
            }
            return cityName;
        }

        public void AddUserToDatabase(string name, DateTime birthDate, bool marriage, int cityId)

        {
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

        public List<string> GetCityNames()

        {
            List<string> cityNames = new List<string>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Name FROM Cities";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    cityNames.Add(reader["Name"].ToString());
                }
                reader.Close();
            }
            return cityNames;
        }

        public List<City> GetCities()
        {
            List<City> cities = new List<City>();
            string query = @" SELECT Id, Name, Province FROM Cities WHERE IsDelete = 0";

            if (connection.State != System.Data.ConnectionState.Open)
                connection.Open();

            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                cities.Add(new City
                {
                    Id = (int)reader["Id"],
                    Name = reader["Name"].ToString(),
                    Province = reader["Province"].ToString()
                });
            }
            reader.Close();

            return cities;
        }

        public List<City> GetCities(string searchText = "")
        {
            List<City> cities = new List<City>();
           
            string query = @" SELECT Id, Name, Province FROM Cities WHERE IsDelete = 0 " 
               + (string.IsNullOrEmpty(searchText) ? "" : $"AND Name LIKE N'%{searchText}%'");
            
            if (connection.State != System.Data.ConnectionState.Open)
                connection.Open();

            SqlCommand command = new SqlCommand(query, connection);
          
             SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                cities.Add(new City
                {
                    Id = (int)reader["Id"],

                    Name = reader["Name"].ToString(),
                    Province = reader["Province"].ToString()
                });
            }
            reader.Close();

            return cities;
        }
    }
}
