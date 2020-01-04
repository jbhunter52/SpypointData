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
    public partial class AddBuckNameForm : Form
    {
        public AddBuckNameForm()
        {
            InitializeComponent();
        }

        public string BuckName;
        private void buttonSaveName_Click(object sender, EventArgs e)
        {
            BuckName = textBoxBuckName.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public void SetName(string s)
        {
            textBoxBuckName.Text = s;
        }

        private void AddBuckNameForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                buttonSaveName.PerformClick();
            }
        }
    }
}
