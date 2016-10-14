using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Diagnostics;

namespace SecurityWebPart.WebParts
{
    [ToolboxItemAttribute(false)]
    [EventLogPermission(System.Security.Permissions.SecurityAction.Demand)]
        public class WebPartMinimalTrust : WebPart
    {
        public WebPartMinimalTrust()
        {
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            Label l = new Label();
            // Access something minimal trust doas not allows
            EventLog log = new EventLog("Application");
            log.WriteEntry("WebPart was Show", EventLogEntryType.Information, 10000, 4);

            l.Text = "Hello Security Web Part";
            Controls.Add(l);
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            base.RenderContents(writer);
        }
    }
}
