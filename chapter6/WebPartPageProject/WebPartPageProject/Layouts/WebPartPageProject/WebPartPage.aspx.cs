using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.WebPartPages;
using WP=System.Web.UI.WebControls.WebParts;
using System.Web.UI.WebControls;

namespace WebPartPageProject.Layouts.WebPartPageProject
{
    public partial class WebPartPage : LayoutsPageBase
    {

        SPWebPartManager spWebPartManager;

        protected void Page_Load(object sender, EventArgs e)
        {

            spWebPartManager = WP.WebPartManager.GetCurrentWebPartManager(this) as SPWebPartManager;


            if (spWebPartManager.SupportedDisplayModes.Contains(WP.WebPartManager.CatalogDisplayMode))
            {
                lnkCatM.Visible = true;
            }
            else
            {
                lnkCatM.Visible = false;
            }

            //SPContext.Current.Web.AllowUnsafeUpdates = true; // GET
            //SPList list = SPContext.Current.Web.Lists["AutThors"];
            //listView1.ListName = list.ID.ToString("B").ToUpper();
            //listView1.ViewGuid = list.Views[0].ID.ToString("B").ToUpper();
            //spWebPartManager.AddWebPart(listView1, leftZone, 1);


        }

        protected void btnAddWebpart_Click(object sender, EventArgs e)
        {

            //Label l = new Label();
            //l.Text = "Test";
            //l.ID = "lbl" + Guid.NewGuid().ToString();
            //WP.GenericWebPart lwp = spWebPartManager.CreateWebPart(l);
            //lwp.Title = "Label Wrapped as WebPArt";
            //spWebPartManager.AddWebPart(lwp, leftZone, 0);

            //CustomWebPart wp = new CustomWebPart();
            //spWebPartManager.AddWebPart(wp, leftZone, 0);
            //wp.Title = "Generic Webpart";
            //wp.Description = "Generic WebPart Description";
            //wp.AllowClose = false;
        }

        protected void lnkCatMode_Click(object sender, EventArgs e)
        {
            spWebPartManager.DisplayMode = WP.WebPartManager.CatalogDisplayMode;
        }

        protected void lnkMode_Click(object sender, EventArgs e)
        {
            if (spWebPartManager != null)
            {
                if (lnkMode.Text == "Design Mode")
                {
                    spWebPartManager.DisplayMode = WP.WebPartManager.BrowseDisplayMode;
                    lnkMode.Text = "Browse Mode";
                }
                else
                {
                    spWebPartManager.DisplayMode = WP.WebPartManager.DesignDisplayMode;
                    lnkMode.Text = "Design Mode";
                }
            }
        }

    }
}
