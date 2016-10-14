using System;
using System.Diagnostics;
using System.ComponentModel;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using APRESS.SP2010.FullTrustProxy;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;

namespace APRESS.SP2010.SandboxedWebPart.SandboxWebPart
{
    [ToolboxItemAttribute(false)]
    public class SandboxWebPart : WebPart
    {
        private string myLists;
        private ListBox myListsBox = new ListBox();
        private Label result = new Label();
        public SandboxWebPart()
        {

            try
            {
                //string siteUrl = "http://localhost"; //SPContext.Current.Site// Page.Request.QueryString["site"];

                //SPContext.Current.Site.RootWeb
                //using (SPSite siteCollection = new SPSite(siteUrl))
                {
                    using (SPWeb site = SPContext.Current.Site.RootWeb) // siteCollection.OpenWEb()
                    {
                        SPListCollection lists = site.Lists;
                        foreach (SPList list in lists)
                        {
                           myListsBox.Items.Add(list.Title);
                           myLists += list.Title + Environment.NewLine;
                        }
                        

                    }
                }
               

                FullTrustProxyArgs proxyArgs = new FullTrustProxyArgs(myLists);
                SPUtility.ExecuteRegisteredProxyOperation(
              proxyArgs.FullTrustProxyOpsAssemblyName,
              proxyArgs.FullTrustProxyOpsTypeName,
              proxyArgs);
              
            }
            catch (Exception ex)
            {
                result.Text = ex.Message;
               
            }

        }

        protected override void CreateChildControls()
        {
            this.Controls.Add(myListsBox);
            this.Controls.Add(result);

            base.CreateChildControls();
        }
    }
}
