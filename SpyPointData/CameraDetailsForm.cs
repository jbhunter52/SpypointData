﻿using System;
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
            //dt.Columns.Add(new DataColumn("sim", typeof(string)));
            dt.Columns.Add(new DataColumn("lastPicDays", typeof(string)));
            dt.Columns.Add(new DataColumn("Battery", typeof(string)));
            dt.Columns.Add(new DataColumn("Signal", typeof(string)));
            dt.Columns.Add(new DataColumn("PlanName", typeof(string)));
            dt.Columns.Add(new DataColumn("MaxThisMonth", typeof(int)));
            dt.Columns.Add(new DataColumn("ThisMonth", typeof(int)));
            dt.Columns.Add(new DataColumn("Left", typeof(int)));
            //dt.Columns.Add(new DataColumn("Start", typeof(string)));
            //dt.Columns.Add(new DataColumn("End", typeof(string)));
            dt.Columns.Add(new DataColumn("DaysLeft", typeof(int)));
            dt.Columns.Add(new DataColumn("AutoRenew", typeof(string)));
            dt.Columns.Add(new DataColumn("Multishot", typeof(int)));
            dt.Columns.Add(new DataColumn("Delay", typeof(string)));
            dt.Columns.Add(new DataColumn("MotionDelay", typeof(string)));
            dt.Columns.Add(new DataColumn("Location", typeof(string)));

            foreach (var conn in Data.Connections)
            {
                string login = conn.UserLogin.Username;
                foreach (var kvp in conn.CameraInfoList)
                {
                    CameraInfo ci = kvp.Value;

                    if (ci.ManualDisable)
                        continue;

                    string lastPicDays = "-";
                    if (ci.lastPhotoDate > new DateTime(1990, 1, 1))
                        lastPicDays = ((int)DateTime.Now.Subtract(ci.lastPhotoDate).TotalDays).ToString();

                    DataRow dr = dt.NewRow();
                    dr["User"] = login;
                    //dr["ID"] = ci.id;
                    dr["Name"] = ci.config.name;
                    dr["Model"] = ci.status.model;
                    dr["Version"] =  ci.status.version ??  ""; //possible null
                    dr["LastUpdate"] = ci.status.lastUpdate.ToShortDateString();
                    dr["Type"] = (ci.status.signal == null) ? "": ci.status.signal.type; //possible null
                    //dr["sim"] = ci.status.sim ?? ""; //possible null
                    dr["lastPicDays"] = lastPicDays;
                    dr["Battery"] = (ci.status.batteries == null) ? "": ci.status.batteries[0].ToString(); //possible null
                    dr["Signal"] = (ci.status.signal == null) ? "": ci.status.signal.dBm.ToString(); //possible null
                    dr["PlanName"] = ci.subscriptions[0].plan.name;
                    dr["MaxThisMonth"] = ci.subscriptions[0].plan.photoCountPerMonth;
                    dr["ThisMonth"] = ci.subscriptions[0].photoCount;
                    dr["Left"] = ci.subscriptions[0].plan.photoCountPerMonth - ci.subscriptions[0].photoCount;
                    //dr["Start"] = ci.subscriptions[0].startDateBillingCycle;
                    //dr["End"] = ci.subscriptions[0].endDateBillingCycle;
                    dr["DaysLeft"] = ci.subscriptions[0].endDateBillingCycle.Subtract(DateTime.Now).Days;
                    dr["AutoRenew"] = ci.subscriptions[0].isAutoRenew;
                    dr["Multishot"] = ci.config.multiShot;
                    dr["Delay"] = ci.config.delay;
                    dr["MotionDelay"] = (ci.config.motionDelay / 60).ToString() + " mins"; ;
                    dr["Location"] = GetLocationUrl(conn, ci.id);

                    dt.Rows.Add(dr);
                }
            }
            ToCSV(dt, "cameraData.csv");
            return dt;
        }

        private string GetLocationUrl(SPConnection conn, string camera_id)
        {
            if (!conn.CameraPictures.ContainsKey(camera_id))
                return "";

            var cp = conn.CameraPictures[camera_id];
            if (cp.HaveLocation)
            {
                return String.Format("https://maps.google.com/?q={0},{1}", cp.Latitude.ToString(), cp.Longitude.ToString());
            }
            return "";
        }


        public void ToCSV(DataTable dtDataTable, string strFilePath)
        {
            var sw = new System.IO.StreamWriter(strFilePath, false);
            //headers    
            for (int i = 0; i < dtDataTable.Columns.Count; i++)
            {
                sw.Write(dtDataTable.Columns[i]);
                if (i < dtDataTable.Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
            foreach (DataRow dr in dtDataTable.Rows)
            {
                for (int i = 0; i < dtDataTable.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        string value = dr[i].ToString();
                        if (value.Contains(','))
                        {
                            value = String.Format("\"{0}\"", value);
                            sw.Write(value);
                        }
                        else
                        {
                            sw.Write(dr[i].ToString());
                        }
                    }
                    if (i < dtDataTable.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }
    }
}
