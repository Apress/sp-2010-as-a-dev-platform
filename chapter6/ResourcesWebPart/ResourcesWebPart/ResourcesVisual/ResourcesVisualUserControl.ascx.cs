using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web;

namespace ResourcesWebPart
{
    public partial class ResourcesVisualUserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                object data = HttpContext.GetGlobalResourceObject("ResourcesWebPart.StaticResource", "GlobalInfo");
                if (data != null)
                {
                    lblEmbedded.Text = data.ToString();
                }
            }
        }

    }
}
