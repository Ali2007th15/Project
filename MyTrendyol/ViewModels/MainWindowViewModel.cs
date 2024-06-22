using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using MyTrendyol.Messages;
using MyTrendyol.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTrendyol.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IMessenger _messenger;
        private ViewModelBase currentView;


        public ViewModelBase CurrentView
        {
            get => currentView;
            set => Set(ref currentView, value);
        }

        public MainWindowViewModel(IMessenger messenger)
        {
            CurrentView = App.Container.GetInstance<LoginViewModel>();
            _messenger = messenger;

            _messenger.Register<NavigationMessage>(this, message =>
            {
                CurrentView = message.ViewModelType;
            });
        }
    }
}
