using Doamin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Date.Service
{
    public interface IUserService
    {
        Task<List<Client>> LoadJson();
    }
}
