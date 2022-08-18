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
    public partial class FilterBuckIDForm : Form
    {
        public List<string> SelectedIDs;
        public FilterBuckIDForm(List<string> buckids)
        {
            InitializeComponent();
            SelectedIDs = new List<string>();
            listBox1.SelectionMode = SelectionMode.MultiExtended;
            listBox1.Items.Clear();
            foreach (var s in buckids)
                listBox1.Items.Add(s);
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            List<string> selected = new List<string>();
            foreach (var s in listBox1.SelectedItems)
                selected.Add((string)s);

            SelectedIDs = selected;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
