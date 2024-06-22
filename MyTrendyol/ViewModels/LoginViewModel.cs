using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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
    public class LoginViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly UserService _userService;
        private readonly AdminService _adminService;
        private readonly CurrentUserService _currentUserService;
        private readonly TrendyolDbContext _context;
        private string _email;
        public string Email
        {
            get => _email;
            set => Set(ref _email, value);
        }
        private string _password;
        public string Password
        {
            get => _password;
            set => Set(ref _password, value);
        }


        public LoginViewModel(INavigationService navigationService,  CurrentUserService currentUserService)
        {
            _context = new TrendyolDbContext();
            _navigationService = navigationService;
            _currentUserService = currentUserService;
            _userService = new UserService(_context);
            _adminService = new AdminService(_context);
        }

        public RelayCommand Login
        {
            get => new(async () =>
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(Email) && string.IsNullOrWhiteSpace(Password))
                    {
                        MessageBox.Show("Fields cannot be empty");
                        return;
                    }
                    if (await _adminService.AdminLogin(Email, Password))
                    {
                        await _navigationService.NavigateTo<AdminViewModel>();
                    }
                    else if (await _userService.UserLogin(Email, Password))
                    {
                        var user = await _userService.GetUser(Email);
                        _currentUserService.UpdateUserData(user);
                        await _navigationService.NavigateTo<MainViewModel>();
                    }
                    else if (Email == "ali2007" && Password == "ali2007")
                    {
                        await _navigationService.NavigateTo<SuperAdminViewModel>();
                    }
                    else
                    {
                        MessageBox.Show("Incorrent Password");
                        return;
                    }
                    Email = "";
                    Password = "";
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

        public RelayCommand Register
        {
            get => new(async () =>
            {
                await _navigationService.NavigateTo<RegisterViewModel>();
            });
        }

        public RelayCommand Forgot
        {
            get => new(async () =>
            {
                await _navigationService.NavigateTo<ForgotPasswordViewModel>();
            });
        }
    }
}
