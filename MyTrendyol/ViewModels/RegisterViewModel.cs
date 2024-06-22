using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Humanizer;
using Microsoft.Data.SqlClient;
using MyTrendyol.Contexts;
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
    public class RegisterViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IUserService _userService;
        private readonly TrendyolDbContext _context;

        private string _name;
        public string Name
        {
            get => _name; set
            {
                if (Regex.IsMatch(value, "^[A-Z][a-z]+$") || string.IsNullOrEmpty(value))
                {
                    Set(ref _name, value);
                }
                else
                {
                    MessageBox.Show("Invalid name");
                    return;
                }
            }
        }
        private string _surname;
        public string Surname
        {
            get => _surname; set
            {
                if (Regex.IsMatch(value, "^[A-Z][a-z]+$") || string.IsNullOrEmpty(value))
                {
                    Set(ref _surname, value);
                }
                else
                {
                    MessageBox.Show("Invalid surname");
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
        private string _email;
        public string Email
        {
            get => _email; set
            {
                if (Regex.IsMatch(value, "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$") || string.IsNullOrEmpty(value))
                {
                    Set(ref _email, value);
                }
                else
                {
                    MessageBox.Show("Invalid mail");
                    return;
                }
            }
        }
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
        private string _password;
        public string Password
        {
            get => _password; set
            {
                if (Regex.IsMatch(value, @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[!@#$%^&*()-_+=])[A-Za-z\d!@#$%^&*()-_+=]{8,}$") || string.IsNullOrEmpty(value))
                {
                    Set(ref _password, value);
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

        public RegisterViewModel(INavigationService navigationService, IUserService userService, TrendyolDbContext context)
        {
            _navigationService = navigationService;
            _userService = userService;
            _context = context;
        }

        public RelayCommand Register
        {
            get => new(async() =>
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(Name) && string.IsNullOrWhiteSpace(Surname)
                        && string.IsNullOrWhiteSpace(Login) && string.IsNullOrWhiteSpace(Email)
                        && string.IsNullOrWhiteSpace(Phone) && string.IsNullOrWhiteSpace(Password)
                        && string.IsNullOrWhiteSpace(TryPassword))
                    {
                        MessageBox.Show("Fields cannot be empty");
                        return;
                    }
                    if (_context.Users.Any(u => u.Login == Login || u.Email == Email || u.Phone == Phone))
                    {
                        MessageBox.Show("A user with such data already exists in the database");
                        return;
                    }
                    else if (TryPassword != Password)
                    {
                        MessageBox.Show("You entered the repeated code incorrectly");
                        return;
                    }
                    var newUser = _userService.UserRegister(Name, Surname, Login, Email, Password, Phone);
                    await _context.Users.AddAsync(newUser);
                    await _context.SaveChangesAsync();
                    MessageBox.Show("Registration has been successfully completed");
                    await _navigationService.NavigateTo<LoginViewModel>();
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
            Name = "";
            Surname = "";
            Login = "";
            Email = "";
            Password = "";
            TryPassword = "";
            Phone = "";
        }

        public RelayCommand LoginView
        {
            get => new(async () =>
            {
                await _navigationService.NavigateTo<LoginViewModel>();
            });
        }
    }
}
