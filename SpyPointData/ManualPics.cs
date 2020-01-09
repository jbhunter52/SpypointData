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
    public class ManualPics
    {
        public List<Photo> Photos;

        public ManualPics()
        {
            Photos = new List<Photo>();
        }

        public void AddPics(List<string> files)
        {
            string dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"ManualPics");
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            foreach (string file in files)
            {
                using (ExifReader reader = new ExifReader(file))
                {
                    DateTime date;
                    if (reader.GetTagValue<DateTime>(ExifTags.DateTimeOriginal, out date))
                    {
                        string newName = Path.Combine(dir,Path.GetFileName(file));
                        if (!File.Exists(newName))
                        {
                            File.Copy(file, newName);

                            Photo p = new Photo();
                            p.CameraName = "Manual";
                            p.CardPicFilename = newName;
                            p.date = date;
                            p.HaveCardPic = true;
                            p.originDate = date;
                            p.id = GetRandomString() + GetRandomString();
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
    }
}
