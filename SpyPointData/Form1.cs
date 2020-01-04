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

namespace SpyPointData
{
    public partial class Form1 : Form
    {
        //private ProPictureBox PPP;
        private DataCollection Data;
        private List<LoginInfo> UserLogins;
        private string file = @"C:\Users\Jared\AppData\Local\SpyPoint\Data.json";
        private BuckData BuckData;
        public Form1()
        {
            InitializeComponent();

            comboBoxChartType.DataSource = Enum.GetNames(typeof(HistogramType));
            comboBoxChartType.SelectedIndexChanged += comboBoxChartType_SelectedIndexChanged;

            treeView1.SelectedNode = null;

            UserLogins = new List<LoginInfo>();
            UserLogins.Add(new LoginInfo("e.beatty27@icloud.com", "sxpvyt"));
            UserLogins.Add(new LoginInfo("jbhunter52@yahoo.com", "fjkn3u"));

            BuckData = new SpyPointData.BuckData();
            BuckData.Load();
            foreach (BuckID id in BuckData.IDs)
            {
                comboBoxBuckIDs.Items.Add(id.Name);
            }

            Data = new DataCollection();

            if (File.Exists(file))
            {
                Data.Load(file);
                Redraw();
            }

            //PPP = new ProPictureBox();
            //splitContainer2.Panel2.Controls.Add(PPP);
            //PPP.Dock = DockStyle.Fill;

            //Bitmap bmp = new Bitmap(imageBox1.Width, imageBox1.Height);
            //using (Graphics graph = Graphics.FromImage(bmp))
            //{
            //    Rectangle ImageSize = new Rectangle(0,0,bmp.Width, bmp.Height);
            //    graph.FillRectangle(Brushes.White, ImageSize);
            //}
            //imageBox1.Image = bmp;

            treeView1.SelectedNode = null;
            treeView1.AfterSelect += treeView1_AfterSelect;
        }

        private void SetNodes()
        {
            treeView1.Nodes.Clear();
            TreeNode main = new TreeNode("SpyPointData");

            OrganizeMethod method = OrganizeMethod.Camera;
            if (cameraIdToolStripMenuItem.Checked)
                method = OrganizeMethod.Camera;
            else if (locationToolStripMenuItem.Checked)
                method = OrganizeMethod.Location;


            TreeNode[] nodes = Data.GetNodes(GetFilterCriteria(), method);
            main.Nodes.AddRange(nodes);
            int cnt = 0;
            foreach (TreeNode user in main.Nodes)
            {
                foreach (TreeNode cam in user.Nodes)
                {
                    cnt += cam.Nodes.Count;
                }
                
            }
            main.Text = "SpyPointData, " + cnt.ToString();
            main.Name = "SpyPointData";
            treeView1.Nodes.Add(main);
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

            SetNodes();           

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Photo p;
            string tag = (string)e.Node.Tag;

            p = Data.FindPhoto(tag);

            if (p != null)
            {
                chartHistogram.Series.Clear();
                System.Drawing.Image image = Data.GetPhotoFromFile(p);
                imageBox1.Image = image;
                imageBox1.ZoomToFit();
                labelCamName.Text = p.CameraName;
                if (p.Buck != null)
                    checkBoxBuck.Checked = p.Buck;
                else
                    checkBoxBuck.Checked = false;
                if (p.Deer != null)
                    checkBoxDeer.Checked = p.Deer;
                else
                    checkBoxDeer.Checked = false;

                if (p.BuckAge == null)
                    textBoxBuckAge.Text = "";
                else
                {
                    textBoxBuckAge.Text = (p.BuckAge + 0.5).ToString();
                }

            }
            else
            {
                UpdateHistogram();
            }

            

        }
        private void UpdateHistogram()
        {
            TreeNode node = treeView1.SelectedNode;
            List<TreeNode> nodes = new List<TreeNode>();
            if (node.Parent == null) //if null a user is selected
            {
                foreach (TreeNode user in node.Nodes)
                {
                    foreach (TreeNode n in user.Nodes)
                    {
                        TreeNode[] pics = new TreeNode[n.Nodes.Count];
                        n.Nodes.CopyTo(pics, 0);
                        nodes.AddRange(pics);
                    }
                }
            }
            else if (node.Parent.Name.Equals("SpyPointData")) //if "main" a user is selected
            {
                foreach (TreeNode n in node.Nodes)
                {
                    TreeNode[] pics = new TreeNode[n.Nodes.Count];
                    n.Nodes.CopyTo(pics, 0);
                    nodes.AddRange(pics);
                }
            }
            else //Must be a camera picked
            {
                TreeNode[] pics = new TreeNode[node.Nodes.Count];
                node.Nodes.CopyTo(pics, 0);
                nodes.AddRange(pics);
            }



            HistogramType htype;
            Enum.TryParse<HistogramType>(comboBoxChartType.SelectedValue.ToString(), out htype);

            chartHistogram = Data.Histogram(nodes.ToArray(), 24, chartHistogram, htype);
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
            SetNodes();
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

            foreach (var login in UserLogins)
            {
                SPConnection SP = data.Add(login);
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

        private void checkBoxBuck_CheckedChanged(object sender, EventArgs e)
        {
            Photo p;
            string tag = (string)treeView1.SelectedNode.Tag;
            
            p = Data.FindPhoto(tag);

            if (p == null)
            {
                return;
            }

            if (p != null)
            {
                p.Buck = checkBoxBuck.Checked;
            }
            if (p.Buck)
                treeView1.SelectedNode.ForeColor = Color.DarkGreen;
            else
                treeView1.SelectedNode.ForeColor = Color.Black;
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('b'))
            {
                TreeNode n = treeView1.SelectedNode;
                string tag = (string)treeView1.SelectedNode.Tag;
                Photo p = Data.FindPhoto(tag);
                if (p != null)
                {
                    checkBoxBuck.Checked = !checkBoxBuck.Checked;
                }
                e.Handled = true;
            }
            if (e.KeyChar.Equals('d'))
            {
                TreeNode n = treeView1.SelectedNode;
                string tag = (string)treeView1.SelectedNode.Tag;
                Photo p = Data.FindPhoto(tag);
                if (p != null)
                {
                    checkBoxDeer.Checked = !checkBoxDeer.Checked;
                }
                e.Handled = true;
            }
            int age;
            if (int.TryParse(e.KeyChar.ToString(), out age))
            {
                if (age <= 4)
                {
                    TreeNode n = treeView1.SelectedNode;
                    string tag = (string)treeView1.SelectedNode.Tag;
                    Photo p = Data.FindPhoto(tag);
                    if (p != null)
                    {
                        p.BuckAge = age;
                        textBoxBuckAge.Text = (age+0.5).ToString();
                    }
                    
                }
                e.Handled = true;
            }

        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.N)
            {
                e.Handled = true;
                changeCameraNameToolStripMenuItem.PerformClick();
                
            }
        }
        private void bucksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Redraw();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Data.Save(file);
        }

        private void checkBoxDeer_CheckedChanged(object sender, EventArgs e)
        {
            Photo p;
            string tag = (string)treeView1.SelectedNode.Tag;

            p = Data.FindPhoto(tag);
            if (p == null)
            {
                return;
            }

            if (p != null)
            {
                p.Deer = checkBoxDeer.Checked;
            }
            if (p.Deer)
                treeView1.SelectedNode.BackColor = Color.LightGray;
            else
                treeView1.SelectedNode.BackColor = Color.White;
        }

        private void deerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Redraw();
        }
        private void Redraw()
        {
            SetNodes();
            foreach (TreeNode user in treeView1.Nodes[0].Nodes)
            {
                user.Expand();
                
            }
            treeView1.SelectedNode = treeView1.Nodes[0];
            treeView1.Nodes[0].Expand();
            
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

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            string tag = (string)treeView1.SelectedNode.Tag;

            Photo p = Data.FindPhoto(tag);

            if (p != null)
            {
                string s = JsonConvert.SerializeObject(p, Formatting.Indented);

                InfoWindow iw = new InfoWindow();
                iw.SetTextData(s);
                iw.ShowDialog();
            }

        }

        private void cameraIdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            locationToolStripMenuItem.Checked = !locationToolStripMenuItem.Checked;
            Redraw();
        }

        private void locationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cameraIdToolStripMenuItem.Checked = !cameraIdToolStripMenuItem.Checked;
            Redraw();
        }

        private void changeCameraNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Photo p;

            string tag = (string)treeView1.SelectedNode.Tag;

            p = Data.FindPhoto(tag);

            if (p == null)
                return;

            using (var changeNameForm = new ChangeCamNameForm())
            {
                changeNameForm.SetName(p.CameraName);
                var result = changeNameForm.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    p.CameraName = changeNameForm.CamName;
                }
            }
            
            

        }

        private void setLocationCoordinatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cameraIdToolStripMenuItem.Checked)
                return;
            TreeNode node = treeView1.SelectedNode;
            if (!node.Parent.Text.Contains("@"))
                return;

            SetCoordinatesForm form = new SetCoordinatesForm();
                
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                double lat = form.Latitude;
                double lng = form.Longitude;

                foreach (TreeNode picNode in node.Nodes)
                {
                    Photo p;
                    p = Data.FindPhoto((string)picNode.Tag);

                    if (p != null)
                    {
                        p.HaveLocation = true;
                        p.Latitude = lat;
                        p.Longitude = lng;
                    }
                }
            }
        }

        private void buckIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BuckIDForm buckIDform = new BuckIDForm();
            BuckData.Save();

            if (buckIDform.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                BuckData bd = buckIDform.BuckData;
                comboBoxBuckIDs.Items.Clear();
                foreach (BuckID id in bd.IDs)
                {
                    comboBoxBuckIDs.Items.Add(id.Name);
                }
            }
        }

        private void comboBoxBuckIDs_SelectedIndexChanged(object sender, EventArgs e)
        {
            string idName = (string)comboBoxBuckIDs.SelectedItem;

            Photo p;

            
        }


    }
}
