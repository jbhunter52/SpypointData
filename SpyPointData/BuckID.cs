using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpyPointData
{
    public partial class BuckIDForm : Form
    {
        public DataCollection Data;
        public BuckIDForm(DataCollection data)
        {
            InitializeComponent();
            Data = data;

            ReDraw();

            treeView1.SelectedNode = null;
            treeView1.AfterSelect += treeView1_AfterSelect;
        }


        private string GetNodeName(Photo p)
        {
            return p.originDate.ToShortDateString() + ", " + p.originDate.ToString("hh:mm:ss tt");
        }

        private void addNewBuckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var addBuckName = new AddBuckNameForm())
            {
                var result = addBuckName.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    Data.BuckData.IDs.Add(new BuckID(addBuckName.BuckName));
                }
            }
            
            ReDraw();
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
        }

    }
}
