using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace VisualWebPartEditor.Layouts.VisualWebPartEditor
{
    public partial class PopupEditor : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((Microsoft.SharePoint.WebControls.DialogMaster)Master).OkButton.OnClientClick = "CloseOkButton();";
        }

        //protected string OkButtonID
        //{
        //    get
        //    {
        //        return ((Microsoft.SharePoint.WebControls.DialogMaster)Master).OkButton.ClientID;
        //    }
        //}

    }
}
