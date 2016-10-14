using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Web.UI;

namespace WebPartPageProject.BingWebPart
{

    public struct DegMinSec
    {
        public int Deg { get; set; }
        public int Min { get; set; }
        public int Sec { get; set; }
    }

    public struct Coordinates
    {

        public Coordinates(DegMinSec lat, DegMinSec lng) : this()
        {
            SetLongitude(lng);
            SetLatitude(lat);

        }

        public static Coordinates Empty
        {
            get
            {
                return new Coordinates();
            }
        }

        public static bool operator ==(Coordinates c1, Coordinates c2)
        {
            return (c1.Latitude == c2.Latitude && c1.Longitude == c2.Longitude);
        }

        public static bool operator !=(Coordinates c1, Coordinates c2)
        {
            return (c1.Latitude != c2.Latitude || c1.Longitude != c2.Longitude);
        }

        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public bool IsInRange(Coordinates from, Coordinates to)
        {
            return (
                this.Longitude > from.Longitude && this.Latitude > from.Latitude &&
                this.Longitude < to.Longitude && this.Latitude < to.Latitude);
        }

        public DegMinSec LatitudeDegrees
        {
            get
            {
                return GetDegMinSec(Latitude);
            }
        }

        public DegMinSec LongitudeDegrees
        {
            get
            {
                return GetDegMinSec(Longitude);
            }
        }

        private static DegMinSec GetDegMinSec(decimal longlat)
        {
            int deg = (int)Math.Truncate(longlat);  // 121
            decimal mins = (longlat - deg) * 60;    // .1356 * 60
            int min = (int)Math.Truncate(mins);
            int sec = (int)(mins - min) * 60;
            return new DegMinSec()
            {
                Deg = deg,
                Min = min,
                Sec = sec
            };
        }

        public void SetLongitude(DegMinSec t)
        {
            Longitude = t.Deg + t.Min + t.Sec;
        }

        public void SetLatitude(DegMinSec t)
        {
            Latitude = t.Deg + t.Min + t.Sec;
        }

        public override string ToString()
        {
            return String.Format(CultureInfo.CurrentCulture, "{0}:{1}", Latitude, Longitude);
        }

        public override bool Equals(object obj)
        {
            if (obj is Coordinates)
            {
                return ((Coordinates)obj) == this;
            }
            return false;
        }

    }
}
