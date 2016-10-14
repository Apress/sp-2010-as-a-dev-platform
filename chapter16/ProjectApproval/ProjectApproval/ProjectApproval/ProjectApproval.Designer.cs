using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Reflection;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;

namespace ProjectApproval
{
    public sealed partial class ProjectApproval
    {
        #region Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCode]
        private void InitializeComponent()
        {
            this.CanModifyActivities = true;
            System.Workflow.Activities.Rules.RuleConditionReference ruleconditionreference1 = new System.Workflow.Activities.Rules.RuleConditionReference();
            System.Workflow.ComponentModel.ActivityBind activitybind1 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind2 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Runtime.CorrelationToken correlationtoken1 = new System.Workflow.Runtime.CorrelationToken();
            System.Workflow.ComponentModel.ActivityBind activitybind3 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind4 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Activities.CodeCondition codecondition1 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.ComponentModel.ActivityBind activitybind5 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind6 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind8 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Runtime.CorrelationToken correlationtoken2 = new System.Workflow.Runtime.CorrelationToken();
            System.Workflow.ComponentModel.ActivityBind activitybind7 = new System.Workflow.ComponentModel.ActivityBind();
            this.logDenied = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.logApproval = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.Denied = new System.Workflow.Activities.IfElseBranchActivity();
            this.Approved = new System.Workflow.Activities.IfElseBranchActivity();
            this.onEstimationTaskChanged = new Microsoft.SharePoint.WorkflowActions.OnTaskChanged();
            this.ApprovalOutcome = new System.Workflow.Activities.IfElseActivity();
            this.completeEstimationTask = new Microsoft.SharePoint.WorkflowActions.CompleteTask();
            this.whileActivity1 = new System.Workflow.Activities.WhileActivity();
            this.createEstimationTask = new Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType();
            this.onWorkflowActivated1 = new Microsoft.SharePoint.WorkflowActions.OnWorkflowActivated();
            // 
            // logDenied
            // 
            this.logDenied.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logDenied.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            this.logDenied.HistoryDescription = "ApprovalOutcome";
            this.logDenied.HistoryOutcome = "Denied";
            this.logDenied.Name = "logDenied";
            this.logDenied.OtherData = "";
            this.logDenied.UserId = -1;
            // 
            // logApproval
            // 
            this.logApproval.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logApproval.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            this.logApproval.HistoryDescription = "ApprovalOutcome";
            this.logApproval.HistoryOutcome = "Approved";
            this.logApproval.Name = "logApproval";
            this.logApproval.OtherData = "";
            this.logApproval.UserId = -1;
            // 
            // Denied
            // 
            this.Denied.Activities.Add(this.logDenied);
            this.Denied.Name = "Denied";
            // 
            // Approved
            // 
            this.Approved.Activities.Add(this.logApproval);
            ruleconditionreference1.ConditionName = "IsApproved";
            this.Approved.Condition = ruleconditionreference1;
            this.Approved.Name = "Approved";
            // 
            // onEstimationTaskChanged
            // 
            activitybind1.Name = "ProjectApproval";
            activitybind1.Path = "estimationTask_AfterProperties";
            activitybind2.Name = "ProjectApproval";
            activitybind2.Path = "estimationTask_BeforeProperties";
            correlationtoken1.Name = "estimationTaskToken";
            correlationtoken1.OwnerActivityName = "ProjectApproval";
            this.onEstimationTaskChanged.CorrelationToken = correlationtoken1;
            this.onEstimationTaskChanged.Executor = null;
            this.onEstimationTaskChanged.Name = "onEstimationTaskChanged";
            activitybind3.Name = "ProjectApproval";
            activitybind3.Path = "estimationTask_TaskId";
            this.onEstimationTaskChanged.Invoked += new System.EventHandler<System.Workflow.Activities.ExternalDataEventArgs>(this.onTaskChanged1_Invoked);
            this.onEstimationTaskChanged.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.AfterPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            this.onEstimationTaskChanged.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.BeforePropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            this.onEstimationTaskChanged.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind3)));
            // 
            // ApprovalOutcome
            // 
            this.ApprovalOutcome.Activities.Add(this.Approved);
            this.ApprovalOutcome.Activities.Add(this.Denied);
            this.ApprovalOutcome.Name = "ApprovalOutcome";
            // 
            // completeEstimationTask
            // 
            this.completeEstimationTask.CorrelationToken = correlationtoken1;
            this.completeEstimationTask.Name = "completeEstimationTask";
            activitybind4.Name = "ProjectApproval";
            activitybind4.Path = "estimationTask_TaskId";
            this.completeEstimationTask.TaskOutcome = "Completed";
            this.completeEstimationTask.SetBinding(Microsoft.SharePoint.WorkflowActions.CompleteTask.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind4)));
            // 
            // whileActivity1
            // 
            this.whileActivity1.Activities.Add(this.onEstimationTaskChanged);
            codecondition1.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.taskNotFinished);
            this.whileActivity1.Condition = codecondition1;
            this.whileActivity1.Name = "whileActivity1";
            // 
            // createEstimationTask
            // 
            this.createEstimationTask.ContentTypeId = "0x0108010031cda7a5483c452f93520d98df2f824a";
            this.createEstimationTask.CorrelationToken = correlationtoken1;
            this.createEstimationTask.ListItemId = -1;
            this.createEstimationTask.Name = "createEstimationTask";
            this.createEstimationTask.SpecialPermissions = null;
            activitybind5.Name = "ProjectApproval";
            activitybind5.Path = "estimationTask_TaskId";
            activitybind6.Name = "ProjectApproval";
            activitybind6.Path = "estimationTaskProperties";
            this.createEstimationTask.MethodInvoking += new System.EventHandler(this.estimationTask_MethodInvoking);
            this.createEstimationTask.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind5)));
            this.createEstimationTask.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType.TaskPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind6)));
            activitybind8.Name = "ProjectApproval";
            activitybind8.Path = "workflowId";
            // 
            // onWorkflowActivated1
            // 
            correlationtoken2.Name = "workflowToken";
            correlationtoken2.OwnerActivityName = "ProjectApproval";
            this.onWorkflowActivated1.CorrelationToken = correlationtoken2;
            this.onWorkflowActivated1.EventName = "OnWorkflowActivated";
            this.onWorkflowActivated1.Name = "onWorkflowActivated1";
            activitybind7.Name = "ProjectApproval";
            activitybind7.Path = "workflowProperties";
            this.onWorkflowActivated1.Invoked += new System.EventHandler<System.Workflow.Activities.ExternalDataEventArgs>(this.onWorkflowActivated1_Invoked);
            this.onWorkflowActivated1.SetBinding(Microsoft.SharePoint.WorkflowActions.OnWorkflowActivated.WorkflowIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind8)));
            this.onWorkflowActivated1.SetBinding(Microsoft.SharePoint.WorkflowActions.OnWorkflowActivated.WorkflowPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind7)));
            // 
            // ProjectApproval
            // 
            this.Activities.Add(this.onWorkflowActivated1);
            this.Activities.Add(this.createEstimationTask);
            this.Activities.Add(this.whileActivity1);
            this.Activities.Add(this.completeEstimationTask);
            this.Activities.Add(this.ApprovalOutcome);
            this.Name = "ProjectApproval";
            this.CanModifyActivities = false;

        }

        #endregion

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logApproval;
        private IfElseBranchActivity Denied;
        private IfElseBranchActivity Approved;
        private IfElseActivity ApprovalOutcome;
        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logDenied;
        private Microsoft.SharePoint.WorkflowActions.CompleteTask completeEstimationTask;
        private Microsoft.SharePoint.WorkflowActions.OnTaskChanged onEstimationTaskChanged;
        private WhileActivity whileActivity1;
        private Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType createEstimationTask;
        private Microsoft.SharePoint.WorkflowActions.OnWorkflowActivated onWorkflowActivated1;














































    }
}
