using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MaterialDesignThemes.Wpf;
using Microsoft.Data.SqlClient;
using MyTrendyol.Contexts;
using MyTrendyol.Services.Classes;
using MyTrendyol.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyTrendyol.ViewModels
{
    public class PayViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly TrendyolDbContext _context;
        private readonly CurrentUserService _currentUserService;
        private decimal _amount;
        public decimal Amount
        {
            get => _amount; set => Set(ref _amount, value);
        }

        public PayViewModel(INavigationService navigationService, TrendyolDbContext context, CurrentUserService currentUserService)
        {
            _navigationService = navigationService;
            _context = context;
            _currentUserService = currentUserService;
        }

        public RelayCommand Back
        {
            get => new(async () =>
            {
                await _navigationService.NavigateTo<BalanceViewModel>();
            });
        }

        public RelayCommand Pay
        {
            get => new(async () =>
            {
                try
                {
                    var user = _context.Users.FirstOrDefault(u => u.UserId == _currentUserService.UserId);
                    if (user != null)
                    {
                        if (Amount == 0)
                        {
                            MessageBox.Show("Enter value");
                            return;
                        }
                        user.Balance += Amount;
                        await _context.SaveChangesAsync();
                        MessageBox.Show($"The balance has been successfully replenished on {Amount} AZN");
                        _currentUserService.Balance = user.Balance;
                        Amount = 0;
                        await _navigationService.NavigateTo<BalanceViewModel>();
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
