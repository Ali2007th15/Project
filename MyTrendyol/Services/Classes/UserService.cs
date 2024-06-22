using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using MyTrendyol.Contexts;
using MyTrendyol.Models;
using MyTrendyol.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTrendyol.Services.Classes
{
    public class UserService : IUserService
    {
        private readonly TrendyolDbContext _context;

        public UserService(TrendyolDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUser(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> UserLogin(string email, string password)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user != null)
            {
                return BCrypt.Net.BCrypt.Verify(password, user.Password);
            }
            return false;
        }

        public User UserRegister(string name, string surname, string login, string email, string password, string phone)
        {
            User newUser = new()
            {
                Name = name,
                Surname = surname,
                Login = login,
                Email = email,
                Password = BCrypt.Net.BCrypt.HashPassword(password),
                Phone = phone
            };
            return newUser;
        }
    }
}
