using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Data.SqlClient;
using MyTrendyol.Contexts;
using MyTrendyol.Models;
using MyTrendyol.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyTrendyol.ViewModels
{
    public class RemoveUserSuperAdminViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly TrendyolDbContext _context;
        private ObservableCollection<User> _users;
        private User _selectedUsers;

        public ObservableCollection<User> Users
        {
            get => _users; set => Set(ref _users, value);
        }

        public User SelectedUsers
        {
            get => _selectedUsers; set => Set(ref _selectedUsers, value);
        }

        public RemoveUserSuperAdminViewModel(INavigationService navigationService, TrendyolDbContext context)
        {
            _navigationService = navigationService;
            _context = context;
            Users = new ObservableCollection<User>(_context.Users);
        }

        public RelayCommand Back
        {
            get => new(async () =>
            {
                await _navigationService.NavigateTo<SuperAdminViewModel>();
            });
        }

        public RelayCommand Remove
        {
            get => new(async () =>
            {
                try
                {
                    if (SelectedUsers == null)
                    {
                        MessageBox.Show("Choice user");
                        return;
                    }
                    var user = _context.Users.FirstOrDefault(u => u.UserId == _selectedUsers.UserId);
                    _context.Users.Remove(user);
                    _context.SaveChanges();
                    Users.Remove(user);
                    MessageBox.Show("The user has been successfully deleted ");
                    await _navigationService.NavigateTo<SuperAdminViewModel>();
                    SelectedUsers = null;
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
