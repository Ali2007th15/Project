using MyTrendyol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTrendyol.Services.Interfaces
{
    public interface IAddOrderService
    {
        public ProductsForOrder AddProductOrder(string name, string description, decimal price, int count, string category, byte[] _image);
    }
}
