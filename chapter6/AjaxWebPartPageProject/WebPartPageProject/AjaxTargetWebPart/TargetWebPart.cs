using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.Utilities;

namespace AjaxWebPartPageProject.ConnectedWebPart
{
    [ToolboxItemAttribute(false)]
    public class AjaxTargetWebPart : WebPart
    {

        private IEventWebPartField customerProvider;
        private UpdatePanel panel;

        public AjaxTargetWebPart()
        {
        }

        [ConnectionConsumer("Image Name")]
        public void RegisterCustomerProvider(IEventWebPartField provider)
        {
            this.customerProvider = provider;
            this.customerProvider.ImageChanged += new EventHandler(customerProvider_ImageChanged);
        }

        void customerProvider_ImageChanged(object sender, EventArgs e)
        {
            panel.Update();
        }

        private string ImageName
        {
            get
            {
                return customerProvider.ImageName;
            }
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            Image img = new Image();
            panel = new UpdatePanel();
            if (customerProvider != null && ImageName != null)
            {
                string path = "/_layouts/images/";
                img.ImageUrl = SPContext.Current.Web.Url + path + ImageName;
                panel.ContentTemplateContainer.Controls.Add(img);
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
                panel.ContentTemplateContainer.Controls.Add(l);
            }
            Controls.Add(panel);
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            base.RenderContents(writer);
        }

        public bool ConnectionPointEnabled
        {
            get
            {
                object o = ViewState["ConnectionPointEnabled"];
                return (o != null) ? (bool)o : true;
            }
            set
            {
                ViewState["ConnectionPointEnabled"] = value;
            }
        }
    }
}