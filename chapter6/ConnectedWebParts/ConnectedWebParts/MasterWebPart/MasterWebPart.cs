using System;
using System.Linq;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace ConnectedWebParts
{
    [ToolboxItemAttribute(false)]
    public class MasterWebPart : WebPart, IWebPartField
    {

        private SPGridView webLists;
        public string ListName { get; set; }

        public MasterWebPart()
        {
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            SPWeb web = SPContext.Current.Web;
            var lists = web.Lists.Cast<SPList>();
            webLists = new SPGridView();
            webLists.AutoGenerateColumns = false;
            webLists.DataSource = lists;
            webLists.Columns.Add(new BoundField()
                {
                    DataField = "Title",
                    HeaderText = "List Name"
                });
            webLists.Columns.Add(new BoundField()
            {
                DataField = "ItemCount",
                HeaderText = "No Items"
            });
            webLists.Columns.Add(new CommandField()
            {
                HeaderText = "Action",
                ControlStyle = { Width = new Unit(70) },
                SelectText = "Show Items",
                ShowSelectButton = true
            });
            webLists.DataKeyNames = new string[] { "Title" };
            webLists.DataBind();
            Controls.Add(webLists);
            webLists.SelectedIndexChanged += new EventHandler(webLists_SelectedIndexChanged);
        }

        void webLists_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListName = webLists.SelectedValue.ToString();
        }

        public void GetFieldValue(FieldCallback callback)
        {
            callback(Schema.GetValue(this));
        }

        public PropertyDescriptor Schema
        {
            get
            {
                PropertyDescriptorCollection props = TypeDescriptor.GetProperties(this);
                return props.Find("ListName", false);
            }
        }

        [ConnectionProvider("List Name Selection Provider")]
        public IWebPartField GetFieldInterface()
        {
            return this;
        }

    }
}
