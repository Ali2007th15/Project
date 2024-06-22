using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
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
    public class BuyProductViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IMessenger _messenger;
        private readonly TrendyolDbContext _context;
        private ObservableCollection<ProductsForOrder> _products;
        private ProductsForOrder _selectedProducts;
        
        public ObservableCollection<ProductsForOrder> Products
        {
            get => _products; set => Set(ref _products, value); 
        }

        public ProductsForOrder SelectedProducts
        {
            get => _selectedProducts; set
            {
                if (Set(ref _selectedProducts, value))
                {
                    _messenger.Send(value, "SelectedProduct");
                }
            }
        }

        public BuyProductViewModel(INavigationService navigationService, IMessenger messenger, TrendyolDbContext context)
        {
            _navigationService = navigationService;
            _messenger = messenger;
            _context = context;
            Products = new ObservableCollection<ProductsForOrder>(_context.ProductsForOrders);
            _messenger.Send(SelectedProducts);
        }

        public RelayCommand Back
        {
            get => new(async () =>
            {
                await _navigationService.NavigateTo<MainViewModel>();
            });
        }

        public RelayCommand Buy
        {
            get => new(async () =>
            {
                try
                {
                    if (SelectedProducts != null)
                    {
                        _messenger.Send(SelectedProducts, "SelectedProduct");
                        await _navigationService.NavigateTo<ProductCountViewModel>();
                    }
                    else
                    {
                        MessageBox.Show("You have not selected the product");
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
