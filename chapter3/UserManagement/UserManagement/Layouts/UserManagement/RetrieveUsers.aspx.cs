using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web.UI.WebControls;

namespace UserManagement.Layouts.UserManagement
{
    public partial class RetrieveUsers : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // if not an application page: SPContext.Current.Web.AllUsers:
                gvUsers.DataSource = Web.AllUsers;
                gvUsers.DataBind();

            }

            // Current user:
            lblCurrent.Text = Web.CurrentUser.Name;
            // Site Access with another user (uncomment to run)
            //SPUser userAlex = Web.AllUsers[@"JOERG-NETBOOK\joerg"];
            //SPSite site = new SPSite("http://joerg-netbook", userAlex.UserToken);
            //using (SPWeb web = site.OpenWeb())
            //{
            //    lblCurrent.Text = web.CurrentUser.LoginName;
            //}

        }

        protected void gvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string name = gvUsers.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text;
            SPUser user = Web.AllUsers[name];
            SPPrincipal p = user as SPPrincipal;
            SPRoleAssignment roles = Web.RoleAssignments.GetAssignmentByPrincipal(p);
            gvRoles.DataSource = roles;
            gvGroups.DataSource = user.Groups;
            DataBind();
        }

    }
}
