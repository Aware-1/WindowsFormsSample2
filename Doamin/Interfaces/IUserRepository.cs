﻿using Doamin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doamin.Interfaces
{
    public interface IUserRepository
    {
        void AddUser(User u);
        
        List<User> GetUsers();

        List<User> GetUsers(string searchText = null, string selectedCity = null);
        void AddUserToDatabase(string name, DateTime birthDate, bool marriage, int cityId);
       void DeleteUser(int userId);
        void InsertUser(string name, DateTime birthDate, bool marriage, int cityId);
    }
}
