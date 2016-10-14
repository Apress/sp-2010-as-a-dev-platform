using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Security.Principal;

namespace UserManagement.Layouts.UserManagement
{



    public partial class ElevatedPrivileges : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // elevate priv's
            string name;
            SPSecurity.RunWithElevatedPrivileges(() => name = WindowsIdentity.GetCurrent().Name);

            // check permissions
            if ((Web.EffectiveBasePermissions & SPBasePermissions.ViewListItems) == SPBasePermissions.ViewListItems)
            {

            }

        }

        private void GetName(string s)
        {
            return ;
        }

    }

  
}
