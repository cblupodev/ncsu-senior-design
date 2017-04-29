using Fujitsu.Tools.SDKExplorer.Controller.Interfaces;
using Fujitsu.Tools.SDKExplorer.Model;
using Fujitsu.Utilities.UserManagement.Contract;
using Fujitsu.Utilities.UserManagement.Proxy;

namespace Fujitsu.Tools.SDKExplorer.Controller.Scenarios
{
    public class UserManagementGetOperatorGroups : IScenario
    {
        public void Start()
        {
            var remoteHost = "net.tcp://" + m_Settings.General.RemoteHostName +
                             "/Fujitsu.Utilities.UserManagement.Service.UserManagement";
            using (var proxy = new UserManagementProxy(remoteHost))
            {
                var result = proxy.ExecuteReadOperation(
                    new GetOperatorGroupsOperation()
                    );
                var xml = new MessageViewFormatter<UserManagementReadOperationResult>(result).FormatMessage();
                m_Controller.ShowResult(xml);
            }
        }

        #region Constructor

        private readonly IController m_Controller;
        private readonly IUserManagementSettings m_Settings;

        public UserManagementGetOperatorGroups(IController controller, IUserManagementSettings settings)
        {
            m_Controller = controller;
            m_Settings = settings;
        }

        #endregion Constructor
    }
}