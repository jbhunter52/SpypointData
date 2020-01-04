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
        public BuckData BuckData;
        public BuckIDForm()
        {
            InitializeComponent();
        }

        private void BuckIDForm_Load(object sender, EventArgs e)
        {
            if (BuckData == null)
                BuckData = new SpyPointData.BuckData();
            BuckData.Load();

            ReDraw();
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
                    BuckData.IDs.Add(new BuckID(addBuckName.BuckName));
                }
            }
            
            ReDraw();
        }

        private void ReDraw()
        {
            treeView1.Nodes.Clear();

            foreach (BuckID id in BuckData.IDs)
            {
                TreeNode node = new TreeNode(id.Name);

                foreach (Photo p in id.Photos)
                {
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
            BuckData.Save();
        }

    }
}
