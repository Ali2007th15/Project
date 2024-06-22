using Microsoft.EntityFrameworkCore;
using MyTrendyol.Contexts;
using MyTrendyol.Models;
using MyTrendyol.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace MyTrendyol.Services.Classes
{
    public class ForgotPasswordService : IForgotPasswordService
    {
        private readonly TrendyolDbContext _context;

        public ForgotPasswordService(TrendyolDbContext context)
        {
            _context = context;
        }

        public async Task<User> ForgotPasswordAsync(string phone, string login, string password)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Phone == phone);
            if (user != null)
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(password);
            }
            return user;
        }
    }
}
