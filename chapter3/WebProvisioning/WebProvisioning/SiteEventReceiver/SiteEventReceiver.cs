using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;

namespace WebProvisioning.SiteEventReceiver
{
    /// <summary>
    /// Web Events
    /// </summary>
    public class SiteEventReceiver : SPWebEventReceiver
    {
       /// <summary>
       /// A site is being deleted.
       /// </summary>
       public override void WebDeleting(SPWebEventProperties properties)
       {
           try
           {
               if (properties.Web.Lists["Data"] != null)
               {
                   properties.Cancel = true;
               }
           }
           catch
           {
               //throw new SPException("Cannot delete the site because it has user data in it");
           }
           base.WebDeleting(properties);
       }

       public override void WebAdding(SPWebEventProperties properties)
       {
           base.WebAdding(properties);
       }

       /// <summary>
       /// A site is being provisioned.
       /// </summary>
       public override void WebProvisioned(SPWebEventProperties properties)
       {
           properties.Web.Title += String.Format(" [Created By: {0}]", properties.UserDisplayName);
           properties.Web.AllowUnsafeUpdates = true;
           properties.Web.Update();
           base.WebAdding(properties);
       }

    }
}
