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
    public partial class AddLogin : Form
    {
        public AddLogin()
        {
            InitializeComponent();
        }

        public LoginInfo Login;
        private void buttonApply_Click(object sender, EventArgs e)
        {
            if (textBoxUserName.Text.Length > 0 && textBoxPassword.Text.Length > 0)
            {
                LoginInfo li = new LoginInfo(textBoxUserName.Text, textBoxPassword.Text);
                Login = li;

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                this.Close();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {

            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
