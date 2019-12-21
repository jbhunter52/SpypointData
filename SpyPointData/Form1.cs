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

        public Form1()
        {
            InitializeComponent();

            comboBoxChartType.DataSource = Enum.GetNames(typeof(HistogramType));
            comboBoxChartType.SelectedIndexChanged += comboBoxChartType_SelectedIndexChanged;

            treeView1.SelectedNode = null;

            UserLogins = new List<LoginInfo>();
            UserLogins.Add(new LoginInfo("e.beatty27@icloud.com", "sxpvyt"));
            UserLogins.Add(new LoginInfo("jbhunter52@yahoo.com", "fjkn3u"));

            Data = new DataCollection();

            if (File.Exists(file))
            {
                Data.Load(file);
                SetNodes();
              
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
            TreeNode[] nodes = Data.GetNodes(deerToolStripMenuItem.Checked, bucksToolStripMenuItem.Checked);
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
        private void mergeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataCollection data = new DataCollection();

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

            SetNodes();
            Data.Save(file);
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
            treeView1.SelectedNode = treeView1.Nodes[0];          
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
    }
}
