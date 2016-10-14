using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace UserManagement.Layouts.UserManagement
{
    public partial class AddGroup : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetGroups();
            }
        }

        private void GetGroups()
        {
            ddlGroups.DataSource = Web.Groups;
            ddlGroups.DataTextField = "Name";
            ddlGroups.DataValueField = "ID";
            ddlGroups.DataBind();
        }

        protected void btnAddGroups_Click(object sender, EventArgs e)
        {
            // Retrieve root collection
            SPSite site = new SPSite("http://joerg-netbook");
            using (SPWeb web = site.OpenWeb())
            {
                string newGroup = txtGroup.Text;
                // create
                web.SiteGroups.Add(newGroup, web.CurrentUser, web.CurrentUser, "A group created by code");
                // assign to site
                SPGroup group = web.SiteGroups[newGroup];
                SPRoleAssignment roles = new SPRoleAssignment(group);
                SPRoleDefinition perms = web.RoleDefinitions["Full Control"];
                roles.RoleDefinitionBindings.Add(perms);
                web.RoleAssignments.Add(roles);
                // add users to this group
                SPUser user1 = web.AllUsers[@"joerg-netbook\bernd"];
                SPUser user2 = web.AllUsers[@"joerg-netbook\martin"];
                group.AddUser(user1);
                group.AddUser(user2);
                
            }
            GetGroups();
        }
    }
}
