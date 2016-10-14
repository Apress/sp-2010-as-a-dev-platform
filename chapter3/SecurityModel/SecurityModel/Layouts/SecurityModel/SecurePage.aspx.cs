using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.Utilities;
using System.Security;

namespace SecurityModel.Layouts.SecurityModel
{
    public partial class SecurePage : LayoutsPageBase
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
                // Version 1
                // SPUtility.Redirect(SPUtility.AccessDeniedPage, SPRedirectFlags.RelativeToLayoutsPage, Context);

                // Version 2
                SecurityException ex = new SecurityException();
                SPUtility.HandleAccessDenied(ex);
            }
        }
    }
}
