namespace Fujitsu.Tools.SDKExplorer.Controller.Interfaces
{
    public interface IController
    {
        void RunScenario(ScenarioItem scenarioItem);
        void ShowAPIDoc(ScenarioItem scenarioItem);
        void ShowResult(MessageView message);
        void ShowSettings(ScenarioItem scenarioItem);
        void ShowSourceCode(ScenarioItem scenarioItem);
        void ShowUserManagementMessage(MessageView message);
        string StartupWorker();
        void StartupWorkerComplete();
        //StsServiceHost<TransactionRepositoryNotificationService> TransactionRepositoryNotification { get; set; }
        //StsServiceHost<UserManagementNotificationService> UserManagementNotification { get; set; }
        //StsServiceHost<ElectronicJournalRepositoryNotificationService> ElectronicJournalNotification { get; set; }
        //StsServiceHost<ReceiptRepositoryNotificationService> ReceiptRepositoryNotification { get; set; }
        //StsServiceHost<StoreOperationsCallBackService> StoreOperationsCallBackService { get; set; }
        //StoreOperationsProxy StoreOperationsProxy { get; set; }
        //Fujitsu.Tools.SDKExplorer.Controller.Interfaces.ISDKExplorerView View { get; }

        void Shutdown();
    }
}