using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;

namespace Apress.SP2010.VisualWebPartProject
{
    public partial class VisualWebPart1UserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (SPMonitoredScope scope = new SPMonitoredScope(this.GetType().Name))
            {
                Button b = new Button();
                b.Text = "Click me (Embedded Web Part)!";
                Controls.Add(b);
                using (SPMonitoredScope scopeInner = new SPMonitoredScope("Inner Scope"))
                {
                    System.Threading.Thread.Sleep(500); // lengthy operation
                }
            }
        }
    }
}
