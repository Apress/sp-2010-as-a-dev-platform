using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.WebControls;

namespace WebPartPageProject.BingWebPart
{
    public class CoordinatesEditorPart : EditorPart
    {

        private const string _ascxPath = @"~/_CONTROLTEMPLATES/WebPartPageProject/BingWebPart/CoordinatesEditor.ascx";

        public CoordinatesEditorPart()
            : base()
        {
        }

        BingWebPart webPart;
        CoordinatesEditor center;
        CoordinatesEditor pushp;

        protected override void CreateChildControls()
        {
            center = this.Page.LoadControl(_ascxPath) as CoordinatesEditor;
            center.Title = "Center Coordinates";
            pushp = this.Page.LoadControl(_ascxPath) as CoordinatesEditor;
            pushp.Title = "PushPin Coordinates";
            Controls.Add(center);
            Controls.Add(pushp);
            base.CreateChildControls();
            webPart = (BingWebPart)this.WebPartToEdit;
            // Add Coordinates as Min:Sec:Fragment and return as map compatible float
            center.Coordinate = webPart.CenterCoordinate;
            pushp.Coordinate = webPart.PushPin;
        }

        public override bool ApplyChanges()
        {
            EnsureChildControls();
            // check pushpin
            if (pushp.Coordinate != Coordinates.Empty)
            {
                webPart.CenterCoordinate = center.Coordinate;
                webPart.PushPin = pushp.Coordinate;
            }
            return true;
        }

        public override void SyncChanges()
        {
            EnsureChildControls();
            webPart.CenterCoordinate = center.Coordinate;
            webPart.PushPin = pushp.Coordinate;
        }
    }
}
