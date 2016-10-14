using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace SandboxWebPartProject.SandboxWebPart
{
    [ToolboxItemAttribute(false)]
    public class SandboxWebPart : WebPart
    {
        public SandboxWebPart()
        {
        }

        protected override void CreateChildControls()
        {
            Label l = new Label();
            l.Text = "Test";
            base.Controls.Add(l);
            base.CreateChildControls();
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            base.RenderContents(writer);
        }
    }
}
