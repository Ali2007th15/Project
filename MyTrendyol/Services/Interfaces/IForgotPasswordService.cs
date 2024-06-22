using MyTrendyol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTrendyol.Services.Interfaces
{
    public interface IForgotPasswordService
    {
        public Task<User> ForgotPasswordAsync(string phone, string login, string password);
    }
}
