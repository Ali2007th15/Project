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
    public class AddOrderService : IAddOrderService
    {
        private readonly TrendyolDbContext _context;

        public AddOrderService(TrendyolDbContext context)
        {
            _context = context;
        }

        public ProductsForOrder AddProductOrder(string name, string description, decimal price, int count, string category, byte[] _image)
        {
            ProductsForOrder newProductOrder = new()
            {
                Name = name,
                Description = description,
                Price = price,
                Count = count,
                Category = category,
                Image = _image
            };
            return newProductOrder;
        }
    }
}
