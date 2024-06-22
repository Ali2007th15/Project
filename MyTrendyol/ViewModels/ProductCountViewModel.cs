using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Data.SqlClient;
using MyTrendyol.Contexts;
using MyTrendyol.Models;
using MyTrendyol.Services.Classes;
using MyTrendyol.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyTrendyol.ViewModels
{
    public class ProductCountViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IMessenger _messenger;
        private readonly IAddCargoService _addCargoService;
        private readonly TrendyolDbContext _context;
        private readonly CurrentUserService _currentUserService;
        private ObservableCollection<ProductsForOrder> _products;
        private ProductsForOrder _selectedProducts;
        private string _product;
        private int _count;

        public int Count
        {
            get => _count; set => Set(ref _count, value);
        }

        public string Product
        {
            get => _product; set => Set(ref _product, value);
        }

        public ObservableCollection<ProductsForOrder> Products
        {
            get => _products; set => Set(ref _products, value);
        }

        public ProductsForOrder SelectedProducts
        {
            get => _selectedProducts; set => Set(ref _selectedProducts, value);
        }

        public ProductCountViewModel(INavigationService navigationService, IMessenger messenger, TrendyolDbContext context, CurrentUserService currentUserService)
        {
            _navigationService = navigationService;
            _messenger = messenger;
            _context = context;
            _currentUserService = currentUserService;
            _selectedProducts = new ProductsForOrder();
            _addCargoService = new AddCargoService(_context);
            _messenger.Register<ProductsForOrder>(this, "SelectedProduct", (product) =>
            {
                SelectedProducts = product;
            });
        }

        public RelayCommand Back
        {
            get => new(async () =>
            {
                await _navigationService.NavigateTo<BuyProductViewModel>();
            });
        }
        public RelayCommand BuyProduct
        {
            get => new(async () =>
            {
                try
                {
                    if (_selectedProducts != null)
                    {
                        var wareHouseProduct = _context.WareHouse.FirstOrDefault(p => p.ProductId == _selectedProducts.Id);
                        if (wareHouseProduct != null)
                        {
                            if (wareHouseProduct.ProductCount < Count)
                            {
                                MessageBox.Show("There is not so much quantity of this product in stock.");
                                Count = 0;
                                await _navigationService.NavigateTo<BuyProductViewModel>();
                                return;
                            }
                            else if (Count == 0)
                            {
                                MessageBox.Show("Enter count");
                                return;
                            }
                            else if (_currentUserService.Balance < _selectedProducts.Price * Count)
                            {
                                MessageBox.Show("Insufficient funds");
                                await _navigationService.NavigateTo<BuyProductViewModel>();
                                return;
                            }
                        }
                        var product = _addCargoService.AddProduct(_currentUserService.UserId, SelectedProducts.Name, SelectedProducts.Description, SelectedProducts.Price, SelectedProducts.Category, Count);
                        Order order = new()
                        {
                            UserId = _currentUserService.UserId,
                            Product = product.Name,
                            ProductsCount = Count,
                            Created = DateTime.Now,
                        };

                        _context.Products.Add(product);
                        await _context.SaveChangesAsync();

                        _context.Orders.Add(order);
                        await _context.SaveChangesAsync();

                        var user = _context.Users.FirstOrDefault(u => u.UserId == _currentUserService.UserId);
                        if (user != null)
                        {
                            user.Balance -= _selectedProducts.Price * Count;
                            await _context.SaveChangesAsync();
                        }
                        _currentUserService.Balance -= _selectedProducts.Price * Count;
                        await _context.SaveChangesAsync();

                        wareHouseProduct.ProductCount -= Count;
                        await _context.SaveChangesAsync();

                        _selectedProducts.Count -= Count;
                        await _context.SaveChangesAsync();

                        MessageBox.Show("The product was successfully purchased");
                        Count = 0;
                        await _navigationService.NavigateTo<BuyProductViewModel>();
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
    }
}
