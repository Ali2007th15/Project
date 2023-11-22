using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp8.Messages
{
    class NavigationMessage
    {
        public Type Data { get; set; }
        public ViewModelBase ViewModelType { get; set; }
    }
}