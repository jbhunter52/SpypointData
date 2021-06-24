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
    public partial class CameraDetailsForm : Form
    {
        public DataCollection Data;
        public CameraDetailsForm(DataCollection data)
        {
            InitializeComponent();
            Data = data;

            dgv.CellClick += dgv_CellClick;

            DataTable dt = GetDataTable();
            dgv.DataSource = dt;
            dgv.AutoResizeColumns();
        }

        void dgv_CellClick(object sender, BrightIdeasSoftware.CellClickEventArgs e)
        {
            string s = e.Item.SubItems[e.ColumnIndex].Text;
            Clipboard.SetDataObject(s);
        }

        public DataTable GetDataTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("User", typeof(string)));
            dt.Columns.Add(new DataColumn("ID", typeof(string)));
            dt.Columns.Add(new DataColumn("Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Model", typeof(string)));
            dt.Columns.Add(new DataColumn("Version", typeof(string)));
            dt.Columns.Add(new DataColumn("LastUpdate", typeof(string)));
            dt.Columns.Add(new DataColumn("Type", typeof(string)));
            dt.Columns.Add(new DataColumn("sim", typeof(string)));
            dt.Columns.Add(new DataColumn("Battery", typeof(int)));
            dt.Columns.Add(new DataColumn("Signal", typeof(int)));
            dt.Columns.Add(new DataColumn("PlanName", typeof(string)));
            dt.Columns.Add(new DataColumn("MaxThisMonth", typeof(int)));
            dt.Columns.Add(new DataColumn("ThisMonth", typeof(int)));
            dt.Columns.Add(new DataColumn("Left", typeof(int)));
            dt.Columns.Add(new DataColumn("Start", typeof(string)));
            dt.Columns.Add(new DataColumn("End", typeof(string)));
            dt.Columns.Add(new DataColumn("DaysLeft", typeof(int)));
            dt.Columns.Add(new DataColumn("AutoRenew", typeof(string)));
            dt.Columns.Add(new DataColumn("Multishot", typeof(int)));
            dt.Columns.Add(new DataColumn("Delay", typeof(string)));

            foreach (var conn in Data.Connections)
            {
                string login = conn.UserLogin.Username;
                foreach (var kvp in conn.CameraInfoList)
                {
                    CameraInfo ci = kvp.Value;

                    DataRow dr = dt.NewRow();
                    dr["User"] = login;
                    dr["ID"] = ci.id;
                    dr["Name"] = ci.config.name;
                    dr["Model"] = ci.status.model;
                    dr["Version"] = ci.status.version;
                    dr["LastUpdate"] = ci.status.lastUpdate.ToShortDateString();
                    dr["Type"] = ci.status.signal.type;
                    dr["sim"] = ci.status.sim;
                    dr["Battery"] = ci.status.batteries[0];
                    dr["Signal"] = ci.status.signal.dBm;
                    dr["PlanName"] = ci.subscriptions[0].plan.name;
                    dr["MaxThisMonth"] = ci.subscriptions[0].plan.photoCountPerMonth;
                    dr["ThisMonth"] = ci.subscriptions[0].photoCount;
                    dr["Left"] = ci.subscriptions[0].plan.photoCountPerMonth - ci.subscriptions[0].photoCount;
                    dr["Start"] = ci.subscriptions[0].startDateBillingCycle;
                    dr["End"] = ci.subscriptions[0].endDateBillingCycle;
                    dr["DaysLeft"] = ci.subscriptions[0].endDateBillingCycle.Subtract(DateTime.Now).Days;
                    dr["AutoRenew"] = ci.subscriptions[0].isAutoRenew;
                    dr["Multishot"] = ci.config.multiShot;
                    dr["Delay"] = ci.config.delay;
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
    }
}
