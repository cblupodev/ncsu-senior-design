using System.ComponentModel;
using System.Runtime.CompilerServices;
using Fujitsu.Infrastructure.Core;

namespace Fujitsu.Tools.SDKExplorer.Model
{
    public class UserManagementSettings : IUserManagementSettings
    {
        public UserManagementSettings(IGeneralSettings general)
        {
            General = general;
            General.PropertyChanged += GeneralPropertyChange;
            var networkInfo = EnvironmentHelpers.Client.GetNetworkIdentifier();
            PhysicalAddress = networkInfo.PhysicalAddress;
            WorkerID = "10003";
            OperatorID = "1000";
            StoreID = 1;
            TestTenderContainerID = "110";
            TestWorkerID = "1000";
            TestSellingDeviceID = "POS10003";
            TestTerminalAccountability = false;
            TestWorkerName = "Bruce";
        }

        public IGeneralSettings General { get; set; }
        public string PhysicalAddress { get; set; }
        public string OperatorID { get; set; }
        public string WorkerID { get; set; }
        public int StoreID { get; set; }
        public string TestTenderContainerID { get; set; }
        public string TestWorkerID { get; set; }
        public string TestSellingDeviceID { get; set; }
        public bool TestTerminalAccountability { get; set; }
        public string TestWorkerName { get; set; }

        /// <summary>
        ///     True if certificate security proxy should be used
        /// </summary>
        public bool UseCertificateProxy { get; set; }

        #region Property Change Event

        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property. 
        // The CallerMemberName attribute that is applied to the optional propertyName 
        // parameter causes the property name of the caller to be substituted as an argument. 
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void GeneralPropertyChange(object sender, PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged(e.PropertyName);
        }

        #endregion Property Change Event
    }
}