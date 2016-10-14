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

namespace WebPartPageProject.ConnectedWebPart
{
    [ToolboxItemAttribute(false)]
    public class SourceWebPart : WebPart, IImageSelectorProvider
    {

        private GroupedDropDownList list;

        public SourceWebPart()
        {
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
                Button b = new Button();
                b.Text = "Select Image";
                Controls.Add(b);
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
        public IImageSelectorProvider GetCustomerProvider()
        {
            return this;
        }

    }
}
