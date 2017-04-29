using System.ComponentModel;

namespace Fujitsu.Tools.SDKExplorer.Model
{
    public interface IUserManagementSettings : INotifyPropertyChanged
    {
        IGeneralSettings General { get; set; }
        string OperatorID { get; set; }
        string PhysicalAddress { get; set; }
        string WorkerID { get; set; }
        int StoreID { get; set; }
        string TestTenderContainerID { get; set; }
        string TestWorkerID { get; set; }
        string TestSellingDeviceID { get; set; }
        bool TestTerminalAccountability { get; set; }
        string TestWorkerName { get; set; }

        /// <summary>
        ///     True if certificate security proxy should be used
        /// </summary>
        bool UseCertificateProxy { get; set; }

//        event PropertyChangedEventHandler PropertyChanged;
    }
}