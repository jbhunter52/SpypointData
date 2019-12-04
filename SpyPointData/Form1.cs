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
        public SPConnection SP;
        private ProPictureBox PPP;

        public Form1()
        {
            InitializeComponent();

            comboBoxChartType.DataSource = Enum.GetNames(typeof(HistogramType));
            comboBoxChartType.SelectedIndexChanged += comboBoxChartType_SelectedIndexChanged;

            treeView1.SelectedNode = null;

            SP = new SPConnection();
            if (File.Exists(SP.DataFile))
            {
                SP.Load();
                treeView1.Nodes.Add(SP.GetNodes(deerToolStripMenuItem.Checked, bucksToolStripMenuItem.Checked));
                
            }

            

            PPP = new ProPictureBox();
            splitContainer2.Panel2.Controls.Add(PPP);
            PPP.Dock = DockStyle.Fill;

            Bitmap bmp = new Bitmap(PPP.Width, PPP.Height);
            using (Graphics graph = Graphics.FromImage(bmp))
            {
                Rectangle ImageSize = new Rectangle(0,0,bmp.Width, bmp.Height);
                graph.FillRectangle(Brushes.White, ImageSize);
            }
            PPP.Image = bmp;

            treeView1.SelectedNode = null;
            treeView1.AfterSelect += treeView1_AfterSelect;
        }


        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SP.CameraInfoList.Clear();
            SP.CameraPictures.Clear();
            SP.Login();
            SP.GetCameraInfo();
            SP.GetAllPicInfo();
            SP.DownloadPhotosFromAllCameras();

            treeView1.Nodes.Clear();
            treeView1.Nodes.Add(SP.GetNodes(deerToolStripMenuItem.Checked, bucksToolStripMenuItem.Checked));

            SP.Save();

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Photo p;
            string tag = (string)e.Node.Tag;

            p = SP.FindPhoto(tag);

            if (p != null)
            {
                chartHistogram.Series.Clear();
                System.Drawing.Image image = SP.GetPhotoFromFile(p);
                PPP.Image = image;
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

            chartHistogram = SP.Histogram(nodes.ToArray(), 24, chartHistogram, htype);
        }
        private void mergeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SPConnection sp = new SPConnection(); 
            sp.Login();
            sp.GetCameraInfo();
            sp.GetAllPicInfo();

            SP.Merge(sp);

            treeView1.Nodes.Clear();
            treeView1.Nodes.Add(SP.GetNodes(deerToolStripMenuItem.Checked, bucksToolStripMenuItem.Checked));

            SP.Save();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SP.Save();
            this.Close();
        }

        private void checkBoxBuck_CheckedChanged(object sender, EventArgs e)
        {
            Photo p;
            string tag = (string)treeView1.SelectedNode.Tag;
            
            p = SP.FindPhoto(tag);

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
                Photo p = SP.FindPhoto(tag);
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
                Photo p = SP.FindPhoto(tag);
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
            SP.Save();
        }

        private void checkBoxDeer_CheckedChanged(object sender, EventArgs e)
        {
            Photo p;
            string tag = (string)treeView1.SelectedNode.Tag;

            p = SP.FindPhoto(tag);
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
            treeView1.Nodes.Clear();
            TreeNode node = SP.GetNodes(deerToolStripMenuItem.Checked, bucksToolStripMenuItem.Checked);
            treeView1.Nodes.Add(node);
            treeView1.SelectedNode = treeView1.Nodes[0];          
        }


        private void comboBoxChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateHistogram();
        }
    }
}
