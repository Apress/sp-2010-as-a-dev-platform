using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.Utilities;
using System.Collections.Generic;

namespace UtilityExperiment.Layouts.UtilityExperiment
{
    public partial class SecurityUtils : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool maxCount;
            IList<SPPrincipalInfo> users = SPUtility.SearchPrincipals(Web,
                txtInput.Text,
                SPPrincipalType.All,
                SPPrincipalSource.All,
                null,
                100,
                out maxCount);
            grdPrincipals.DataSource = users;
            grdPrincipals.DataBind();
        }
    }
}
