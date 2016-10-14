using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Resources;
using System.Web;

namespace ResourcesWebPart
{
    [ToolboxItemAttribute(false)]
    public class ResourceNonVisual : WebPart
    {
        public ResourceNonVisual()
        {
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            Label l = new Label();

        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            base.RenderContents(writer);
        }
    }
}
