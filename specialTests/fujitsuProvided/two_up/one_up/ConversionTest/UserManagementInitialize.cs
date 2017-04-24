using Fujitsu.Infrastructure.Contract.ExecutionEnvironment;
using Fujitsu.Tools.SDKExplorer.Controller.Interfaces;
using Fujitsu.Tools.SDKExplorer.Model;
using Fujitsu.Utilities.UserManagement.Proxy;

namespace Fujitsu.Tools.SDKExplorer.Controller.Scenarios
{
    /// <summary>
    ///     User Management Initialization Scenario
    /// </summary>
    public class UserManagementInitialize : IScenario
    {
        /// <summary>
        ///     Starts the scenario
        /// </summary>
        public void Start()
        {
            if (m_Settings.UseCertificateProxy)
            {
                var remoteHost = "net.tcp://" + m_Settings.General.RemoteHostName +
                                 "/Fujitsu.Utilities.UserManagement.Service.UserManagementWithCertificateSecurity";
                using (var proxy = new UserManagementProxyWithCertificateSecurity(remoteHost))
                {
                    proxy.Initialize(new NetworkIdentifier("SC1", "000C29F0C32B", "StoreCenter"));
                    m_Controller.ShowResult(new MessageView
                    {
                        //Pretty = Utilities.RenderHTMLMessage(
                        //    "User Management Session Initialized\r\n\r\n" +
                        //    "Start User Management to monitor User Management Messages")
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
                    proxy.Initialize(new NetworkIdentifier("SC1", "000C29F0C32B", "StoreCenter"));
                    m_Controller.ShowResult(new MessageView
                    {
                        //Pretty = Utilities.RenderHTMLMessage(
                        //    "User Management Session Initialized\r\n\r\n" +
                        //    "Start User Management to monitor User Management Messages")
                    }
                        );
                }
            }
        }

        #region Constructor

        /// <summary>
        ///     Controller
        /// </summary>
        private readonly IController m_Controller;

        /// <summary>
        ///     Settings
        /// </summary>
        private readonly IUserManagementSettings m_Settings;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="controller">Controller</param>
        /// <param name="settings">Settings</param>
        public UserManagementInitialize(IController controller, IUserManagementSettings settings)
        {
            m_Controller = controller;
            m_Settings = settings;
        }

        #endregion Constructor
    }
}