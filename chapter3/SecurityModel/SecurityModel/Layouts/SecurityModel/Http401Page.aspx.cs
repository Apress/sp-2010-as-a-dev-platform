using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.Utilities;
using System.Security;

namespace SecurityModel.Layouts.SecurityModel
{
    public partial class Http401Page : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // This page requires Admin permissions
            if (Web.CurrentUser.IsSiteAdmin)
            {
                lblName.Text = Web.CurrentUser.Name;
            }
            else
            {
                SecurityException ex = new SecurityException();
                SPUtility.SendAccessDeniedHeader(ex);
            }
        }
    }
}
