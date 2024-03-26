using GalaSoft.MvvmLight.Messaging;
using Monefy.Messages;
using Monefy.Services.Interfaces;

namespace Monefy.Services.Classes;
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
