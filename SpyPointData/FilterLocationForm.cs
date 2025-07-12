using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.WindowsForms;

using BrightIdeasSoftware;

namespace SpyPointData
{
    public partial class FilterLocationForm : Form
    {
        public List<string> Locations;
        public HashSet<PointLatLng> PicMarkersLocs;
        public GMapPolygon Rectangle;
        public List<PointLatLng> RectanglePoints;
        public Dictionary<string, SortedSet<string>> DictAll;
        public Dictionary<string, SortedSet<string>> DictFiltered;
        public string SearchString = "";
        public FilterLocationForm(DataCollection data)
        {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;
            gMapControl1.MapProvider = GMap.NET.MapProviders.GoogleHybridMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
            gMapControl1.Position = new GMap.NET.PointLatLng(38.0323, -89.5657);
            gMapControl1.ShowCenter = false;
            gMapControl1.Overlays.Clear();
            
            PicMarkersLocs = new HashSet<PointLatLng>();

            treeListView1.ChildrenGetter = delegate (object obj)
            {
                if (obj is KeyValuePair<string, SortedSet<string>>)
                {
                    var kvp = (KeyValuePair<string, SortedSet<string>>)obj;

                    return kvp.Value;
                }
                else if (obj is string)
                {
                    return null;
                }
                else
                    return "";
            };

            treeListView1.CanExpandGetter = delegate (object obj)
            {
                if (obj is KeyValuePair<string, SortedSet<string>>)
                    return true;
                else if (obj is string)
                    return false;
                else
                    return false;
            };


            treeListView1.AdditionalFilter = new ModelFilter(delegate (object o)
            {
                if (o is string)
                {
                    string s = (string)o;
                    if (s.ToLower().Contains(SearchString))
                        return true;
                    else
                        return false;
                }
                else
                    return true;
            });

            OLVColumn newCol = treeListView1.AllColumns.Find(c => c.Text.Equals("Name"));
            newCol.AspectGetter = delegate (object obj)
            {
                if (obj is KeyValuePair<string, SortedSet<string>>)
                {
                    var kvp = (KeyValuePair<string, SortedSet<string>>)obj;
                    return kvp.Key;
                }
                else if (obj is string)
                    return obj;
                else
                    return "";
            };

            DictAll = new Dictionary<string, SortedSet<string>>();
            foreach (var spc in data.Connections)
            {
                foreach (var kvp in spc.CameraPictures)
                {
                    var cp = kvp.Value.photos;
                    foreach (var p in cp)
                    {
                        if (!DictAll.ContainsKey(spc.UserLogin.Username))
                            DictAll.Add(spc.UserLogin.Username, new SortedSet<string>(StringComparer.OrdinalIgnoreCase));
                        else
                            DictAll[spc.UserLogin.Username].Add(p.GetSimpleCameraName());
                        PicMarkersLocs.Add(new PointLatLng(p.Latitude, p.Longitude));
                    }

                }
            }

            treeListView1.SetObjects(DictAll);



            //Create map markers
            GMap.NET.WindowsForms.GMapOverlay markers = new GMap.NET.WindowsForms.GMapOverlay("markers");
            foreach (var loc in PicMarkersLocs)
            {
                if (loc.Lat != 0 && loc.Lng != 0)
                {
                    GMap.NET.WindowsForms.GMapMarker marker =
                        new GMap.NET.WindowsForms.Markers.GMarkerGoogle(
                        new GMap.NET.PointLatLng(loc.Lat, loc.Lng),
                        GMap.NET.WindowsForms.Markers.GMarkerGoogleType.blue_pushpin);
                    marker.Tag = "";
                    markers.Markers.Add(marker);
                }
            }

            if (data.ManualPics.Photos.Count > 0 && data.ManualPics.HidePics == false)
            {
                foreach (var p in data.ManualPics.Photos)
                {
                    if (p.Latitude != 0 && p.Longitude != 0)
                    {
                        GMap.NET.WindowsForms.GMapMarker marker =
                            new GMap.NET.WindowsForms.Markers.GMarkerGoogle(
                            new GMap.NET.PointLatLng(p.Latitude, p.Longitude),
                            GMap.NET.WindowsForms.Markers.GMarkerGoogleType.blue_pushpin);
                        marker.Tag = "";
                        markers.Markers.Add(marker);
                    }
                }
            }

            gMapControl1.Overlays.Add(markers);
            gMapControl1.ZoomAndCenterMarkers("markers");
            gMapControl1.Zoom = 16;
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            
            Locations = new List<string>();
            if (treeListView1.SelectedObjects.Count > 0)
            {
                foreach (object obj in treeListView1.SelectedObjects)
                {
                    if (obj is string)
                        Locations.Add((string)obj);
                }
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public PointLatLng InitialRect;
        public PointLatLng FinalRect;

        private void gMapControl1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && ModifierKeys.HasFlag(Keys.Control))
            {
                double lat = gMapControl1.FromLocalToLatLng(e.X, e.Y).Lat;
                double lng = gMapControl1.FromLocalToLatLng(e.X, e.Y).Lng;
                InitialRect = new PointLatLng(lat, lng);
            }
        }

        private void gMapControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && ModifierKeys.HasFlag(Keys.Control) && InitialRect != null)
            {
                double lat = gMapControl1.FromLocalToLatLng(e.X, e.Y).Lat;
                double lng = gMapControl1.FromLocalToLatLng(e.X, e.Y).Lng;
                FinalRect = new PointLatLng(lat, lng);

                GMapOverlay polygons = new GMapOverlay("polygons");
                List<PointLatLng> points = new List<PointLatLng>();
                points.Add(new PointLatLng(InitialRect.Lat, InitialRect.Lng));
                points.Add(new PointLatLng(FinalRect.Lat, InitialRect.Lng));
                points.Add(new PointLatLng(FinalRect.Lat, FinalRect.Lng));
                points.Add(new PointLatLng(InitialRect.Lat, FinalRect.Lng));
                RectanglePoints = points;

                Rectangle = new GMapPolygon(points, "Rect");
                Rectangle.Fill = new SolidBrush(Color.FromArgb(50, Color.Red));
                Rectangle.Stroke = new Pen(Color.Red, 1);
                polygons.Polygons.Add(Rectangle);

                List<GMapOverlay> remove = new List<GMapOverlay>();
                foreach (var ol in gMapControl1.Overlays)
                {
                    if (ol.Polygons.Count > 0)
                        remove.Add(ol);
                }
                foreach (var ol in remove)
                {
                    gMapControl1.Overlays.Remove(ol);
                }

                gMapControl1.Overlays.Add(polygons);
                gMapControl1.Position = new PointLatLng(gMapControl1.Position.Lat+0.00000001, gMapControl1.Position.Lng);
            }
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            SearchString = textBoxSearch.Text.ToLower();
            treeListView1.UpdateColumnFiltering();
        }

        private void gMapControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //   Capture the Double Click event when 2 clicks occured
            if (e.Clicks.Equals(2))
            {
                PointLatLng pt = gMapControl1.FromLocalToLatLng(e.X, e.Y);

                gMapControl1.Position = pt;

                if(e.Button.Equals(MouseButtons.Left))
                {
                    // Zoom in with left mouse button
                    gMapControl1.Zoom += 1;
                }
                else if(e.Button.Equals(MouseButtons.Right))
                {
                    // Zoom out with right mouse button
                    gMapControl1.Zoom -= 1;
                }
            }
        }
    }

}
