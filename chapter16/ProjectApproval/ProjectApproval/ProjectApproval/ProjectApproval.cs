using System;
using System.Workflow.Activities;
using Microsoft.SharePoint.Workflow;

namespace ProjectApproval
{
    public sealed partial class ProjectApproval : SequentialWorkflowActivity
    {
        public ProjectApproval()
        {
            InitializeComponent();
        }

        public Guid workflowId = default(System.Guid);
        public SPWorkflowActivationProperties workflowProperties = new SPWorkflowActivationProperties();
        public SPWorkflowTaskProperties estimationTaskProperties = new SPWorkflowTaskProperties();
        bool isTaskComplete = false;

        public SPWorkflowTaskProperties estimationTask_AfterProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();
        public SPWorkflowTaskProperties estimationTask_BeforeProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();
        public string costs = default(System.String);
        public string outcome = default(System.String);
        public Guid estimationTask_TaskId = default(System.Guid);


        private void onWorkflowActivated1_Invoked(object sender, ExternalDataEventArgs e)
        {
            workflowId = workflowProperties.WorkflowId;
        }

        private void estimationTask_MethodInvoking(object sender, EventArgs e)
        {
            estimationTask_TaskId = Guid.NewGuid();
            estimationTaskProperties.AssignedTo = "wapps\\administrator";
            estimationTaskProperties.Title = "Approve_" + workflowProperties.Item.Title;
        }

        private void taskNotFinished(object sender, ConditionalEventArgs e)
        {
            e.Result = !isTaskComplete;
        }

        private void onTaskChanged1_Invoked(object sender, ExternalDataEventArgs e)
        {
            isTaskComplete = true;
            costs = estimationTask_AfterProperties.ExtendedProperties["_EstimatedCosts"].ToString();
            outcome = estimationTask_AfterProperties.ExtendedProperties["_Outcome"].ToString();

        }

    }
}