using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LiveCharts.Wpf;
using LiveCharts;
using Monefy.Services.Interfaces;
using Monefy.Models;
using System.Text;
using System.Windows.Media;
using Monefy.Services.Classes;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using GalaSoft.MvvmLight.Messaging;
using Monefy.Messages;

namespace Monefy.ViewModels
{
    class CalculatorViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        private readonly IChartService _chartService;

        private readonly IDataService _dataService;

        private PackIcon icon;

        private Button MyButton { get; set; }

        private MyChart Chart;

        private double Balance;

        private double Amount;

        private StringBuilder Expression = new();

        private string _expressionText;

        private string text;
        public string Text
        {
            get => Text;
            set
            {
                Set(ref text, value);
            }
        }
        public PackIcon Icon
        {
            get => icon;
            set
            {
                Set(ref icon, value);
            }
        }

        public string ExpressionText
        {
            get => _expressionText;
            set
            {
                Set(ref _expressionText, value);
            }
        }

        public CalculatorViewModel(INavigationService navigationService, IChartService chartService, IMessenger messenger, IDataService dataService)
        {
            _navigationService = navigationService;
            _chartService = chartService;
            _dataService = dataService;

            messenger.Register<DatasMessage>(this, message =>
            {
                Chart = message.Datas[0] as MyChart;
                MyButton = message.Datas[1] as Button;
                Icon = MyButton.Content as PackIcon;
            });
        }


		


        public RelayCommand Equal
        {
            get => new(
            () =>
            {
                if (Expression.Length > 0)
                {

                    ExpressionText = new System.Data.DataTable().Compute(Expression.ToString(), null).ToString();
                    Expression.Clear();
                    Expression.Append(ExpressionText);
                }
            });
        }
        public RelayCommand Back
        {
            get => new(() =>
            {
                Expression.Clear();
                _expressionText = "";
                _navigationService.NavigateTo < InfoViewModel>();
            
			});
        }

        public RelayCommand Clear
        {
            get => new(() =>
            {
                Expression.Clear();
                ExpressionText = "";
            });
        }

		public RelayCommand<string> numClick
		{
			get => new((operation) =>
			{
				if (operation != "+" && operation != "-" && operation != "*" && operation != "/")
					ExpressionText += operation;
				else
				{
					ExpressionText = "";
				}
				Expression.Append(operation);
			});
		}



		public RelayCommand AddTransaction
        {
            get => new(
            () =>
            {
                
                Balance = double.Parse(new System.Data.DataTable().Compute(Expression.ToString(), null).ToString());

                _dataService.SendData(Balance);
                PackIcon _packIcon = MyButton.Content as PackIcon;
               
                
                    _chartService.AddSerie(Chart, _packIcon.Foreground);
                    _navigationService.NavigateTo<InfoViewModel>();
                    Expression.Clear();
                    ExpressionText = "";
                
                
            });
    

        }

    }

}
