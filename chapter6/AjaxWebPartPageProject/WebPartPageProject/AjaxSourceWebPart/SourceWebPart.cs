using System;
using System.IO;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.Utilities;
using System.Web.UI.HtmlControls;
using Microsoft.SharePoint.WebPartPages.Communication;
using Microsoft.SharePoint.WebPartPages;
using Microsoft.SharePoint.Workflow;

namespace AjaxWebPartPageProject.ConnectedWebPart
{

    public interface IEventWebPartField
    {
        event EventHandler ImageChanged;
        string ImageName { get; }
    }

    [ToolboxItemAttribute(false)]
    public class AjaxSourceWebPart : System.Web.UI.WebControls.WebParts.WebPart, IEventWebPartField
    {

        private GroupedDropDownList list;

        public AjaxSourceWebPart()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            WebPartManager.ConnectionsActivated += new EventHandler(WebPartManager_ConnectionsActivated);
        }

        void WebPartManager_ConnectionsActivated(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //
            }
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            try
            {

                // few controls with id's sent to target
                string path = SPUtility.GetGenericSetupPath("TEMPLATE\\IMAGES");
                // Group Drop Box
                SPHtmlSelect dlGroup = new SPHtmlSelect();
                dlGroup.ID = this.ID + "dlGroup";
                dlGroup.Height = 22;
                dlGroup.Width = 100;
                Controls.Add(dlGroup);
                SPHtmlSelect dlCandidate = new SPHtmlSelect();
                dlCandidate.ID = this.ID + "dlCandidate";
                dlCandidate.Height = 22;
                Controls.Add(dlCandidate);
                // Put the button into the panel
                UpdatePanel panel = new UpdatePanel()
                {
                    ID = this.SkinID + "updatePanel",
                    ChildrenAsTriggers = false,
                    UpdateMode = UpdatePanelUpdateMode.Conditional
                };
                Button b = new Button();
                b.Text = "Select Image";
                b.Click += new EventHandler(Button_OnClick);
                panel.ContentTemplateContainer.Controls.Add(b);
                Controls.Add(panel);
                // Register for async
                ScriptManager sc = ScriptManager.GetCurrent(Page);
                if (sc != null)
                {
                    sc.RegisterAsyncPostBackControl(b);
                }
                //
                Controls.Add(new HtmlGenericControl("br"));
                HtmlGenericControl lblText = new HtmlGenericControl("span");
                lblText.ID = this.ID + "lblText";
                lblText.InnerText = "No image selected";
                Controls.Add(lblText);
                list = new GroupedDropDownList();
                list.GroupControlId = dlGroup.ID;
                list.CandidateControlId = dlCandidate.ID;
                list.DescriptionControlId = lblText.ID;
                string filter = (Page.IsPostBack && dlGroup.Items.Count > 0) ? dlGroup.Items[dlGroup.SelectedIndex].Value : "*.*";
                foreach (string file in Directory.GetFiles(path, filter))
                {
                    list.AddItem(
                        Path.GetFileName(file),
                        Path.GetFileNameWithoutExtension(file),
                        file,
                        Path.GetExtension(file).ToLowerInvariant());
                }
                Controls.Add(list);
            }
            catch
            {
            }
        }

        protected void Button_OnClick(object sender, EventArgs e)
        {
            OnImageChanged();
        }


        protected override void RenderContents(HtmlTextWriter writer)
        {
            base.RenderContents(writer);
        }

        public string ImageName
        {
            get 
            { 
                return (list == null) ? null : list.Value; 
            }
        }

        [ConnectionProvider("Image Name", AllowsMultipleConnections=false)]
        public IEventWebPartField GetCustomerProvider()
        {
            return this;
        }

        public event EventHandler ImageChanged;

        protected void OnImageChanged()
        {
            if (ImageChanged != null)
            {
                ImageChanged(this, EventArgs.Empty);
            }
        }

        public PropertyDescriptor Schema
        {
            get { return TypeDescriptor.GetProperties(typeof(string))[0]; }
        }
    }
}
