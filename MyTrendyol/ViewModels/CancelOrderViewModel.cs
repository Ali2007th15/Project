using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Data.SqlClient;
using MyTrendyol.Contexts;
using MyTrendyol.Models;
using MyTrendyol.Services.Classes;
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
    public class CancelOrderViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly CurrentUserService _currentUserService;
        private readonly TrendyolDbContext _context;
        private ObservableCollection<Order> _order;
        private Order _selectedOrder;

        public ObservableCollection<Order> Order
        {
            get => _order;
            set => Set(ref _order, value);
        }

        public Order SelectedOrder
        {
            get => _selectedOrder;
            set => Set(ref _selectedOrder, value);
        }


        public CancelOrderViewModel(INavigationService navigationService, TrendyolDbContext context, CurrentUserService currentUserService)
        {
            _navigationService = navigationService;
            _currentUserService = currentUserService;
            _context = context;
            Order = new ObservableCollection<Order>(_context.Orders.Where(o => o.UserId == _currentUserService.UserId));
        }

        public RelayCommand Back
        {
            get => new(async () =>
            {
                await _navigationService.NavigateTo<MainViewModel>();
            });
        }

        public RelayCommand Cancel
        {
            get => new(async () =>
            {
                try
                {
                    if (SelectedOrder == null)
                    {
                        MessageBox.Show("Choice order");
                        return;
                    }
                    Order order = _context.Orders.FirstOrDefault(o => o.Product == _selectedOrder.Product);
                    if (_selectedOrder.Status == "Order Placed")
                    {
                        Product product = _context.Products.FirstOrDefault(p => p.Name == _selectedOrder.Product);
                        _context.Products.Remove(product);
                        await _context.SaveChangesAsync();
                        _context.Orders.Remove(order);
                        await _context.SaveChangesAsync();
                        Order.Remove(order);
                        MessageBox.Show("The order has been successfully cancelled");
                        SelectedOrder = null;
                    }
                    else
                    {
                        MessageBox.Show("It is impossible to cancel the order, because it has already left");
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
