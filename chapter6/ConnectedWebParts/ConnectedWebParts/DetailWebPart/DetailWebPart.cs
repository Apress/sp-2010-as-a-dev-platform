using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace ConnectedWebParts
{
    [ToolboxItemAttribute(false)]
    public class DetailWebPart : WebPart
    {
        public DetailWebPart()
        {
            dataLabel = new Label();
        }

        private Label dataLabel;

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            Controls.Add(dataLabel);
        }

        [ConnectionConsumer("List Name Consumer", AllowsMultipleConnections = false)]
        public void SetFieldInterface(IWebPartField field)
        {
            field.GetFieldValue(new FieldCallback(SetLabel));
        }

        private void SetLabel(object fieldData)
        {
            if (fieldData != null)
            {
                SPList list = SPContext.Current.Web.Lists[fieldData.ToString()];
                dataLabel.Text = String.Format("List {0} with {1} items.",
                    list.Title,
                    list.ItemCount);
            }
        }

    }
}
