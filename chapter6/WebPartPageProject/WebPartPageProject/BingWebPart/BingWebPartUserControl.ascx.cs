using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Globalization;

namespace WebPartPageProject.BingWebPart
{
    public partial class BingWebPartUserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public Unit Width
        {
            get { return Unit.Parse(spBingMap.Style[HtmlTextWriterStyle.Width]); }
            set { spBingMap.Style[HtmlTextWriterStyle.Width] = value.ToString(); }
        }

        public Unit Height
        {
            get { return Unit.Parse(spBingMap.Style[HtmlTextWriterStyle.Height]); }
            set { spBingMap.Style[HtmlTextWriterStyle.Height] = value.ToString(); }
        }

        public decimal Longitude
        {
            get { return Convert.ToDecimal(latField.Value); }
            set { latField.Value = value.ToString(CultureInfo.InvariantCulture); }
        }

        public decimal Latitude
        {
            get { return Convert.ToDecimal(lngField.Value); }
            set { lngField.Value = value.ToString(CultureInfo.InvariantCulture); }
        }

    }
}
