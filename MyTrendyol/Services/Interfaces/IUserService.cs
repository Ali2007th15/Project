using MyTrendyol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTrendyol.Services.Interfaces
{
    public interface IUserService
    {
        public Task<User> GetUser(string email);
        public Task<bool> UserLogin(string email, string password);
        public User UserRegister(string name, string surname, string login, string email, string password, string phone);
    }
}
