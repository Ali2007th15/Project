using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Monefy.ViewModels;
using SimpleInjector;
using Monefy.Services.Interfaces;
using Monefy.Services.Classes;
using Monefy.Views;

namespace Monefy;
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Container Container { get; set; } = new();

     
        public void Register()
        {

            Container.RegisterSingleton<IMessenger, Messenger>();
            Container.RegisterSingleton<INavigationService, NavigationService>();
            Container.RegisterSingleton<IDataService, DataService>();
            Container.RegisterSingleton<IChartService,ChartService>();

            Container.RegisterSingleton<MainViewModel>();
            Container.RegisterSingleton<InfoViewModel>();
            Container.RegisterSingleton<CalculatorViewModel>();
          


        Container.Verify();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Register();

            var window = new MainView();

            window.DataContext = Container.GetInstance<MainViewModel>();

            window.ShowDialog();
        }
    }


