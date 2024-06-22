using MyTrendyol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTrendyol.Services.Interfaces
{
    public interface IAdminService
    {
        public Task<bool> AdminLogin(string name, string password);
        public Task<Admin> AdminRegister(string username, string password);
    }
}
