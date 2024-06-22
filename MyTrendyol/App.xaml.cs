
using GalaSoft.MvvmLight.Messaging;
using MyTrendyol.Contexts;
using MyTrendyol.Models;
using MyTrendyol.Services.Classes;
using MyTrendyol.Services.Interfaces;
using MyTrendyol.ViewModels;
using MyTrendyol.Views;
using SimpleInjector;
using System.Configuration;
using System.Data;
using System.Windows;

namespace MyTrendyol
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Container Container { get; set; }
        void Register()
        {
            Container = new Container();

            Container.RegisterSingleton<IMessenger, Messenger>();
            Container.RegisterSingleton<INavigationService, NavigationService>();
            Container.RegisterSingleton<IUserService, UserService>();
            Container.RegisterSingleton<IAdminService, AdminService>();
            Container.RegisterSingleton<IAddOrderService, AddOrderService>();
            Container.RegisterSingleton<IAddCargoService, AddCargoService>();
            Container.RegisterSingleton<TrendyolDbContext>();
            Container.RegisterSingleton<RadioButtons>();
            Container.RegisterSingleton<MainWindowViewModel>();
            Container.RegisterSingleton<RegisterViewModel>();
            Container.RegisterSingleton<MainViewModel>();
            Container.RegisterSingleton<ForgotPasswordViewModel>();
            Container.RegisterSingleton<MyAccountViewModel>();
            Container.RegisterSingleton<CurrentUserService>();
            Container.RegisterSingleton<PayViewModel>();
            Container.RegisterSingleton<BalanceViewModel>();
            Container.RegisterSingleton<ProductCountViewModel>();
            Container.RegisterSingleton<AddProductAdminViewModel>();
            Container.RegisterSingleton<AddProductSuperAdminViewModel>();
            Container.RegisterSingleton<AddAdminSuperAdminViewModel>();
            Container.RegisterSingleton<AddUserSuperAdminViewModel>();
            Container.Register<RemoveProductAdminViewModel>();
            Container.Register<RemoveProductSuperAdminViewModel>();
            Container.Register<RemoveAdminSuperAdminViewModel>();
            Container.Register<RemoveUserSuperAdminViewModel>();
            Container.Register<LoginViewModel>();
            Container.Register<AdminViewModel>();
            Container.Register<SuperAdminViewModel>();
            Container.Register<OrderHistoryViewModel>();
            Container.Register<BuyProductViewModel>();
            Container.Register<CancelOrderViewModel>();

            Container.Verify();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Register();
            var window = new MainWindow();
            window.DataContext = Container.GetInstance<MainWindowViewModel>();
            window.Show();
        }
    }

}
