using System.Security;
using Fujitsu.Infrastructure.Core;
using Fujitsu.Tools.SDKExplorer.Controller.Interfaces;
using Fujitsu.Tools.SDKExplorer.Model;
using Fujitsu.Utilities.Security.Cryptography.Contract;
using Fujitsu.Utilities.UserManagement.Contract;
using Fujitsu.Utilities.UserManagement.Proxy;
using Microsoft.Practices.Unity;

namespace Fujitsu.Tools.SDKExplorer.Controller.Scenarios
{
    internal class UserManagementChangePassword : IScenario
    {
        /// <summary>
        ///     Add worker
        /// </summary>
        public void Start()
        {
            UserManagementUpdateOperationResult result;

            var password = new SecureString();
            password.AppendChar('a');
            password.AppendChar('0');
            password.AppendChar('0');
            password.AppendChar('0');
            password.AppendChar('0');
            password.AppendChar('0');
            password.AppendChar('0');
            var helper = RootContainer.container.Resolve<IPasswordHandler>();
            var enc = helper.EncryptPassword(password).ToString();

            if (m_Settings.UseCertificateProxy)
            {
                var remoteHost = "net.tcp://" + m_Settings.General.RemoteHostName +
                                 "/Fujitsu.Utilities.UserManagement.Service.UserManagementWithCertificateSecurity";
                using (var proxy = new UserManagementProxyWithCertificateSecurity(remoteHost))
                {
                    result = proxy.ExecuteUpdateOperation(
                        new PasswordCreateOperation
                        {
                            NewPasswordEncrypted = enc,
                            WorkerID = m_Settings.TestWorkerID
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
                        new PasswordChangeOperation
                        {
                            NewPasswordEncrypted = enc
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
        public UserManagementChangePassword(IController controller, IUserManagementSettings settings)
        {
            m_Controller = controller;
            m_Settings = settings;
        }

        #endregion Constructor
    }
}