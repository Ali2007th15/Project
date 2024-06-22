using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MyTrendyol.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTrendyol.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public RelayCommand Account
        {
            get => new(async () =>
            {
                await _navigationService.NavigateTo<MyAccountViewModel>();
            });
        }

        public RelayCommand Balance
        {
            get => new(async () =>
            {
                await _navigationService.NavigateTo<BalanceViewModel>();
            });
        }

        public RelayCommand History
        {
            get => new(async () =>
            {
                await _navigationService.NavigateTo<OrderHistoryViewModel>();
            });
        }

        public RelayCommand Cancel
        {
            get => new(async () =>
            {
                await _navigationService.NavigateTo<CancelOrderViewModel>();
            });
        }

        public RelayCommand Buy
        {
            get => new(async () =>
            {
                await _navigationService.NavigateTo<BuyProductViewModel>();
            });
        }

        public RelayCommand Exit
        {
            get => new(async () =>
            {
                await _navigationService.NavigateTo<LoginViewModel>();
            });
        }


    }
}
