using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.Utilities;

namespace WebPartPageProject.ConnectedWebPart
{
    [ToolboxItemAttribute(false)]
    public class TargetWebPart : WebPart
    {

        private IImageSelectorProvider customerProvider;

        public TargetWebPart()
        {
        }

        [ConnectionConsumer("Image Name")]
        public void RegisterCustomerProvider(IImageSelectorProvider provider)
        {
            this.customerProvider = provider;
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            Image img = new Image();
            if (customerProvider != null && customerProvider.ImageName != null)
            {
                string path = "/_layouts/images/";
                img.ImageUrl = SPContext.Current.Web.Url + path + customerProvider.ImageName;
                Controls.Add(img);
            }
            else
            {
                Label l = new Label();
                if (customerProvider == null)
                {
                    l.Text = "No Connection established.";
                }
                else
                {
                    l.Text = "No image selected.";
                }
                l.ForeColor = System.Drawing.Color.Red;
                Controls.Add(l);
            }
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            base.RenderContents(writer);
        }
    }
}
