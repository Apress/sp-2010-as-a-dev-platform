using System;
using Microsoft.SharePoint.WebControls;

namespace APRESS.SP2010.SolutionValidatorDemo.Layouts.SolutionValidatorDemo
{
    public partial class SolutionValidationErrorPage : LayoutsPageBase
    {
        public string ErrorMessage= String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["ErrorMessage"] != null)
            {
                this.ErrorMessage = Request.QueryString["ErrorMessage"].ToString();
            }
            
        }
    }
}
