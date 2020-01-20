using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using BrightIdeasSoftware;

namespace SpyPointData
{
    public partial class Form1 : Form
    {
        private DataCollection Data;
        private List<LoginInfo> UserLogins;
        private string file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SpyPoint" ,"Data.json");
        public Form1()
        {
            InitializeComponent();

            comboBoxChartType.DataSource = Enum.GetNames(typeof(HistogramType));
            comboBoxChartType.SelectedIndexChanged += comboBoxChartType_SelectedIndexChanged;

            UserLogins = new List<LoginInfo>();

            Data = new DataCollection();

            if (File.Exists(file))
            {
                Data.Load(file);
            }

            InitializeTree();
            
            gMapControl1.MapProvider = GMap.NET.MapProviders.GoogleHybridMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            gMapControl1.Position = new GMap.NET.PointLatLng(38.0323, -89.5657);
            gMapControl1.ShowCenter = false;

            //Update BuckID combobox
            comboBoxBuckIDs.Items.Add("");
            foreach (BuckID id in Data.BuckData.IDs)
            {
                comboBoxBuckIDs.Items.Add(id.Name);
            }
        }
        private void InitializeTree()
        {

            treeListView1.ChildrenGetter = delegate(object obj)
            {
                if (obj is DataCollection)
                {
                    List<object> objs = new List<object>();
                    objs.AddRange(((DataCollection)obj).Connections);
                    objs.Add(Data.ManualPics);
                    return objs;
                }
                else if (obj is SPConnection)
                {
                    return ((SPConnection)obj).CameraPictures;
                }
                else if (obj is ManualPics)
                {
                    return ((ManualPics)obj).Photos;
                }
                else if (obj is KeyValuePair<string, CameraPics>)
                {
                    return ((KeyValuePair<string, CameraPics>)obj).Value.photos;
                }
                else if (obj is Photo)
                {


                    return null;
                }
                else
                    return "";
            };
            treeListView1.CanExpandGetter = delegate(object obj)
            {
                if (obj is DataCollection)
                    return true;
                else if (obj is SPConnection)
                    return true;
                else if (obj is ManualPics)
                    return true;
                else if (obj is KeyValuePair<string, CameraPics>)
                    return true;
                else if (obj is Photo)
                    return false;
                else
                    return false;
            };

            treeListView1.AdditionalFilter = new ModelFilter(delegate(object o)
            {
                if (o is Photo)
                {
                    Photo p = (Photo)o;
                    return p.CheckFilter(Data.Filter);
                }
                else
                    return true;
            });

            OLVColumn nameCol = treeListView1.AllColumns.Find(c => c.Text.Equals("Name"));
            nameCol.AspectGetter = delegate(object obj)
            {
                if (obj is DataCollection)
                {
                    return "SpyPointData";
                }
                else if (obj is SPConnection)
                {
                    return ((SPConnection)obj).UserLogin.Username;
                }
                else if (obj is ManualPics)
                    return "ManualPics";
                else if (obj is KeyValuePair<string, CameraPics>)
                {
                    string camId = ((KeyValuePair<string, CameraPics>)obj).Value.cameraId;
                    foreach (SPConnection conn in Data.Connections)
                    {
                        if (conn.CameraInfoList.ContainsKey(camId))
                        {
                            CameraInfo ci = conn.CameraInfoList[camId];
                            return ci.config.name;
                        }
                    }
                    return "";
                }
                else if (obj is Photo)
                {
                    return ((Photo)obj).GetNodeName();
                }
                else
                    return "";
            };

            OLVColumn locCol = treeListView1.AllColumns.Find(c => c.Text.Equals("Location"));
            locCol.AspectGetter = delegate(object obj)
            {
                if (obj is Photo)
                {
                    return ((Photo)obj).CameraName;
                }
                if (obj is KeyValuePair<string, CameraPics>)
                {
                    CameraPics cp = ((KeyValuePair<string, CameraPics>)obj).Value;
                    return "";
                }
                else
                    return "";
            };

            OLVColumn gpsCol = treeListView1.AllColumns.Find(c => c.Text.Equals("GPS"));
            gpsCol.AspectGetter = delegate(object obj)
            {
                if (obj is KeyValuePair<string, CameraPics>)
                {
                    CameraPics cp = ((KeyValuePair<string, CameraPics>)obj).Value;
                    if (cp.HaveLocation)
                        return "x";
                    else
                        return "";
                }
                if (obj is Photo)
                {
                    Photo p = (Photo)obj;
                    if (p.HaveLocation)
                        return "x";
                    else
                        return "";
                }
                else
                    return "";
            };

            OLVColumn countCol = treeListView1.AllColumns.Find(c => c.Text.Equals("Count"));
            countCol.AspectGetter = delegate(object obj)
            {
                if (obj is DataCollection)
                {
                    return ((DataCollection)obj).GetFilteredPhotos().Count.ToString();
                }
                else if (obj is SPConnection)
                {
                    return ((SPConnection)obj).GetFilteredPhotos(Data.Filter).Count.ToString();
                }
                else if (obj is ManualPics)
                    return ((ManualPics)obj).GetFilteredPhotos(Data.Filter).Count.ToString();
                else if (obj is KeyValuePair<string, CameraPics>)
                {
                    return ((KeyValuePair<string, CameraPics>)obj).Value.GetFilteredPhotos(Data.Filter).Count.ToString();
                }
                else if (obj is Photo)
                {
                    return "";
                }
                else
                    return "";

            };
            OLVColumn deerCol = treeListView1.AllColumns.Find(c => c.Text.Equals("Deer"));
            deerCol.AspectGetter = delegate(object obj)
            {
                if (obj is Photo)
                {
                    if (((Photo)obj).Deer)
                        return "x";
                    else
                        return "";
                }
                else
                    return "";
            };
            

            OLVColumn buckCol = treeListView1.AllColumns.Find(c => c.Text.Equals("Buck"));
            buckCol.AspectGetter = delegate(object obj)
            {
                if (obj is Photo)
                {
                    if (((Photo)obj).Buck)
                        return "x";
                    else
                        return "";
                }
                else
                    return "";
            };
            OLVColumn ageCol = treeListView1.AllColumns.Find(c => c.Text.Equals("Age"));
            ageCol.AspectGetter = delegate(object obj)
            {
                if (obj is Photo)
                {
                    int age = ((Photo)obj).BuckAge;
                    if (age > 0)
                        return (age + 0.5).ToString();
                    else
                        return "";

                }
                else
                    return "";
            };
            OLVColumn idCol = treeListView1.AllColumns.Find(c => c.Text.Equals("ID"));
            idCol.AspectGetter = delegate(object obj)
            {
                if (obj is Photo)
                {
                    Photo p = (Photo)obj;
                    string name = Data.BuckData.CheckForPhoto(p.id);
                    return name;
                }
                else
                    return "";
            };
            
            treeListView1.AddObject(Data);
            RefreshTree();
        }
        private FilterCriteria GetFilterCriteria()
        {
            FilterCriteria fc = new FilterCriteria();
            fc.Deer = deerToolStripMenuItem.Checked;
            fc.Buck = bucksToolStripMenuItem.Checked;
            fc.Age0 = toolStripMenuItemAge0p5.Checked;
            fc.Age1 = toolStripMenuItemAge1p5.Checked;
            fc.Age2 = toolStripMenuItemAge2p5.Checked;
            fc.Age3 = toolStripMenuItemAge3p5.Checked;
            fc.Age4 = toolStripMenuItemAge4p5.Checked;
            return fc;
        }
        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Data = new DataCollection();

            foreach (var login in UserLogins)
            {
                SPConnection SP = Data.Add(login);
                SP.Login();
                SP.GetCameraInfo();
                SP.GetAllPicInfo();
                SP.DownloadPhotosFromAllCameras();
            }

            RefreshTree();

        }
        private void UpdateHistogram()
        {
            object o = treeListView1.SelectedObject;
            List<Photo> photos = new List<Photo>();
            if (o is DataCollection)
            {
                photos = ((DataCollection)o).GetFilteredPhotos();
            }
            else if (o is SPConnection)
            {
                photos = ((SPConnection)o).GetFilteredPhotos(Data.Filter);
            }
            else if (o is KeyValuePair<string, CameraPics>)
            {
                photos = ((KeyValuePair<string, CameraPics>)o).Value.GetFilteredPhotos(Data.Filter);
            }
            else if (o is Photo)
            {

            }

            HistogramType htype;
            Enum.TryParse<HistogramType>(comboBoxChartType.SelectedValue.ToString(), out htype);

            chartHistogram = Data.Histogram(photos, 24, chartHistogram, htype);
        }
        private BackgroundWorker bgMerge;
        private ProgressWindow pw;
        private void mergeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bgMerge = new BackgroundWorker();
            bgMerge.WorkerReportsProgress = true;
            bgMerge.DoWork += bgMerge_DoWork;
            bgMerge.ProgressChanged += bgMerge_ProgressChanged;
            pw = new ProgressWindow();
            pw.Show();
            pw.Start();
            pw.AddText("Merge started");
            bgMerge.RunWorkerCompleted += bgMerge_RunWorkerCompleted;
            
            bgMerge.RunWorkerAsync();
        }

        void Data_OnProgressUpdate(string s)
        {
            bgMerge.ReportProgress(0, s);
        }

        void bgMerge_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Data.OnProgressUpdate -= Data_OnProgressUpdate;
            
            //pw.Close();
            RefreshTree();
            Data.Save(file);
        }

        void bgMerge_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pw.AddText(Convert.ToString(e.UserState));
        }

        void bgMerge_DoWork(object sender, DoWorkEventArgs e)
        {
            DataCollection data = new DataCollection();
            data.OnProgressUpdate += Data_OnProgressUpdate;
            Data.OnProgressUpdate += Data_OnProgressUpdate;
            Data.RegisterEvents();

            foreach (var spc in Data.Connections)
            {
                SPConnection SP = data.Add(spc.UserLogin);
                SP.Login();
                SP.GetCameraInfo();
                SP.GetAllPicInfo();
                SPConnection oldSP = Data.Connections.Find(c => c.uuid.Equals(SP.uuid));

                if (oldSP != null) //Already exists
                    oldSP.Merge(SP);
                else //Doesn't exist yet
                {
                    SP.DownloadPhotosFromAllCameras();
                    Data.Connections.Add(SP);
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Data.Save(file);
            this.Close();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.N)
            {
                e.Handled = true;
                changeCameraNameToolStripMenuItem.PerformClick();   
            }
            if (e.KeyCode == Keys.B)
            {
                Photo p;
                if (treeListView1.SelectedObject is Photo)
                {
                    p = (Photo)treeListView1.SelectedObject;
                    p.Buck = !p.Buck;
                }
                else
                    return;
                treeListView1.RefreshObject(p);
                e.Handled = true;
            }
            if (e.KeyCode == Keys.D)
            {
                Photo p;
                if (treeListView1.SelectedObject is Photo)
                {
                    p = (Photo)treeListView1.SelectedObject;
                    p.Deer = !p.Deer;
                }
                else
                    return;
                treeListView1.RefreshObject(p);
                e.Handled = true;
            }
            if (e.KeyValue >= 48 && e.KeyValue <= 57)
            {
                int age = e.KeyValue - 48;;
                if (age <= 4)
                {
                    Photo p;
                    if (treeListView1.SelectedObject is Photo)
                    {
                        p = (Photo)treeListView1.SelectedObject;
                        p.BuckAge = age;
                    }
                    else
                        return;
                    p.BuckAge = age;
                    treeListView1.RefreshObject(p);
                }

                e.Handled = true;
            }
        }
        private void bucksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Data.Filter.Buck = bucksToolStripMenuItem.Checked;
            Data.Filter.Age0 = toolStripMenuItemAge0p5.Checked;
            Data.Filter.Age1 = toolStripMenuItemAge1p5.Checked;
            Data.Filter.Age2 = toolStripMenuItemAge2p5.Checked;
            Data.Filter.Age3 = toolStripMenuItemAge3p5.Checked;
            Data.Filter.Age4 = toolStripMenuItemAge4p5.Checked;
            treeListView1.UpdateColumnFiltering();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Data.Save(file);
        }

        private void deerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Data.Filter.Deer = deerToolStripMenuItem.Checked;
            treeListView1.UpdateColumnFiltering();
        }

        private void comboBoxChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateHistogram();
        }

        private void importCardPicsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Jpeg files (*.jpg)|*.jpg";
            ofd.Title = "Select photos";
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (string file in ofd.FileNames)
                {
                    Data.AddCardPic(file);
                }
            }
        }

        private void importManualPicsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Jpeg files (*.jpg)|*.jpg";
            ofd.Title = "Select photos";
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Data.ManualPics.AddPics(ofd.FileNames.ToList());
            }

            Data.Save(file);
            RefreshTree();
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            Photo p;
            if (treeListView1.SelectedObject is Photo)
                p = (Photo)treeListView1.SelectedObject;
            else
                return;

            string s = JsonConvert.SerializeObject(p, Formatting.Indented);

            InfoWindow iw = new InfoWindow();
            iw.SetTextData(s);
            iw.ShowDialog();

        }

        private void cameraIdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            locationToolStripMenuItem.Checked = !locationToolStripMenuItem.Checked;
        }

        private void locationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cameraIdToolStripMenuItem.Checked = !cameraIdToolStripMenuItem.Checked;
        }

        private void changeCameraNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Photo p;
            if (treeListView1.SelectedObject is Photo)
                p = (Photo)treeListView1.SelectedObject;
            else
                return;

            using (var changeNameForm = new InputForm("Change Camera Name"))
            {
                changeNameForm.SetInput(p.CameraName);
                var result = changeNameForm.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    p.CameraName = changeNameForm.Input;
                }
            }
            
            

        }

        private void setLocationCoordinatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeListView1.SelectedObjects.Count < 1)
                return;

            SetCoordinatesForm form = new SetCoordinatesForm();
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                double lat = form.Latitude;
                double lng = form.Longitude;

                foreach (object o in treeListView1.SelectedObjects)
                {
                    if (o is Photo)
                    {
                        Photo p = (Photo)o;
                        p.HaveLocation = true;
                        p.Latitude = lat;
                        p.Longitude = lng;
                    }
                    if (o is KeyValuePair<string, CameraPics>)
                    {
                        CameraPics cp = ((KeyValuePair<string, CameraPics>)o).Value;
                        cp.HaveLocation = true;
                        cp.Latitude = lat;
                        cp.Longitude = lng;
                    }
                }
            }
        }

        private void buckIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BuckIDForm buckIDform = new BuckIDForm(Data);

            if (buckIDform.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Data.BuckData = buckIDform.Data.BuckData;
                comboBoxBuckIDs.Items.Clear();
                comboBoxBuckIDs.Items.Add("");
                foreach (BuckID id in Data.BuckData.IDs)
                {
                    comboBoxBuckIDs.Items.Add(id.Name);
                }
            }
        }

        private void comboBoxBuckIDs_SelectedIndexChanged(object sender, EventArgs e)
        {
            Photo p;
            string idName = (string)comboBoxBuckIDs.SelectedItem;

            if (idName == null)
                return;

            if (treeListView1.SelectedObject is Photo)
            {
                p = (Photo)treeListView1.SelectedObject;
                Data.BuckData.AddConnection(idName, p);
            }
                
            else if (treeListView1.SelectedObject == null && treeListView1.SelectedObjects.Count > 0)
            {
                foreach (object o in treeListView1.SelectedObjects)
                {
                    if (o is Photo)
                    {
                        p = (Photo)o;
                        Data.BuckData.AddConnection(idName, p);
                    }
                }
            }
            else
                return;



            
        }

        private void weatherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WeatherTracker.WeatherTracker tracker = new WeatherTracker.WeatherTracker();
            tracker.ShowDialog();
        }

        private void treeListView1_SelectionChanged(object sender, EventArgs e)
        {
            Object obj = treeListView1.SelectedObject;
            if (SelectingMarker == false)
                gMapControl1.Overlays.Clear();

            if (obj is Photo)
            {
                Photo p;
                p = (Photo)treeListView1.SelectedObject;
                chartHistogram.Series.Clear();
                System.Drawing.Image image = Data.GetPhotoFromFile(p);
                imageBox1.Image = image;
                imageBox1.ZoomToFit();
                labelCamName.Text = p.CameraName;

                //Check if photo exists in BuckData
                string name = Data.BuckData.CheckForPhoto(p.id);
                if (name == null)
                    comboBoxBuckIDs.SelectedItem = null;
                else
                {
                    comboBoxBuckIDs.SelectedIndexChanged -= comboBoxBuckIDs_SelectedIndexChanged;
                    comboBoxBuckIDs.SelectedItem = name;
                    comboBoxBuckIDs.SelectedIndexChanged += comboBoxBuckIDs_SelectedIndexChanged;
                }
                if (SelectingMarker)
                {
                    SelectingMarker = false;
                    return;
                }

                if (p.HaveLocation && enableMapToolStripMenuItem.Checked)
                    UpdateMapMarkers(new List<object>() { p });
            }
            else if (treeListView1.SelectedObject == null)
            {
                imageBox1.Image = null;

                if (enableMapToolStripMenuItem.Checked)
                {
                    List<object> objs = new List<object>();
                    foreach (object o in treeListView1.SelectedObjects)
                        objs.Add(o);
                    UpdateMapMarkers(objs);
                }
            }
            else
            {
                imageBox1.Image = null;
                UpdateHistogram();
            }

            //gMapControl1.Invalidate();
        }
        public void UpdateMapMarkers(List<object> objs)
        {
            if (objs == null)
                return;
            GMap.NET.WindowsForms.GMapOverlay markers = new GMap.NET.WindowsForms.GMapOverlay("markers");
            foreach (object o in objs)
            {
                if (o is Photo)
                {
                    Photo p = (Photo)o;
                    if (p.HaveLocation)
                    {
                        GMap.NET.WindowsForms.GMapMarker marker =
                        new GMap.NET.WindowsForms.Markers.GMarkerGoogle(
                            new GMap.NET.PointLatLng(p.Latitude, p.Longitude),
                            GMap.NET.WindowsForms.Markers.GMarkerGoogleType.blue_pushpin);
                        marker.Tag = p.id;
                        markers.Markers.Add(marker);
                    }
                }
            }

            gMapControl1.Overlays.Add(markers);
            gMapControl1.ZoomAndCenterMarkers("markers");
            gMapControl1.Zoom = 16;
        }
        public void RefreshTree()
        {
            treeListView1.RefreshObject(Data);
            treeListView1.UpdateColumnFiltering();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Data.Save(file);
        }

        private void treeListView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Object o = treeListView1.SelectedObject;
            string json = JsonConvert.SerializeObject(o, Formatting.Indented);
            InfoWindow iw = new InfoWindow();
            iw.SetTextData(json);
            iw.Show();
        }

        private void enableMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!enableMapToolStripMenuItem.Checked)
            {
                gMapControl1.Overlays.Clear();
            }
        }

        bool SelectingMarker = false;
        private void gMapControl1_OnMarkerClick(GMap.NET.WindowsForms.GMapMarker marker, MouseEventArgs e)
        {
            string id = (string)marker.Tag;

            Photo p = Data.FindPhoto(id);

            SelectingMarker = true;
            treeListView1.SelectObject(p, true);
        }

    }
}
