using Fujitsu.Tools.SDKExplorer.Controller.Interfaces;
using Fujitsu.Tools.SDKExplorer.Model;
using Fujitsu.Utilities.UserManagement.Contract.Operations.Read;
using Fujitsu.Utilities.UserManagement.Contract.Operations.Update;
using Fujitsu.Utilities.UserManagement.Contract;
using Fujitsu.Utilities.UserManagement.Proxy;

namespace Fujitsu.Tools.SDKExplorer.Controller.Scenarios
{
    public class UserManagementReplicatePassword : IScenario
    {
        /// <summary>
        ///     Run test
        /// </summary>
        public void Start()
        {
            UserManagementUpdateOperationResult result;
            if (m_Settings.UseCertificateProxy)
            {
                var remoteHost = "net.tcp://" + m_Settings.General.RemoteHostName +
                                 "/Fujitsu.Utilities.UserManagement.Service.UserManagementWithCertificateSecurity";
                using (var proxy = new UserManagementProxyWithCertificateSecurity(remoteHost))
                {
                    result = proxy.ExecuteUpdateOperation(
                        new PasswordReplicationOperation
                        {
                            WorkerIds = new[] {"10001", "10002", "10003"}
                        }
                        );
                }
            }
            else
            {
                var remoteHost = "net.tcp://" + m_Settings.General.RemoteHostName +
                                 "/Fujitsu.Utilities.UserManagement.Service.UserManagement";
                using (var proxy = new UserManagementProxy(remoteHost))
                {
                    result = proxy.ExecuteUpdateOperation(
                        new PasswordReplicationOperation
                        {
                            WorkerIds = new[] {"10001", "10002", "10003"}
                        }
                        );
                }
            }
            var xml = new MessageViewFormatter<UserManagementReadOperationResult>(result).FormatMessage();
            m_Controller.ShowResult(xml);
        }

        #region Constructor

        private readonly IController m_Controller;
        private readonly IUserManagementSettings m_Settings;

        public UserManagementReplicatePassword(IController controller, IUserManagementSettings settings)
        {
            m_Controller = controller;
            m_Settings = settings;
        }

        #endregion Constructor
    }
}