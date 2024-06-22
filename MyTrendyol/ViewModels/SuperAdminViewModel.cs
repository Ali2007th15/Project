using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MyTrendyol.Contexts;
using MyTrendyol.Models;
using MyTrendyol.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTrendyol.ViewModels
{
    public class SuperAdminViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly TrendyolDbContext _context;
        private ObservableCollection<User> _users;
        private ObservableCollection<Admin> _admin;
        private ObservableCollection<Order> _orders;

        public ObservableCollection<User> Users
        {
            get => _users; set => Set(ref _users, value);
        }

        public ObservableCollection<Admin> Admin
        {
            get => _admin; set => Set(ref _admin, value);
        }

        public ObservableCollection<Order> Orders
        {
            get => _orders; set => Set(ref _orders, value);
        }

        public SuperAdminViewModel(INavigationService navigationService, TrendyolDbContext context)
        {
            _navigationService = navigationService;
            _context = context;
            Users = new ObservableCollection<User>(_context.Users);
            Admin = new ObservableCollection<Admin>(_context.Admins);
            Orders = new ObservableCollection<Order>(_context.Orders);
        }

        public RelayCommand Exit
        {
            get => new(async () =>
            {
                await _navigationService.NavigateTo<LoginViewModel>();
            });
        }
        public RelayCommand AddProduct
        {
            get => new(async () =>
            {
                await _navigationService.NavigateTo<AddProductSuperAdminViewModel>();
            });
        }

        public RelayCommand AddUser
        {
            get => new(async () =>
            {
                await _navigationService.NavigateTo<AddUserSuperAdminViewModel>();
            });
        }

        public RelayCommand AddAdmin
        {
            get => new(async () =>
            {
                await _navigationService.NavigateTo<AddAdminSuperAdminViewModel>();
            });
        }

        public RelayCommand RemoveProduct
        {
            get => new(async () =>
            {
                await _navigationService.NavigateTo<RemoveProductSuperAdminViewModel>();
            });
        }

        public RelayCommand RemoveUser
        {
            get => new(async () =>
            {
                await _navigationService.NavigateTo<RemoveUserSuperAdminViewModel>();
            });
        }

        public RelayCommand RemoveAdmin
        {
            get => new(async () =>
            {
                await _navigationService.NavigateTo<RemoveAdminSuperAdminViewModel>();
            });
        }
    }
}
