using Doamin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doamin.Interfaces
{
    public interface ICityRepository
    {
        int GetCityIdByName(string cityName);

        void AddUserToDatabase(string name, DateTime birthDate, bool marriage, int cityId);
       
        List<string> GetCityNames();

        string GetCityNameById(int cityId);

        List<City> GetCities();

        List<City> GetCities(string searchText = "");
    }
}
