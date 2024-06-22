using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTrendyol.Services.Interfaces
{
    public interface INavigationService
    {
        public Task NavigateTo<T>() where T : ViewModelBase;
    }
}
