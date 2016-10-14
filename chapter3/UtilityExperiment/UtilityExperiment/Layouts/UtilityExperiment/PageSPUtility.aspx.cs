using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.Utilities;

namespace UtilityExperiment.Layouts.UtilityExperiment
{
    public partial class PageSPUtility : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           // SPUtility.FormatDate
            DateTime curDate = DateTime.UtcNow;
            DateTime regionDate = Web.RegionalSettings.TimeZone.UTCToLocalTime(curDate);
            lblData.Text = SPUtility.FormatDate(Web, curDate, SPDateFormat.ISO8601);
            
            // Get the 14-Hive filesystem path
            //Get path to features directory
            //Would typically return "C:\Program Files\Common Files\Microsoft Shared\web server extensions\12\TEMPLATE\FEATURES"
            string featurePath = SPUtility.GetGenericSetupPath("template\\features");
            lblData.Text = featurePath;

            //Get url to list item
            SPList list = Web.Lists["Authors"];
            SPListItem item = list.Items[0];
            string itemUrl = SPUtility.GetFullUrl(Web.Site, item.Url);

            //Redirect to specified page, adding querystring
            string url = "http://portal/TestResults/Pages/results.aspx";
            string queryString = "successflag=passed";
            SPUtility.Redirect(url, SPRedirectFlags.Default, Context, queryString);

            //Send email from current SPWeb
            SPWeb web = SPContext.Current.Site.OpenWeb();
            string subject = "Email from the " + web.Title + " web";
            string body = "The body of the email";

            SPUtility.SendEmail(web, false, false, "someone@somewhere.com", subject, body);

            //Transfer to Error Page
            Exception ex = new Exception("Some error message");
            SPUtility.TransferToErrorPage(ex.Message);

            //Transfer to success page, and specify url to move onto after "Ok" clicked
            SPUtility.TransferToSuccessPage("Operation was completed", @"/Docs/default.aspx", "Click here for more...", "success.aspx");

        }
    }
}
