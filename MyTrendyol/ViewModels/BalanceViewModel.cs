using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MyTrendyol.Models;
using MyTrendyol.Services.Classes;
using MyTrendyol.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTrendyol.ViewModels
{
    public class BalanceViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly CurrentUserService _currentUserService;
        private readonly User user = new();
        private decimal _balance;
        public decimal Balance
        {
            get => _balance; set => Set(ref _balance, value);
        }

        public BalanceViewModel(INavigationService navigationService, CurrentUserService currentUserService)
        {
            _navigationService = navigationService;
            _currentUserService = currentUserService;

            _currentUserService.PropertyChanged += (sender, args) =>
            {
                Balance = _currentUserService.Balance;
            };
            _currentUserService.UpdateUserData(user);
        }

        public RelayCommand Back
        {
            get => new(async () =>
            {
                await _navigationService.NavigateTo<MainViewModel>();
            });
        }

        public RelayCommand Pay
        {
            get => new(async () =>
            {
                await _navigationService.NavigateTo<PayViewModel>();
            });
        }
    }
}
