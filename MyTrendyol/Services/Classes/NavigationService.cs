using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using MyTrendyol.Messages;
using MyTrendyol.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTrendyol.Services.Classes
{
    public class NavigationService : INavigationService
    {
        private readonly IMessenger _messenger;

        public NavigationService(IMessenger messenger)
        {
            _messenger = messenger;
        }

        public async Task NavigateTo<T>() where T : ViewModelBase
        {
            await Task.Run(() =>
            {
                _messenger.Send(new NavigationMessage()
                {
                    ViewModelType = App.Container.GetInstance<T>()
                });
            });
        }
    }
}
