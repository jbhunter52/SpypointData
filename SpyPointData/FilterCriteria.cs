using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMap.NET;

namespace SpyPointData
{
    public class FilterCriteria
    {
        public bool New;
        public bool Nothing;
        public bool Buck;
        public bool Deer;
        public bool Age0;
        public bool Age1;
        public bool Age2;
        public bool Age3;
        public bool Age4;
        public bool Date;
        public DateTime MinDate;
        public DateTime MaxDate;
        public bool LocationOn;
        public List<string> Locations;
        public bool RectangleOn;
        public List<PointLatLng> RectanglePoints;

        private double MinLng;
        private double MaxLng;
        private double MinLat;
        private double MaxLat;

        public FilterCriteria()
        {
            New = false;
            Buck = false;
            Deer = false;
            Age0 = false;
            Age1 = false;
            Age2 = false;
            Age3 = false;
            Age4 = false;
            LocationOn = false;
            Locations = new List<string>();
            RectangleOn = false;
            RectanglePoints = new List<PointLatLng>();
            MinDate = new DateTime(2017, 1, 1,0,0,0);
            MaxDate = DateTime.Today;
        }

        public void SetRectBounds()
        {
            List<double> lat = new List<double>();
            List<double> lng = new List<double>();
            foreach (var pt in RectanglePoints)
            {
                lat.Add(pt.Lat);
                lng.Add(pt.Lng);
            }
            MinLng = lng.Min();
            MaxLng = lng.Max();
            MinLat = lat.Min();
            MaxLat = lat.Max();
        }

        public bool InRectangle(double lat, double lng)
        {
            if (lat >= MinLat && lat <= MaxLat)
            {
                if (lng >= MinLng && lng <= MaxLng)
                    return true;
            }
            return false;
        }
    }
}
