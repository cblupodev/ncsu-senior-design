using System;
using Fujitsu.Tools.SDKExplorer.Controller.Interfaces;
using Fujitsu.Tools.SDKExplorer.Model;
using Fujitsu.Utilities.Security.DataAccess.Contract;
using Fujitsu.Utilities.UserManagement.Contract;
using Fujitsu.Utilities.UserManagement.Proxy;

namespace Fujitsu.Tools.SDKExplorer.Controller.Scenarios
{
    public class UserManagementAddWorker : IScenario
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
                        new CreateWorkerOperation
                        {
                            Worker = new Worker
                            {
                                DefaultLanguageID = 9,
                                HireDate = DateTimeOffset.Now,
                                Person = new Person
                                {
                                    FirstName = "John",
                                    GenderType = 1,
                                    LanguageID = 9,
                                    LastName = "Smith",
                                    PartyID = Guid.NewGuid(),
                                    Salutation = "Mr"
                                },
                                TerminationDate = null,
                                WorkerID = m_Settings.TestWorkerID,
                                WorkerOperators = new[]
                                {
                                    new WorkerOperator
                                    {
                                        ConfigPoint = m_Settings.StoreID,
                                        DisplayName = "Johnny Smith",
                                        OperatorGroupIDs = new[] {-2},
                                        OperatorID = m_Settings.OperatorID,
                                        WorkerID = m_Settings.TestWorkerID
                                    }
                                }
                            }
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
                        new CreateWorkerOperation
                        {
                            Worker = new Worker
                            {
                                DefaultLanguageID = 9,
                                HireDate = DateTimeOffset.Now,
                                Person = new Person
                                {
                                    FirstName = "John",
                                    GenderType = 1,
                                    LanguageID = 9,
                                    LastName = "Smith",
                                    PartyID = Guid.NewGuid(),
                                    Salutation = "Mr"
                                },
                                TerminationDate = null,
                                WorkerID = m_Settings.TestWorkerID,
                                WorkerOperators = new[]
                                {
                                    new WorkerOperator
                                    {
                                        ConfigPoint = m_Settings.StoreID,
                                        DisplayName = "Johnny Smith",
                                        OperatorGroupIDs = new[] {1},
                                        OperatorID = m_Settings.OperatorID,
                                        WorkerID = m_Settings.TestWorkerID
                                    }
                                },
                                WorkerOperatorSellLocations = new[]
                                {
                                    new WorkerOperatorSellLocation
                                    {
                                        DefaultTenderContainerID = "101",
                                        SellingLocationID = 1
                                    }
                                }
                            }
                        });
                }
            }
            var xml = new MessageViewFormatter<UserManagementUpdateOperationResult>(result).FormatMessage();
            m_Controller.ShowResult(xml);
        }

        #region Constructor

        private readonly IController m_Controller;
        private readonly IUserManagementSettings m_Settings;

        public UserManagementAddWorker(IController controller, IUserManagementSettings settings)
        {
            m_Controller = controller;
            m_Settings = settings;
        }

        #endregion Constructor
    }
}