using Doamin.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Date.Service
{
    public class UserService: IUserService
    {
        private readonly IUserService _userService;
        public UserService()
        {

        }



        public async Task<List<Client>> LoadJson()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://jsonplaceholder.typicode.com/users");
            response.EnsureSuccessStatusCode(); string json = await response.Content.ReadAsStringAsync();
            List<Client> users = JsonConvert.DeserializeObject<List<Client>>(json);
            return users;
        }


    }
}
