using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.WebPartPages;

namespace VisualWebPartEditor.VisualWebPart1
{
    [ToolboxItemAttribute(false)]
    public class VisualWebPart1 : System.Web.UI.WebControls.WebParts.WebPart
    {
        // Visual Studio might automatically update this path when you change the Visual Web Part project item.
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/VisualWebPartEditor/EditorWebPart/VisualWebPart1UserControl.ascx";

        public VisualWebPart1()
        {
            ComplexUrl = "http://www.apress.com";
        }


        [WebBrowsable(true)]
        [Personalizable(true)]
        [WebDisplayName("Complex Url")]
        [HtmlDesignerAttribute(@"/_layouts/VisualWebPartEditor/PopupEditor.aspx", DialogFeatures = "center:yes; dialogHeight:200px", HtmlEditorBuilderType = BrowserBuilderType.Dynamic)]
        public string ComplexUrl
        {
            get;
            set;
        }

        protected override void CreateChildControls()
        {
            Control control = this.Page.LoadControl(_ascxPath);
            Controls.Add(control);
            base.CreateChildControls();
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            base.RenderContents(writer);
        }
    }
}
