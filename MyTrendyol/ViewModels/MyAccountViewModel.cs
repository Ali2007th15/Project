using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MyTrendyol.Contexts;
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
    public class MyAccountViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly TrendyolDbContext _context;
        private readonly CurrentUserService _currentUserService;
        private readonly User user;

        private string _name;
        public string Name
        {
            get => _name; set => Set(ref _name, value);
        }
        private string _surname;
        public string Surname
        {
            get => _surname; set => Set(ref _surname, value);
        }
        private string _email;
        public string Email
        {
            get => _email; set => Set(ref _email, value);
        }
        private string _phone;
        public string Phone
        {
            get => _phone; set => Set(ref _phone, value);
        }
        public MyAccountViewModel(INavigationService navigationService, TrendyolDbContext context, CurrentUserService currentUserService)
        {
            _navigationService = navigationService;
            _context = context;
            _currentUserService = currentUserService;
            user = new();
            _currentUserService.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(CurrentUserService.Name))
                {
                    Name = _currentUserService.Name;
                }
                else if (args.PropertyName == nameof(CurrentUserService.Surname))
                {
                    Surname = _currentUserService.Surname;
                }
                else if (args.PropertyName == nameof(CurrentUserService.Email))
                {
                    Email = _currentUserService.Email;
                }
                else if (args.PropertyName == nameof(CurrentUserService.Phone))
                {
                    Phone = _currentUserService.Phone;
                }
            };
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
