using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ExifLib;
using System.Windows.Forms;

namespace SpyPointData
{
    [Serializable]
    public class ManualPics
    {
        public List<Photo> Photos;
        public bool HidePics;
        public ManualPics()
        {
            Photos = new List<Photo>();
            HidePics = false;
        }

        public void AddPics(List<string> files)
        {
            if (Photos == null)
                Photos = new List<Photo>();
            string dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"ManualPics");
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            foreach (string file in files)
            {
                using (ExifReader reader = new ExifReader(file))
                {
                    DateTime date;
                    Double[] GpsLongArray;
                    Double[] GpsLatArray;
                    Double GpsLongDouble = 0;
                    Double GpsLatDouble = 0;
                    bool hasGPS = false;

                    if (reader.GetTagValue<Double[]>(ExifTags.GPSLongitude, out GpsLongArray)
                        && reader.GetTagValue<Double[]>(ExifTags.GPSLatitude, out GpsLatArray))
                    {
                        hasGPS = true;
                        GpsLongDouble = GpsLongArray[0] + GpsLongArray[1] / 60 + GpsLongArray[2] / 3600;
                        GpsLatDouble = GpsLatArray[0] + GpsLatArray[1] / 60 + GpsLatArray[2] / 3600;
                    }
                    if (reader.GetTagValue<DateTime>(ExifTags.DateTimeOriginal, out date))
                    {
                        string newName = Path.Combine(dir,Path.GetFileName(file));
                        int ind = Photos.FindIndex(p => p.FileName == newName);
                        if (ind < 0)
                        {
                            File.Copy(file, newName, true);

                            Photo p = new Photo();
                            p.CameraName = "Manual";
                            p.CardPicFilename = newName;
                            p.date = date;
                            p.HaveCardPic = true;
                            p.originDate = date;
                            p.id = GetRandomString() + GetRandomString();

                            if (hasGPS)
                            {
                                p.HaveLocation = true;
                                p.Latitude = GpsLatDouble;
                                p.Longitude = -GpsLongDouble;
                            }

                            Photos.Add(p);
                        }
                    }
                }
            }
        }
        public TreeNode GetNodes(FilterCriteria fc)
        {
            List<TreeNode> nodes = new List<TreeNode>();

            foreach (Photo p in Photos)
            {
                    TreeNode n = new TreeNode(GetNodeName(p));
                    n.Tag = p.id;
                    //if (p.Deer == null)
                    //    p.Deer = false;
                    //if (p.Buck == null)
                    //    p.Buck = false;
                    if (p.Buck)
                        n.ForeColor = System.Drawing.Color.DarkGreen;
                    if (p.Deer)
                        n.BackColor = System.Drawing.Color.LightGray;
                    //Apply filters
                    if (p.CheckFilter(fc))
                        nodes.Add(n);  
                                 
            }

            TreeNode manualNode = new TreeNode("ManualPics");
            manualNode.Nodes.AddRange(nodes.ToArray());
            return manualNode;
        }
        public List<Photo> GetFilteredPhotos(FilterCriteria fc)
        {
            List<Photo> photos = new List<Photo>();
            foreach (Photo p in Photos)
            {
                if (p.CheckFilter(fc))
                    photos.Add(p);
            }
            return photos;
        }
        private static string GetRandomString()
        {
            string path = Path.GetRandomFileName();
            path = path.Replace(".", ""); // Remove period.
            return path;
        }
        public string GetNodeName(Photo p)
        {
            return p.originDate.ToShortDateString() + ", " + p.originDate.ToString("hh:mm:ss tt");
        }

        public Photo FindPhoto(string id)
        {
            if (HidePics)
                return null;
            foreach (Photo p in Photos)
            {
                if (p.id.Equals(id))
                    return p;
            }
            return null;
        }

        public void Sort()
        {
            Photos.Sort((x, y) => DateTime.Compare(x.date, y.date));
        }
    }
}
