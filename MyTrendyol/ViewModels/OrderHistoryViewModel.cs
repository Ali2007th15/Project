using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MyTrendyol.Contexts;
using MyTrendyol.Models;
using MyTrendyol.Services.Classes;
using MyTrendyol.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyTrendyol.ViewModels
{
    public class OrderHistoryViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly TrendyolDbContext _context;
        private readonly CurrentUserService _currentUserService;
        private ObservableCollection<Order> _order;
        private Order _selectedOrder;

        public ObservableCollection<Order> Order
        {
            get => _order; set => Set(ref _order, value);
        }
        public Order SelectedOrder
        {
            get => _selectedOrder; set => Set(ref _selectedOrder, value);
        }

        public OrderHistoryViewModel(INavigationService navigationService, TrendyolDbContext context, CurrentUserService currentUserService)
        {
            _navigationService = navigationService;
            _context = context;
            _currentUserService = currentUserService;
            Order = new ObservableCollection<Order>(_context.Orders.Where(o => o.UserId == _currentUserService.UserId));
        }

        public RelayCommand Back
        {
            get => new(async () =>
            {
                await _navigationService.NavigateTo<MainViewModel>();
            });
        }
    }
}
