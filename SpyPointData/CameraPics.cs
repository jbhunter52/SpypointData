using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public List<Header3> headers { get; set; }
    }

    public class Hd
    {
        public string verb { get; set; }
        public string path { get; set; }
        public string host { get; set; }
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
        public Small small { get; set; }
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

        public bool CheckFilter(FilterCriteria fc)
        {
            Photo p = this;
            bool keep = true;
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
            return keep;
        }

        public string GetNodeName()
        {
            return this.originDate.ToShortDateString() + ", " + this.originDate.ToString("hh:mm:ss tt");
        }
    }

    public class CameraPics
    {
        public List<Photo> photos { get; set; }
        public string cameraId { get; set; }
        public int countPhotos { get; set; }

        public CameraPics(string id)
        {
            photos = new List<Photo>();
            cameraId = id;
            countPhotos = 0;
        }
        public void Add(CameraPics cp)
        {
            photos.AddRange(cp.photos);
            countPhotos = photos.Count;
        }
    }
}
