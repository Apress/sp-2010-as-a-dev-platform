using System;
using System.ComponentModel;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Compiler;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WorkflowActions;

namespace SPDActivities
{
    public partial class LookupItem : Activity
    {
        public LookupItem()
        {
            InitializeComponent();
        }

        public static DependencyProperty __ContextProperty = DependencyProperty.Register("__Context", typeof(WorkflowContext), typeof(LookupItem));
        public static DependencyProperty ListIdProperty = DependencyProperty.Register("ListId", typeof(string), typeof(LookupItem), new PropertyMetadata(""));
        private static DependencyProperty SearchQueryProperty = DependencyProperty.Register("SearchQuery", typeof(string), typeof(LookupItem), new PropertyMetadata(""));
        public static DependencyProperty ResultItemIdProperty = DependencyProperty.Register("ResultItemId", typeof(int), typeof(LookupItem));


        #region Properties

        // Properties
        [ValidationOption(ValidationOption.Required)]
        public WorkflowContext __Context
        {
            get
            {
                return (WorkflowContext)base.GetValue(LookupItem.__ContextProperty);
            }
            set
            {
                base.SetValue(LookupItem.__ContextProperty, value);
            }
        }

        [BrowsableAttribute(true)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        [ValidationOption(ValidationOption.Required)]
        public string ListId
        {
            get
            {
                return (string)base.GetValue(LookupItem.ListIdProperty);
            }
            set
            {
                base.SetValue(LookupItem.ListIdProperty, value);
            }
        }

        [BrowsableAttribute(true)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        [ValidationOption(ValidationOption.Required)]
        public string SearchQuery
        {
            get
            {
                return (string)base.GetValue(LookupItem.SearchQueryProperty);
            }
            set
            {
                base.SetValue(LookupItem.SearchQueryProperty, value);
            }
        }

        [BrowsableAttribute(true)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        [ValidationOption(ValidationOption.Required)]
        public int ResultItemId
        {
            get
            {
                return (int)base.GetValue(LookupItem.ResultItemIdProperty);
            }
            set
            {
                base.SetValue(LookupItem.ResultItemIdProperty, value);
            }
        }

        #endregion


        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            Guid listGuid = Helper.GetListGuid(this.__Context, this.ListId);
            if ((this.__Context != null))
            {
                SPWeb web = this.__Context.Web;
                if (null != web)
                {
                    SPList list = web.Lists[listGuid];
                    SPQuery query = new SPQuery();
                    query.Query = this.SearchQuery;
                    SPListItemCollection items = list.GetItems(query);
                    if (items.Count > 0)
                    {
                        ResultItemId = items[0].ID;
                    }

                }
            }
            return ActivityExecutionStatus.Closed;
        }
    }
}
