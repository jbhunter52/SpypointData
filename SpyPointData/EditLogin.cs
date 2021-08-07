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
    public partial class EditLogin : Form
    {
        public SpyPointData.DataCollection Data;
        public BindingList<LoginInfo> Logins;
        public EditLogin(SpyPointData.DataCollection data)
        {
            InitializeComponent();
            Data = data;
            Logins = new BindingList<LoginInfo>();

            foreach (var spc in Data.Connections)
            {
                spc.UserLogin.uuid = spc.uuid;
                var login = new LoginInfo(spc.UserLogin.Username, spc.UserLogin.Password);
                login.uuid = spc.uuid;

                Logins.Add(login);
            }

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.DataSource = Logins;
            foreach (DataGridViewColumn c in dataGridView1.Columns)
            {
                if (c.Name == "uuid")
                    c.ReadOnly = true;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {


            DialogResult = DialogResult.OK;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addLoginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var login = new LoginInfo("user", "pass");
            login.uuid = "";
            Logins.Add(login);
        }

        private void removeLastLoginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var logins = Logins.Where(l => l.uuid.Equals("")).ToList();

            foreach (var l in logins)
                Logins.Remove(l);
        }
    }
}
