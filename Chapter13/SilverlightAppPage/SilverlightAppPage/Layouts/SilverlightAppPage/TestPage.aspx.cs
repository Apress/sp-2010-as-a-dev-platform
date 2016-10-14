using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.IO;
using System.Linq;
using System.Web.UI;
using Microsoft.SharePoint.Utilities;

namespace Apress.SP2010.SilverlightApps.Layouts.SilverlightAppPage
{
    public partial class TestPage : LayoutsPageBase
    {

        const string CLIENTBIN = "ClientBin";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var files = from file in Directory.GetFiles(Server.MapPath(CLIENTBIN), "*.xap", SearchOption.AllDirectories)
                            let fi = new FileInfo(file)
                            select new
                            {
                                Text = Path.GetFileNameWithoutExtension(file),
                                Value = String.Concat(
                                            CLIENTBIN, 
                                            Html32TextWriter.SlashChar,
                                            fi.Directory.Name,
                                            Html32TextWriter.SlashChar,
                                            fi.Name)
                            };
                ddlXap.DataValueField = "Value";
                ddlXap.DataTextField = "Text";
                ddlXap.DataSource = files;
                ddlXap.DataBind();

                //SPUtility.GetUrlDirectory();
            }
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            lblXapFile.Text = ddlXap.SelectedItem.Text;
            xapParam.Attributes["value"] = ddlXap.SelectedItem.Value;
        }

    }
}
