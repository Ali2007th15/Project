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
    public class AddCargoService : IAddCargoService
    {
        private readonly TrendyolDbContext _context;

        public AddCargoService(TrendyolDbContext context)
        {
            _context = context;
        }

        public Product AddProduct(int userId, string name, string description, decimal price, string category, int count)
        {
            Product newProduct = new()
            {
                UserId = userId,
                Name = name,
                Description = description,
                Price = price,
                Category = category,
                Count = count
            };
            return newProduct;
        }
    }
}
