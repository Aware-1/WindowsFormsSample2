using Doamin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doamin.Interfaces
{
    public interface IAdminRepository
    {
        bool ValidateUser(string userName, string password);
        List<Admin> GetUsers();

        Admin GetAdminDetails(int adminId);

        void UpdateAdmin(int adminId, string userName, bool isLimit, bool active);
    }
}
