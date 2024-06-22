using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Data.SqlClient;
using MyTrendyol.Contexts;
using MyTrendyol.Models;
using MyTrendyol.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyTrendyol.ViewModels
{
    public class RemoveAdminSuperAdminViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly TrendyolDbContext _context;
        private ObservableCollection<Admin> _admin;
        private Admin _selectedAdmin;

        public ObservableCollection<Admin> Admin
        {
            get => _admin; set => Set(ref _admin, value);
        }

        public Admin SelectedAdmin
        {
            get => _selectedAdmin; set => Set(ref _selectedAdmin, value);
        }

        public RemoveAdminSuperAdminViewModel(INavigationService navigationService, TrendyolDbContext context)
        {
            _navigationService = navigationService;
            _context = context;
            Admin = new ObservableCollection<Admin>(_context.Admins);
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
                    if (SelectedAdmin == null)
                    {
                        MessageBox.Show("Choice user");
                        return;
                    }
                    var admin = _context.Admins.FirstOrDefault(u => u.AdminId == _selectedAdmin.AdminId);
                    _context.Admins.Remove(admin);
                    await _context.SaveChangesAsync();
                    Admin.Remove(admin);
                    MessageBox.Show("This admin has been successfully deleted ");
                    await _navigationService.NavigateTo<SuperAdminViewModel>();
                    SelectedAdmin = null;
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
