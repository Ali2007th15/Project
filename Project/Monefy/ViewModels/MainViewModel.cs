using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monefy.Messages;
namespace Monefy.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        private readonly IMessenger _messenger;

        private ViewModelBase _currentView;
        public ViewModelBase CurrentView
        {
            get => _currentView;
            set
            {
                Set(ref _currentView, value);
            }
        }

        public MainViewModel(IMessenger messenger)
        {
            _messenger = messenger;
            CurrentView = App.Container.GetInstance<InfoViewModel>();

            _messenger.Register<NavigationMessage>(this, message =>
            {
                CurrentView = message.ViewModelType;
            });
        }


    }
}

