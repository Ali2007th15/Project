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
    public class AddAdminSuperAdminViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly TrendyolDbContext _context;
        private readonly AdminService _adminservice;

        private string _username;
        private string _password;
        private string _trypassword;


        public string Username
        {
            get => _username; set => Set(ref _username, value);
        }

        public string Password
        {
            get => _password; set => Set(ref _password, value);
        }
        public string TryPassword
        {
            get => _trypassword; set => Set(ref _trypassword, value);
        }

        public AddAdminSuperAdminViewModel(INavigationService navigationService, TrendyolDbContext context)
        {
            _navigationService = navigationService;
            _context = context;
            _adminservice = new AdminService(_context);
        }

        public RelayCommand Back
        {
            get => new(async () =>
            {
                await _navigationService.NavigateTo<SuperAdminViewModel>();
            });
        }

        public RelayCommand AddAdmin
        {
            get => new(async () =>
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
                    {
                        MessageBox.Show("Fields cannot be empty");
                        return;
                    }
                    else if (_context.Admins.Any(a => a.Name == Username))
                    {
                        MessageBox.Show("The admin with such data is already in the database");
                        return;
                    }
                    else if (TryPassword != Password)
                    {
                        MessageBox.Show("You entered the repeated password incorrectly");
                        return;
                    }
                    var newadmin = await _adminservice.AdminRegister(Username, Password);
                    _context.Admins.Add(newadmin);
                    await _context.SaveChangesAsync();
                    await _navigationService.NavigateTo<SuperAdminViewModel>();
                    MessageBox.Show("The admin has been successfully created");
                    Password = "";
                    TryPassword = "";
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
