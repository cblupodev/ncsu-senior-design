using Fujitsu.Tools.SDKExplorer.Controller.Interfaces;
using Fujitsu.Tools.SDKExplorer.Model;
using Fujitsu.Utilities.UserManagement.Contract;
using Fujitsu.Utilities.UserManagement.Proxy;

namespace Fujitsu.Tools.SDKExplorer.Controller.Scenarios
{
    public class UserManagementUpdateWorker : IScenario
    {
        public void Start()
        {
            var remoteHost = "net.tcp://" + m_Settings.General.RemoteHostName +
                             "/Fujitsu.Utilities.UserManagement.Service.UserManagement";
            using (var proxy = new UserManagementProxy(remoteHost))
            {
                var readResult = proxy.ExecuteReadOperation(
                    new GetWorkerByIdOperation
                    {
                        WorkerId = m_Settings.TestWorkerID
                    }
                    );
                UserManagementUpdateOperationResult result = null;
                var getWorkerResult = readResult as GetWorkerByIdOperationResult;
                if (getWorkerResult != null)
                {
                    if (getWorkerResult.Errors.Length == 0)
                    {
                        getWorkerResult.Worker.Person.FirstName = m_Settings.TestWorkerName;
                        result = proxy.ExecuteUpdateOperation(
                            new UpdateWorkerOperation
                            {
                                Worker = getWorkerResult.Worker
                            }
                            );
                    }
                }
                var xml = new MessageViewFormatter<UserManagementUpdateOperationResult>(result).FormatMessage();
                m_Controller.ShowResult(xml);
            }
        }

        #region Constructor

        private readonly IController m_Controller;
        private readonly IUserManagementSettings m_Settings;

        public UserManagementUpdateWorker(IController controller, IUserManagementSettings settings)
        {
            m_Controller = controller;
            m_Settings = settings;
        }

        #endregion Constructor
    }
}