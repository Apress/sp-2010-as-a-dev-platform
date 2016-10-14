using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Resources;

namespace Apress.SP2010.ResourceWebPart
{
    [ToolboxItemAttribute(false)]
    public class NonVisualWebPart : WebPart
    {

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            string script = "Apress.SP2010.ResourceWebPart.NonVisualWebPart.MyResources.Alert.js";
            if (!Page.ClientScript.IsClientScriptBlockRegistered(script))
            {
                Page.ClientScript.RegisterClientScriptResource(this.GetType(), script);
            }
        }


        protected override void CreateChildControls()
        {
            Button b = new Button();
            ResourceManager rm = new ResourceManager("Apress.SP2010.ResourceWebPart.NonVisualWebPart", this.GetType().Assembly);
            b.Text = rm.GetString("Button.Text");
            b.UseSubmitBehavior = false;
            b.OnClientClick = "ShowAlert(this)";
            Controls.Add(b);
            ResourceManager rm2 = new ResourceManager("Apress.SP2010.ResourceWebPart.NonVisualWebPart.MyResources.Strings", this.GetType().Assembly);
            Label l = new Label();
            l.Text = rm2.GetString("Product1");
            Controls.Add(l);

            // Assembly Web Resource



        }
    }
}
