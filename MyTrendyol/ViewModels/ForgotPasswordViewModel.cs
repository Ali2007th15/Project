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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace MyTrendyol.ViewModels
{
    public class ForgotPasswordViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IForgotPasswordService _forgotPasswordService;
        private readonly TrendyolDbContext _context;

        private string _phone;
        public string Phone
        {
            get => _phone; set
            {
                if (Regex.IsMatch(value, "^(\\+994\\d{9}|\\d{10})$") || string.IsNullOrEmpty(value))
                {
                    Set(ref _phone, value);
                }
                else
                {
                    MessageBox.Show("Invalid phone");
                    return;
                }
            }
        }
        private string _login;
        public string Login
        {
            get => _login; set
            {
                if (Regex.IsMatch(value, "^[a-zA-Z0-9_-]{3,16}$") || string.IsNullOrEmpty(value))
                {
                    Set(ref _login, value);
                }
                else
                {
                    MessageBox.Show("Invalid login");
                    return;
                }
            }
        }
        private string _newPassword;
        public string NewPassword
        {
            get => _newPassword; set
            {
                if (Regex.IsMatch(value, @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[!@#$%^&*()-_+=])[A-Za-z\d!@#$%^&*()-_+=]{8,}$") || string.IsNullOrEmpty(value))
                {
                    Set(ref _newPassword, value);
                }
                else
                {
                    MessageBox.Show("The password must contain at least 8 characters, " +
                    " including at least one uppercase letter, one lowercase letter, one digit, and one special character");
                    return;
                }
            }
        }
        private string _tryPassword;
        public string TryPassword
        {
            get => _tryPassword; set => Set(ref _tryPassword, value);
        }

        public ForgotPasswordViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _context = new TrendyolDbContext();
            _forgotPasswordService = new ForgotPasswordService(_context);
        }

        public RelayCommand Reset
        {
            get => new(async () =>
            {
                try
                {
                    if (string.IsNullOrEmpty(Phone) && string.IsNullOrEmpty(Login)
                    && string.IsNullOrEmpty(NewPassword) && string.IsNullOrEmpty(TryPassword))
                    {
                        MessageBox.Show("Fields cannot be empty");
                        return;
                    }
                    if (!_context.Users.Any(u => u.Phone == Phone || u.Login == Login))
                    {
                        MessageBox.Show("User not found");
                        return;
                    }
                    else if (TryPassword != NewPassword)
                    {
                        MessageBox.Show("You entered the repeated code incorrectly");
                        return;
                    }
                    await _forgotPasswordService.ForgotPasswordAsync(Phone, Login, NewPassword);
                    await _context.SaveChangesAsync();
                    await _navigationService.NavigateTo<LoginViewModel>();
                    MessageBox.Show("The password has been successfully recovered");
                    ClearField();
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
        public void ClearField()
        {
            Login = "";
            Phone = "";
            NewPassword = "";
            TryPassword = "";
        }

        public RelayCommand Back
        {
            get => new(async () =>
            {
                await _navigationService.NavigateTo<LoginViewModel>();
            });
        }
    }
}
