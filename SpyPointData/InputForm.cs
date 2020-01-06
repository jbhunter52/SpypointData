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
    public partial class InputForm : Form
    {
        public InputForm(string formName)
        {
            InitializeComponent();
            this.Text = formName;
        }

        public string Input;
        private void buttonSaveName_Click(object sender, EventArgs e)
        {
            Input = textBoxInput.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public void SetInput(string s)
        {
            textBoxInput.Text = s;
        }

        private void InputForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                buttonSaveName.PerformClick();
            }
        }
    }
}
