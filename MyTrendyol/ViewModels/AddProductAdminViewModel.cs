using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Data.SqlClient;
using Microsoft.Win32;
using MyTrendyol.Contexts;
using MyTrendyol.Models;
using MyTrendyol.Services.Classes;
using MyTrendyol.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace MyTrendyol.ViewModels
{
    public class AddProductAdminViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IAddOrderService _addOrderService;
        private readonly TrendyolDbContext _context;
        private readonly ProductsForOrder _product;
        private BitmapImage _imageBox;
        private string _name;
        private string _description;
        private decimal _price;
        private int _count;
        private string _category;
        public string Name
        {
            get => _name; set => Set(ref _name, value);
        }

        public string Description
        {
            get => _description; set => Set(ref _description, value);
        }

        public decimal Price
        {
            get => _price; set => Set(ref _price, value);
        }

        public int Count
        {
            get => _count; set => Set(ref _count, value);
        }

        public string Category
        {
            get => _category; set => Set(ref _category, value);
        }

        public BitmapImage ImageBox
        {
            get => _imageBox; set => Set(ref _imageBox, value);
        }

        public AddProductAdminViewModel(INavigationService navigationService, TrendyolDbContext context)
        {
            _navigationService = navigationService;
            _context = context;
            _addOrderService = new AddOrderService(_context);
            _product = new ProductsForOrder();
        }

        public RelayCommand Back
        {
            get => new(async () =>
            {
                await _navigationService.NavigateTo<AdminViewModel>();
            });
        }
        public RelayCommand Image
        {
            get => new(async () =>
            {
                OpenFileDialog file = new();
                file.Filter = "Photo (*.jpg;*.jpeg;*.png;*.gif) | *.jpg;*jpeg;*.png;*.gif |Все файлы (*.*) | *.*";
                if (file.ShowDialog() == true)
                {
                    _product.Image = File.ReadAllBytes(file.FileName);
                    ImageBox = new BitmapImage(new Uri(file.FileName));
                }
            });
        }

        public RelayCommand Add
        {
            get => new(async () =>
            {
                try
                {
                    if (_product.Image == null)
                    {
                        MessageBox.Show("Choice image");
                        return;
                    }
                    else if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Description) ||
                            decimal.IsNegative(Price) || int.IsNegative(Count) ||
                            string.IsNullOrWhiteSpace(Category))
                    {
                        MessageBox.Show("Fields cannot be empty");
                        return;
                    }
                    var product = _addOrderService.AddProductOrder(Name, Description, Price, Count, Category, _product.Image);
                    if (product != null)
                    {
                        _context.ProductsForOrders.AddRange(product);
                        _context.SaveChanges();
                        WareHouse wareHouse = new()
                        {
                            ProductId = product.Id,
                            ProductCount = product.Count,
                            Name = product.Name
                        };
                        _context.WareHouse.Add(wareHouse);
                        _context.SaveChanges();

                        MessageBox.Show("The product has been successfully added to the warehouse");
                        await _navigationService.NavigateTo<AdminViewModel>();
                        ClearField();
                    }
                    else
                    {
                        MessageBox.Show("Error");
                        return;
                    }
                }
                catch (SqlException sql)
                {
                    MessageBox.Show(sql.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });
        }
        private void ClearField()
        {
            Name = "";
            Description = "";
            Price = 0;
            Count = 0;
            Category = "";
            _product.Image = null;
        }
    }
}
