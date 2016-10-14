using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Collections.Generic;

namespace WebPartPageProject.BingWebPart
{
    [ToolboxItemAttribute(false)]
    public class BingWebPart : WebPart
    {
        // Visual Studio might automatically update this path when you change the Visual Web Part project item.
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/WebPartPageProject/BingWebPart/BingWebPartUserControl.ascx";

        public BingWebPart()
        {
        }

        protected override void CreateChildControls()
        {
            BingWebPartUserControl control = this.Page.LoadControl(_ascxPath) as BingWebPartUserControl;
            Controls.Add(control);            
            base.CreateChildControls();
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            base.RenderContents(writer);
        }

        public override EditorPartCollection CreateEditorParts()
        {
            List<EditorPart> parts = new List<EditorPart>();
            EditorPart edit = new CoordinatesEditorPart();
            edit.ID = this.ID + "_coordEditor";
            parts.Add(edit);
            return new EditorPartCollection(base.CreateEditorParts(), parts);
        }

        [WebBrowsable(false)]
        [Personalizable(true)]
        public Coordinates CenterCoordinate
        {
            get;
            set;
        }

        [WebBrowsable(false)]
        [Personalizable(true)]
        public Coordinates PushPin
        {
            get;
            set;
        }

    }
}
