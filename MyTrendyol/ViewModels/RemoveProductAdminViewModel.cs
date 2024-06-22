using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Data.SqlClient;
using MyTrendyol.Contexts;
using MyTrendyol.Models;
using MyTrendyol.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyTrendyol.ViewModels
{
    public class RemoveProductAdminViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly TrendyolDbContext _context;
        private ObservableCollection<ProductsForOrder> _productsForOrder;
        private ProductsForOrder _selectedProductsForOrder;

        public ObservableCollection<ProductsForOrder> ProductsForOrder
        {
            get => _productsForOrder; set => Set(ref _productsForOrder, value);
        }

        public ProductsForOrder SelectedProductsForOrder
        {
            get => _selectedProductsForOrder; set => Set(ref _selectedProductsForOrder, value);
        }

        public RemoveProductAdminViewModel(INavigationService navigationService, TrendyolDbContext context)
        {
            _navigationService = navigationService;
            _context = context;
            ProductsForOrder = new ObservableCollection<ProductsForOrder>(_context.ProductsForOrders);
        }

        public RelayCommand Back
        {
            get => new(async () =>
            {
                await _navigationService.NavigateTo<AdminViewModel>();
            });
        }

        public RelayCommand Remove
        {
            get => new(async () =>
            {
                try
                {
                    if (_selectedProductsForOrder != null)
                    {
                        var productsForOrder = _context.ProductsForOrders.FirstOrDefault(p => p.Name == _selectedProductsForOrder.Name);
                        if (productsForOrder != null)
                        {
                            var wareHouseProduct = _context.WareHouse.FirstOrDefault(w => w.ProductId == _selectedProductsForOrder.Id);
                            if (wareHouseProduct != null)
                            {
                                _context.ProductsForOrders.Remove(productsForOrder);
                                _context.SaveChanges();
                            }
                            _context.WareHouse.Remove(wareHouseProduct);
                            _context.SaveChanges();
                            ProductsForOrder.Remove(productsForOrder);
                            MessageBox.Show("The product has been successfully removed from the warehouse");
                            await _navigationService.NavigateTo<AdminViewModel>();
                            SelectedProductsForOrder = null;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Choice product");
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
