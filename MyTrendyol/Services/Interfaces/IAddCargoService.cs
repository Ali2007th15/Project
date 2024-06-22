using MyTrendyol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTrendyol.Services.Interfaces
{
    public interface IAddCargoService
    {
        public Product AddProduct(int userId, string name, string description, decimal price, string category, int count);
    }
}
