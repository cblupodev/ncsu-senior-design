using System.ComponentModel;

namespace Fujitsu.Tools.SDKExplorer.Model
{
    public interface IGeneralSettings : INotifyPropertyChanged
    {
        string MachineName { get; set; }
        string RemoteHostName { get; set; }

//        event PropertyChangedEventHandler PropertyChanged;
    }
}