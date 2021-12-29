using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

using Cyotek.Windows.Forms;
using System.Windows.Forms;

namespace SpyPointData
{
    public class Header
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    public class Small
    {
        public string verb { get; set; }
        public string path { get; set; }
        public string host { get; set; }
        [JsonIgnore]
        public List<Header> headers { get; set; }
    }
    public class Header2
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    public class Medium
    {
        public string verb { get; set; }
        public string path { get; set; }
        public string host { get; set; }
        [JsonIgnore]
        public List<Header2> headers { get; set; }
    }
    public class Header3
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    public class Large
    {
        public string verb { get; set; }
        public string path { get; set; }
        public string host { get; set; }
        [JsonIgnore]
        public List<Header3> headers { get; set; }
    }

    public class Hd
    {
        public string verb { get; set; }
        public string path { get; set; }
        public string host { get; set; }
        [JsonIgnore]
        public List<object> headers { get; set; }
    }

    public class Photo
    {
        public string id { get; set; }
        public DateTime date { get; set; }
        public List<string> tag { get; set; }
        public string originName { get; set; }
        public int originSize { get; set; }
        public DateTime originDate { get; set; }
        [JsonIgnore]
        public Small small { get; set; }
        [JsonIgnore]
        public Medium medium { get; set; }
        public Large large { get; set; }
        public Hd hd { get; set; }
        public bool hdPending { get; set; }
        public string FileName { get; set; }
        public bool Buck { get; set; }
        public bool Deer { get; set; }
        public int BuckAge { get; set; }
        public string CameraName { get; set; }
        public bool HaveCardPic { get; set; }
        public string CardPicFilename { get; set; }
        public bool HaveLocation { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool HidePhoto { get; set; }
        public bool New { get; set; }

        public string GetSimpleCameraName()
        {
            if (CameraName == null)
                return "null";
            return System.Text.RegularExpressions.Regex.Replace(CameraName, @"[\d-]", string.Empty).Replace(".","").Trim();
        }
        public bool CheckFilter(FilterCriteria fc)
        {
            Photo p = this;
            bool keep = true;

            if (fc.Nothing)
            {
                //if (p.Buck)
                //    keep = false;
                if (p.Deer)
                    keep = false;
            }

            if (fc.New)
                if (p.New == false)
                    keep = false;

            if (fc.Deer)
            {
                if (p.Deer == false)
                    keep = false;
            }
            if (fc.Buck)
            {
                if (p.Buck == false)
                    keep = false;
                else //Photo is buck, filter by age as well
                {
                    if (p.BuckAge == 0 && !fc.Age0)
                        keep = false;
                    if (p.BuckAge == 1 && !fc.Age1)
                        keep = false;
                    if (p.BuckAge == 2 && !fc.Age2)
                        keep = false;
                    if (p.BuckAge == 3 && !fc.Age3)
                        keep = false;
                    if (p.BuckAge == 4 && !fc.Age4)
                        keep = false;

                }
            }
            if (fc.Date)
            {
                DateTime picTime = p.originDate;
                DateTime minDateTime = fc.MinDate;
                DateTime maxDateTime = fc.MaxDate;

                if (fc.DateIgnoreYear)
                {
                    picTime = DateTimeSetYear(picTime, 1990);
                    minDateTime = DateTimeSetYear(minDateTime, 1990);
                    maxDateTime = DateTimeSetYear(maxDateTime, 1990);



                    if (maxDateTime < minDateTime)
                    {
                        if (picTime < minDateTime && picTime < maxDateTime)
                            picTime = DateTimeSetYear(picTime, 1991);
                        maxDateTime = DateTimeSetYear(maxDateTime, 1991);
                    }
                }

                if (picTime < minDateTime)
                    keep = false;
                if (picTime > maxDateTime)
                    keep = false;

            }
            if (fc.LocationOn)
            {
                
                if (!fc.Locations.Contains(p.GetSimpleCameraName()))
                    keep = false;
            }
            if (fc.RectangleOn)
            {
                if (!fc.InRectangle(p.Latitude, p.Longitude))
                    keep = false;
            }    
            return keep;
        }

        public DateTime DateTimeSetYear(DateTime dt, int year)
        {
            return new DateTime(year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
        }

        public string GetNodeName()
        {
            return this.originDate.ToShortDateString() + ", " + this.originDate.ToString("hh:mm:ss tt");
        }

        public void ForceId()
        {
            if (id == null)
            {
                id = Guid.NewGuid().ToString();
            }
        }

        public System.Drawing.Image GetPhotoFromFile()
        {
            string file = GetBestPhotoFile();
            if (file != null && System.IO.File.Exists(file))
            {
                System.Drawing.Image img;
                using (var bmpTemp = new System.Drawing.Bitmap(file))
                {
                    img = new System.Drawing.Bitmap(bmpTemp);
                }
                return img;
            }



            return null;
        }
        public string GetBestPhotoFile()
        {
            if (CardPicFilename != null)
            {
                return CardPicFilename;
            }
            if (FileName != null)
            {
                return FileName;
            }
            else
                return null;
        }

        public void PicToImageBox(ImageBox ib)
        {
            System.Drawing.Image image = GetPhotoFromFile();
            ib.Dock = DockStyle.Fill;
            ib.Image = image;
            ib.ZoomToFit();
            ib.Tag = id;
        }
    }

    public class CameraPics
    {
        public List<Photo> photos { get; set; }
        public string cameraId { get; set; }
        public int countPhotos { get; set; }
        public bool HaveLocation { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public CameraPics(string id)
        {
            photos = new List<Photo>();
            cameraId = id;
            countPhotos = 0;
        }
        public List<Photo> GetFilteredPhotos(FilterCriteria fc)
        {
            List<Photo> filtered = new List<Photo>();
            foreach (Photo p in this.photos)
            {
                if (p.CheckFilter(fc))
                    filtered.Add(p);
            }
            return filtered;
        }
        public void Add(CameraPics cp)
        {
            photos.AddRange(cp.photos);
            countPhotos = photos.Count;
        }
    }
}
