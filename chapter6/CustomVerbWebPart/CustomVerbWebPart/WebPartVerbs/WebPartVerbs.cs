using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Collections.Generic;

namespace CustomVerbWebPart
{
    [ToolboxItemAttribute(false)]
    public class WebPartVerbs : WebPart
    {

        Label l;

        public WebPartVerbs()
        {
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnLoad(e);
            if (!Page.ClientScript.IsClientScriptBlockRegistered("ClientVerbScript"))
            {
                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientVerbScript",
                     String.Format(@"<script>
                         function SetBlueColorOnClient() {{
                         var label = document.getElementById('{0}');
                         label.style.color = 'Blue';                        
                          }}
                         </script>", l.ClientID));
            }
        }

        protected override void CreateChildControls()
        {
            l = new Label();
            l.Text = "Colorize this Label using the context menu";
            Controls.Add(l);
            base.CreateChildControls();
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            base.RenderContents(writer);
        }

        public override WebPartVerbCollection Verbs
        {
            get
            {
                List<WebPartVerb> verbs = new List<WebPartVerb>();
                // Client Side Handler
                WebPartVerb verbClient = new WebPartVerb(this.ID + "clientVerb1",
                                             "SetBlueColorOnClient()");
                verbClient.Text = "Set Blue Color (Client)";
                verbClient.Description = "Invokes a JavaScript Method";
                verbs.Add(verbClient);
                // Server Side Handler
                WebPartEventHandler handler = new
                                               WebPartEventHandler(SetRedColorServerClick);
                WebPartVerb verbServer = new WebPartVerb(this.ID + "serverVerb1", handler);
                verbServer.Text = "Set Red Color (Server)";
                verbServer.Description = "Invokes a post back";
                verbs.Add(verbServer);
                // add
                return new WebPartVerbCollection(base.Verbs, verbs);
            }
        }

        private void SetRedColorServerClick(object sender, WebPartEventArgs e)
        {
            EnsureChildControls();
            l.ForeColor = System.Drawing.Color.Red;
        }

    }
}
