using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace SpyPointData
{
    public partial class BuckIDForm : Form
    {
        public DataCollection Data;
        public ContextMenuStrip NodeRightClickContext;
        public ContextMenuStrip TreeViewRightClickContext;
        public WeatherTracker.DarkSkyData DS;
        public Bitmap Arrow;
        public BuckIDForm(DataCollection data)
        {
            InitializeComponent();
            Data = data;

            DS = new WeatherTracker.DarkSkyData();
            DS.Load();
            DS.UpdateCurves();            

            gMapControl1.MapProvider = GMap.NET.MapProviders.GoogleHybridMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            gMapControl1.Position = new GMap.NET.PointLatLng(38.0323, -89.5657);
            gMapControl1.ShowCenter = false;

            ReDraw();

            treeView1.SelectedNode = null;
            treeView1.AfterSelect += treeView1_AfterSelect;

            NodeRightClickContext = new ContextMenuStrip();
            NodeRightClickContext.Items.Add(new ToolStripMenuItem("Rename"));
            NodeRightClickContext.Items.Add(new ToolStripMenuItem("Delete"));
            NodeRightClickContext.ItemClicked += NodeRightClickContext_ItemClicked;

            TreeViewRightClickContext = new ContextMenuStrip();
            TreeViewRightClickContext.Items.Add(new ToolStripMenuItem("Add"));
            TreeViewRightClickContext.ItemClicked += NodeRightClickContext_ItemClicked;
        }
        private TreeNode rightClickedNode;
        void NodeRightClickContext_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripItem item = e.ClickedItem;
            if (item.Text.Equals("Rename"))
            {
                using (var inputForm = new InputForm("Change Buck Name"))
                {
                    //Get BuckID
                    BuckID buckID = Data.BuckData.IDs.Find(b => b.Name.Equals(rightClickedNode.Text));
                    inputForm.SetInput(buckID.Name);
                    var result = inputForm.ShowDialog();
                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        buckID.Name = inputForm.Input;
                    }
                }
                ReDraw();
            }
            if (item.Text.Equals("Delete"))
            {
                Data.BuckData.IDs.RemoveAll(b => b.Name.Equals(rightClickedNode.Text));
                ReDraw();
            }
            if (item.Text.Equals("Add"))
            {
                using (var inputForm = new InputForm("Add Buck Name"))
                {
                    var result = inputForm.ShowDialog();
                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        Data.BuckData.IDs.Add(new BuckID(inputForm.Input));
                    }
                }

                ReDraw();
            }
        }


        private string GetNodeName(Photo p)
        {
            return p.originDate.ToShortDateString() + ", " + p.originDate.ToString("hh:mm:ss tt");
        }

        private void addNewBuckToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ReDraw()
        {
            treeView1.Nodes.Clear();

            foreach (BuckID id in Data.BuckData.IDs)
            {
                TreeNode node = new TreeNode(id.Name);

                foreach (BuckIDPhoto buckIDPhoto in id.Photos)
                {
                    Photo p = Data.FindPhoto(buckIDPhoto.PhotoID);
                    TreeNode photoNode = new TreeNode(GetNodeName(p));
                    photoNode.Tag = p.id;
                    node.Nodes.Add(photoNode);
                }
                treeView1.Nodes.Add(node);
            }
        }

        private void BuckIDForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Photo p;
            if (e.Node.Parent == null) //Buck Name is selected
            {
                BuckID buckID = Data.BuckData.IDs.Find(b => b.Name.Equals(e.Node.Text));
                gMapControl1.Overlays.Clear();

                GMap.NET.WindowsForms.GMapOverlay markers = new GMap.NET.WindowsForms.GMapOverlay("markers");
                foreach (BuckIDPhoto buckIDPhoto in buckID.Photos)
                {
                    p = Data.FindPhoto(buckIDPhoto.PhotoID);
                    if (p.HaveLocation)
                    {
                        
                        GMap.NET.WindowsForms.GMapMarker marker =
                            new GMap.NET.WindowsForms.Markers.GMarkerGoogle(
                                new GMap.NET.PointLatLng(p.Latitude, p.Longitude),
                                GMap.NET.WindowsForms.Markers.GMarkerGoogleType.blue_pushpin);
                        markers.Markers.Add(marker);
                        
                    }
                    
                }
                gMapControl1.Overlays.Add(markers);
                gMapControl1.ZoomAndCenterMarkers("markers");
                gMapControl1.Zoom = 16;
                return;
            }


            //Must be a photo selected

            string tag = (string)e.Node.Tag;
            p = Data.FindPhoto(tag);

            if (p == null)
            {
                imageBox1.Image = null;
                return;
            }

            System.Drawing.Image image = Data.GetPhotoFromFile(p);
            imageBox1.Image = image;
            imageBox1.ZoomToFit();

            //set map to location
            if (p.HaveLocation)
            {
                gMapControl1.Overlays.Clear();
                GMap.NET.WindowsForms.GMapOverlay markers = new GMap.NET.WindowsForms.GMapOverlay("markers");
                GMap.NET.WindowsForms.GMapMarker marker =
                    new GMap.NET.WindowsForms.Markers.GMarkerGoogle(
                        new GMap.NET.PointLatLng(p.Latitude, p.Longitude),
                        GMap.NET.WindowsForms.Markers.GMarkerGoogleType.blue_pushpin);
                markers.Markers.Add(marker);
                gMapControl1.Overlays.Add(markers);

                gMapControl1.Position = new GMap.NET.PointLatLng(p.Latitude, p.Longitude);
                gMapControl1.ZoomAndCenterMarkers("markers");
                gMapControl1.Zoom = 16;
            }

            //Update trackbar
            DateTime end = p.originDate;
            int hours = 24;
            DateTime start = end.Subtract(new TimeSpan(hours, 0, 0));

            trackBar1.Minimum = 0;
            trackBar1.Maximum = 100;
            trackBar1.Value = 100;

            SetWeatherData(end);
        }

        private void treeListView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                TreeViewHitTestInfo info = treeView1.HitTest(new Point(e.X, e.Y));

                if (info.Node == null) //Not a node click
                {
                    TreeViewRightClickContext.Show(this, new Point(e.X, e.Y), ToolStripDropDownDirection.BelowRight);
                }
                else
                {
                    if (info.Node.Parent == null) //Only show if it is on a BuckName
                    {
                        rightClickedNode = info.Node;
                        NodeRightClickContext.Show(this, new Point(e.X, e.Y), ToolStripDropDownDirection.BelowRight);
                    }
                }
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            string tag = (string)treeView1.SelectedNode.Tag;
            Photo p = Data.FindPhoto(tag);

            if (p == null)
            {
                return;
            }

            DateTime end = p.originDate;
            int hours = 24;
            DateTime start = end.Subtract(new TimeSpan(24, 0, 0));

            double hoursAdded = ((double)trackBar1.Value / 100) * hours;
            DateTime selected = start.AddHours(hoursAdded);

            SetWeatherData(selected);
        }
        private void SetWeatherData(DateTime selected)
        {
            textBoxTime.Text = selected.ToShortDateString() + ", " + selected.ToString("hh:mm:ss tt");

            //Now update info
            textBoxSpeed.Text = DS.InterpolateSpeed(selected).ToString();
            textBoxBearing.Text = DS.InterpolateBearing(selected);

            double rotateAngle = DS.InterpolateBearingDegrees(selected);

            Assembly myAssembly = Assembly.GetExecutingAssembly();
            Stream myStream = myAssembly.GetManifestResourceStream("SpyPointData.arrow.jpg");
            Arrow = new Bitmap(myStream);

            Image img = pictureBoxArrow.Image;
            if (img != null)
                img.Dispose();

            pictureBoxArrow.Image = RotateImage(Arrow, (float)rotateAngle);
            if (Arrow != null)
                Arrow.Dispose();
        }

        public static Image RotateImage(Image image, float angle)
        {

            if (image == null)
                throw new ArgumentNullException("image");

            //create a new empty bitmap to hold rotated image
            Bitmap rotatedBmp = new Bitmap(image.Width, image.Height);
            rotatedBmp.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            //make a graphics object from the empty bitmap
            Graphics g = Graphics.FromImage(rotatedBmp);

            //Put the rotation point in the center of the image
            g.TranslateTransform(image.Width/2, image.Height/2);

            //rotate the image
            g.RotateTransform(angle);

            //move the image back
            g.TranslateTransform(-image.Width / 2, -image.Height / 2);

            //draw passed in image onto graphics object
            g.DrawImage(image, new PointF(0, 0));

            return rotatedBmp;
        }
    }
}
