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
        void DeleteAdmin(int adminId);
        Admin GetAdmin(int adminId);
        Admin GetAdmin(string userName, string password);
        void UpdateAdmin(int adminId, string userName, bool isLimit, bool active);
         List<Admin> GetAdmins(string searchText = null);
       
    }
}
