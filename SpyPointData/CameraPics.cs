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
        public string CameraName { get; set; }
        public bool HaveCardPic { get; set; }
        public string CardPicFilename { get; set; }
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
