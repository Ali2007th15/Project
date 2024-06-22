using Microsoft.EntityFrameworkCore;
using MyTrendyol.Contexts;
using MyTrendyol.Models;
using MyTrendyol.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace MyTrendyol.Services.Classes
{
    public class AdminService : IAdminService
    {
        private readonly TrendyolDbContext _context;

        public AdminService(TrendyolDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AdminLogin(string name, string password)
        {
            Admin? admin = await _context.Admins.FirstOrDefaultAsync(a => a.Name == name);
            if (admin != null)
            {
                return BCrypt.Net.BCrypt.Verify(password, admin.Password);
            }
            return false;
        }

        public async Task<Admin> AdminRegister(string username, string password)
        {
            Admin newAdmin = new()
            {
                Name = username,
                Password = BCrypt.Net.BCrypt.HashPassword(password)
            };
            return newAdmin;
        }
    }
}
