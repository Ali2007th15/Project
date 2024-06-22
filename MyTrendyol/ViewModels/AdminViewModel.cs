using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.EntityFrameworkCore;
using MyTrendyol.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using MyTrendyol.Models;
using System.Collections.ObjectModel;
using MyTrendyol.Contexts;

namespace MyTrendyol.ViewModels
{
    public class AdminViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly TrendyolDbContext _context;
        private ObservableCollection<Order> _order;
        private Order _selectedOrder;
        private RadioButtons _radioButtons;

        public ObservableCollection<Order> Order
        {
            get => _order; set => Set(ref _order, value);
        }

        public Order SelectedOrder
        {
            get => _selectedOrder; set => Set(ref _selectedOrder, value);
        }

        public RadioButtons RadioButton
        {
            get => _radioButtons; set => Set(ref _radioButtons, value);
        }

        public AdminViewModel(INavigationService navigationService, TrendyolDbContext context)
        {
            _navigationService = navigationService;
            _context = context;
            RadioButton = new RadioButtons();
            Order = new ObservableCollection<Order>(_context.Orders);
        }

        public RelayCommand Check
        {
            get => new(
                () =>
                {
                    try
                    {
                        if (RadioButton.OrderPlaced)
                        {
                            if (SelectedOrder.Status == "Order Placed")
                            {
                                SelectedOrder.Status = "Order Placed";
                            }
                        }
                        else if (RadioButton.ArrivedAtTheWarehouse)
                        {
                            if (SelectedOrder.Status == "Order Placed" || SelectedOrder.Status == "Arrived At The Warehouse")
                            {
                                SelectedOrder.Status = "Arrived At The Warehouse";
                            }
                        }
                        else if (RadioButton.Sent)
                        {
                            if (SelectedOrder.Status == "Arrived At The Warehouse" || SelectedOrder.Status == "Sent")
                            {
                                SelectedOrder.Status = "Sent";
                            }
                        }
                        else if (RadioButton.SmartCustomsCheck)
                        {
                            if (SelectedOrder.Status == "Sent" || SelectedOrder.Status == "Smart Customs Check")
                            {
                                SelectedOrder.Status = "Smart Customs Check";
                            }
                        }
                        else if (RadioButton.InFilial)
                        {
                            if (SelectedOrder.Status == "Smart Customs Check" || SelectedOrder.Status == "In fillial")
                            {
                                SelectedOrder.Status = "In fillial";
                            }
                        }
                        else
                        {
                            MessageBox.Show("It is not possible to transfer the order to this stage");
                            return;
                        }
                        _context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                });
        }

        public RelayCommand Back
        {
            get => new(async () =>
            {
                await _navigationService.NavigateTo<LoginViewModel>();
            });
        }

        public RelayCommand AddProduct
        {
            get => new(async () =>
            {
                await _navigationService.NavigateTo<AddProductAdminViewModel>();
            });
        }

        public RelayCommand RemoveProduct
        {
            get => new(async () =>
            {
                await _navigationService.NavigateTo<RemoveProductAdminViewModel>();
            });
        }
    }
}
