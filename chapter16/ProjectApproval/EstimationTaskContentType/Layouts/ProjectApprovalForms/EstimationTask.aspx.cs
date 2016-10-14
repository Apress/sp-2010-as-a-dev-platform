using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.Workflow;
using System.Collections;
using System.Globalization;

namespace EstimationTaskContentType.Layouts.ProjectApprovalForms
{
    public partial class EstimationTask : LayoutsPageBase
    {
        protected SPList _taskList;
        protected SPListItem _taskListItem;
        protected Guid _workflowGuid;
        protected string _listGuid;
        protected string _listItemId;

        protected void Page_Load(object sender, EventArgs e)
        {
            this._listGuid = Request.Params["List"];
            this._listItemId = Request.Params["ID"];

            using (SPWeb web = SPContext.Current.Site.OpenWeb())
            {

                this._taskList = web.Lists[new Guid(this._listGuid)];
                this._taskListItem = this._taskList.GetItemById(Convert.ToInt32(this._listItemId));
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Hashtable hashTable = new Hashtable();
            hashTable["EstimatedCosts"] = EstimatedCosts.Text;
            hashTable["_EstimatedCosts"] = EstimatedCosts.Text;
            hashTable["Outcome"] = Outcome.SelectedValue;
            hashTable["_Outcome"] = Outcome.SelectedValue;
            hashTable["TaskStatus"] = "complete";
            hashTable["PercentComplete"] = "1";

            SPWorkflowTask.AlterTask(this._taskListItem, hashTable, true);
            this.Page.Response.Clear();
            this.Page.Response.Write(string.Format(CultureInfo.InvariantCulture,"<script type=\"text/javascript\">window.frameElement.commonModalDialogClose(1, '{0}');</script>", ""));
            this.Page.Response.End();
        }
    }
}
