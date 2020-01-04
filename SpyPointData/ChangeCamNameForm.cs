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
    public partial class ChangeCamNameForm : Form
    {
        public ChangeCamNameForm()
        {
            InitializeComponent();
        }

        public string CamName;
        private void buttonSave_Click(object sender, EventArgs e)
        {
            CamName = textBoxCameraName.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public void SetName(string s)
        {
            textBoxCameraName.Text = s;
        }

        private void ChangeCamNameForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                buttonSave.PerformClick();
            }
        }
    }
}
