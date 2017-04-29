using Fujitsu.Tools.SDKExplorer.Controller.Interfaces;
using Fujitsu.Tools.SDKExplorer.Model;
using Fujitsu.Utilities.UserManagement.Contract.Operations.Read;
using Fujitsu.Utilities.UserManagement.Contract.Operations.Update;
using Fujitsu.Utilities.UserManagement.Contract;
using Fujitsu.Utilities.UserManagement.Proxy;

namespace Fujitsu.Tools.SDKExplorer.Controller.Scenarios
{
    public class UserManagementResetPassword : IScenario
    {
        /// <summary>
        ///     Add worker
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
                        new PasswordResetOperation
                        {
                            WorkerId = m_Settings.TestWorkerID
                        });
                }
            }
            else
            {
                var remoteHost = "net.tcp://" + m_Settings.General.RemoteHostName +
                                 "/Fujitsu.Utilities.UserManagement.Service.UserManagement";
                using (var proxy = new UserManagementProxy(remoteHost))
                {
                    result = proxy.ExecuteUpdateOperation(
                        new PasswordResetOperation
                        {
                            WorkerId = m_Settings.TestWorkerID
                        });
                }
            }
            var xml = new MessageViewFormatter<UserManagementUpdateOperationResult>(result).FormatMessage();
            m_Controller.ShowResult(xml);
        }

        #region Constructor

        /// <summary>
        ///     Current controller
        /// </summary>
        private readonly IController m_Controller;

        /// <summary>
        ///     Current settings
        /// </summary>
        private readonly IUserManagementSettings m_Settings;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="controller">Controller</param>
        /// <param name="settings">Settings</param>
        public UserManagementResetPassword(IController controller, IUserManagementSettings settings)
        {
            m_Controller = controller;
            m_Settings = settings;
        }

        #endregion Constructor
    }
}