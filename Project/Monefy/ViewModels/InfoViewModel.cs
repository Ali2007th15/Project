using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LiveCharts;
using LiveCharts.Wpf;
using MaterialDesignThemes.Wpf;
using Monefy.Models;
using Monefy.Services.Classes;
using Monefy.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Monefy.ViewModels;

    internal class InfoViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IDataService _dataService;
        public PackIcon? _packIcon = new();
        public MyChart _currentChart;
        public List<MyChart> Charts = new();
        public MyChart currentChart = new();
        
        public MyChart CurrentChart
        {
            get => currentChart;
            set
            {
                Set(ref currentChart, value);
            }
        }

        public InfoViewModel(INavigationService navigationService, IDataService dataService)
        {
            Charts.Add(new MyChart()
            {
                Date = DateTime.Today.AddDays(-2),
            });
            Charts.Add(new MyChart()
            {
                Date = DateTime.Today.AddDays(-1)
            });
            Charts.Add(new MyChart());

            CurrentChart = Charts[searchIndex(DateTime.Today)];
            _navigationService = navigationService;
            _dataService = dataService;
        }

       
       

    public RelayCommand<Button> Add
        {
            get => new(button =>
            {

                _dataService.SendDatas(new object[] { Charts[searchIndex(CurrentChart.Date)], button });
                _navigationService.NavigateTo<CalculatorViewModel>();
            });
        }

        
        public RelayCommand<Button> Plus
        { get => new(button =>
          {
              _navigationService.NavigateTo<CalculatorViewModel>();
                

          });

        }





	public int searchIndex(DateTime Date)
        {
            for (int i = 0; i < Charts.Count; i++)
            {
                if (Charts[i].Date == Date)
                    return i;
            }
            return -1;
        }


    }

    


    

