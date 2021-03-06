﻿using Fujitsu.Tools.SDKExplorer.Controller.Interfaces;
using Fujitsu.Tools.SDKExplorer.Model;
using Fujitsu.Utilities.UserManagement.Contract;
using Fujitsu.Utilities.UserManagement.Proxy;

namespace Fujitsu.Tools.SDKExplorer.Controller.Scenarios
{
    public class UserManagementDeleteOperatorGroup : IScenario
    {
        public void Start()
        {
            var remoteHost = "net.tcp://" + m_Settings.General.RemoteHostName +
                             "/Fujitsu.Utilities.UserManagement.Service.UserManagement";
            using (var proxy = new UserManagementProxy(remoteHost))
            {
                var result = proxy.ExecuteUpdateOperation(
                    new DeleteOperatorGroupOperation
                    {
                        OperatorGroupId = 1000
                    });
                var xml = new MessageViewFormatter<UserManagementUpdateOperationResult>(result).FormatMessage();
                m_Controller.ShowResult(xml);
            }
        }

        #region Constructor

        private readonly IController m_Controller;
        private readonly IUserManagementSettings m_Settings;

        public UserManagementDeleteOperatorGroup(IController controller, IUserManagementSettings settings)
        {
            m_Controller = controller;
            m_Settings = settings;
        }

        #endregion Constructor
    }
}