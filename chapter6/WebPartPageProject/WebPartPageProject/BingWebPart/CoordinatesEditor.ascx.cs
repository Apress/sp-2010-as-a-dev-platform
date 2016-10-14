using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using WebPartPageProject.BingWebPart;

namespace WebPartPageProject.BingWebPart
{
    public partial class CoordinatesEditor : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public Coordinates Coordinate
        {
            set
            {
                DegMinSec lat = value.LatitudeDegrees;
                DegMinSec lng = value.LongitudeDegrees;
                TextBoxDegLat.Text = lat.Deg.ToString();
                TextBoxMinLat.Text = lat.Min.ToString();
                TextBoxSecLat.Text = lat.Sec.ToString();
                TextBoxDegLng.Text = lng.Deg.ToString();
                TextBoxMinLng.Text = lng.Min.ToString();
                TextBoxSecLng.Text = lng.Sec.ToString();
            }
            get
            {
                DegMinSec lat = new DegMinSec();
                DegMinSec lng = new DegMinSec();
                lat.Deg = Int32.Parse(TextBoxDegLat.Text);
                lat.Deg = Int32.Parse(TextBoxMinLat.Text);
                lat.Deg = Int32.Parse(TextBoxSecLat.Text);
                return new Coordinates(lat, lng);
            }

        }

        public string Title
        {
            get { return lblControl.Text; }
            set { lblControl.Text = value; }

        }
    }
}