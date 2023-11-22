using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp8.Messages;
using WpfApp8.Services.Interfaces;

namespace WpfApp8.Services.Classes
{
    class DataService : IDataService
    {
        private readonly IMessenger _messenger;

        public DataService(IMessenger messenger)
        {
            _messenger = messenger;
        }

        public void SendData(object data)
        {
            _messenger.Send(new DataMessage()
            {
                Data = data
            });
        }
        public void SendDatas(object[] data)
        {
            _messenger.Send(new DatasMessage()
            {
                Datas = data
            });
        }
    }
}
